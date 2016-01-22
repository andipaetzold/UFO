package beans;

import services.*;

import javax.annotation.PostConstruct;
import javax.faces.application.FacesMessage;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
import javax.xml.datatype.DatatypeConfigurationException;
import javax.xml.datatype.DatatypeFactory;
import javax.xml.datatype.XMLGregorianCalendar;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.stream.Collectors;

@ManagedBean(name = "performancesBean")
@ViewScoped
public class PerformancesBean {
    private List<Date> dates = new ArrayList<>();
    private DateFormat format = new SimpleDateFormat("dd.MM.yyy");

    private List<Integer> times;
    private List<Artist> artists;
    private List<Venue> venues;
    private Map<Integer, Map<Integer, Performance>> performances = new HashMap<>();

    private Performance dialogPerformance;

    @ManagedProperty(value = "#{userBean}")
    private UserBean userBean;

    @ManagedProperty(value = "#{performancesSessionBean}")
    private PerformancesSessionBean performancesSessionBean;

    @PostConstruct
    public void init() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        // load dates
        List<XMLGregorianCalendar> gregDates = ufo.getDatesWithPerformances().getDateTime();
        for (XMLGregorianCalendar d : gregDates) {
            if (d.getYear() >= Calendar.getInstance().get(Calendar.YEAR)) {
                dates.add(d.toGregorianCalendar().getTime());
            }
        }

        // times to display
        times = new ArrayList<>();
        for (int t = 14; t <= 23; ++t) {
            times.add(t % 24);
        }

        // artists / venues / performances
        reload();
    }

    private void reload() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        venues = ufo.getAllVenues().getVenue();
        artists = ufo.getAllButDeletedArtists().getArtist();

        // Performances
        performances.clear();

        if (performancesSessionBean.getSelectedDate() == null) {
            return;
        }

        // fill with new
        for (Venue v : venues) {
            performances.put(v.getId(), new HashMap<>());

            for (int hour = 14; hour <= 23; ++hour) {
                Performance p = new Performance();
                p.setVenue(v);

                Calendar c = new GregorianCalendar();
                c.setTime(performancesSessionBean.getSelectedDate());
                c.add(Calendar.HOUR_OF_DAY, hour);
                p.setDateTime(dateToGregorian(c.getTime()));

                Artist a = new Artist();
                a.setId(0);
                p.setArtist(a);

                performances.get(v.getId()).put(hour, p);
            }
        }

        // fill with existing
        List<Performance> allPerformances = ufo.getPerformancesByDate(dateToGregorian(performancesSessionBean.getSelectedDate())).getPerformance();
        for (Performance p : allPerformances) {
            int venueId = p.getVenue().getId();
            int hour = p.getDateTime().getHour();

            performances.get(venueId).put(hour, p);
        }
    }

    public List<String> getDates() {
        return dates.stream().map(date -> format.format(date)).collect(Collectors.toList());
    }

    public String getSelectedDate() {
        return (performancesSessionBean.getSelectedDate() == null) ? "" : format.format(performancesSessionBean.getSelectedDate());
    }

    public void setSelectedDate(String selectedDate) {
        try {
            performancesSessionBean.setSelectedDate(format.parse(selectedDate));
        } catch (ParseException e) {
            performancesSessionBean.setSelectedDate(null);
        }
    }

    public void onSelectedDateChange() {
        reload();
    }

    public List<Integer> getTimes() {
        return times;
    }

    public List<Venue> getVenues() {
        return venues;
    }

    public Map<Integer, Map<Integer, Performance>> getPerformances() {
        return performances;
    }

    public void updateDialog() {
        Map<String, String> requestParams = FacesContext.getCurrentInstance().getExternalContext().getRequestParameterMap();
        if (!requestParams.containsKey("id")) {
            return;
        }
        int id = Integer.valueOf(requestParams.get("id"));

        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();
        dialogPerformance = ufo.getPerformanceById(id);
    }

    public Performance getDialogPerformance() {
        return dialogPerformance;
    }

    public List<Artist> getArtists() {
        return artists;
    }

    public void onSelectedArtistChange(int viewId, int time) {
        // check login
        if (!userBean.getLoggedIn()) {
            return;
        }

        // execute action
        Performance p = performances.get(viewId).get(time);

        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        if (p.getId() == 0 && p.getArtist().getId() == 0) {
            return;
        }

        // send to server
        boolean success = true;
        if (p.getId() == 0 && p.getArtist().getId() != 0) {
            success = ufo.addPerformance(p);
        } else if (p.getId() != 0 && p.getArtist().getId() == 0) {
            ufo.deletePerformance(p);
            success = true;
        } else if (p.getId() != 0 && p.getArtist().getId() != 0) {
            success = ufo.updatePerformance(p);
        }

        // message
        FacesContext facesContext = FacesContext.getCurrentInstance();
        FacesMessage facesMessage;
        if (success) {
            facesMessage = new FacesMessage(FacesMessage.SEVERITY_INFO, "Saved", "Data was saved on the server.");
        } else {
            facesMessage = new FacesMessage(FacesMessage.SEVERITY_ERROR, "Error", "The selected artist was invalid. The item will be reset.");
            reload();
        }
        facesContext.addMessage(null, facesMessage);
    }

    public static XMLGregorianCalendar dateToGregorian(Date date) {
        try {
            GregorianCalendar c = new GregorianCalendar();
            c.setTime(date);
            return DatatypeFactory.newInstance().newXMLGregorianCalendar(c);
        } catch (DatatypeConfigurationException e) {
        }
        return null;
    }

    public boolean isEditMode() {
        if (userBean.getLoggedIn()) {
            return performancesSessionBean.isEditMode();
        } else {
            return false;
        }
    }

    public void setEditMode(boolean editMode) {
        if (userBean.getLoggedIn()) {
            performancesSessionBean.setEditMode(editMode);
        } else {
            performancesSessionBean.setEditMode(false);
        }
    }

    public void setUserBean(UserBean userBean) {
        this.userBean = userBean;
    }

    public void setPerformancesSessionBean(PerformancesSessionBean performancesSessionBean) {
        this.performancesSessionBean = performancesSessionBean;
    }
}
