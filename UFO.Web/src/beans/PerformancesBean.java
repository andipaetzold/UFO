package beans;

import services.Performance;
import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;
import services.Venue;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;
import javax.faces.bean.SessionScoped;
import javax.xml.datatype.XMLGregorianCalendar;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.stream.Collectors;

@ManagedBean(name = "performancesBean")
@RequestScoped
public class PerformancesBean {
    private List<Date> dates = new ArrayList<>();
    private Date selectedDate = null;
    private DateFormat format = new SimpleDateFormat("dd.MM.yyy");

    private List<Integer> times;
    private List<Venue> venues;
    private Map<Integer, Map<Integer, Performance>> performances;

    @PostConstruct
    public void init() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        // load dates
        List<XMLGregorianCalendar> gregDates = ufo.getDatesWithPerformances().getDateTime();
        dates.addAll(gregDates.stream().map(date -> date.toGregorianCalendar().getTime()).collect(Collectors.toList()));

        // times to display
        times = new ArrayList<>();
        for (int t = 14; t <= 23; ++t) {
            times.add(t % 24);
        }

        // venues
        venues = ufo.getAllVenues().getVenue();

        // performances
        performances = new HashMap<>();
        
        List<Performance> allPerformances = ufo.getAllPerformances().getPerformance();
        for (Performance p : allPerformances) {
            int venueId = p.getVenue().getId();
            int hour = p.getDateTime().getHour();

            if (!performances.containsKey(venueId)) {
                performances.put(venueId, new HashMap<>());
            }

            performances.get(venueId).put(hour, p);
        }
    }

    public List<String> getDates() {
        return dates.stream().map(date -> format.format(date)).collect(Collectors.toList());
    }

    public Date getToday() {
        return new Date();
    }

    public String getSelectedDate() {
        return (selectedDate == null) ? "" : format.format(selectedDate);
    }

    public void setSelectedDate(String selectedDate) {
        try {
            this.selectedDate = format.parse(selectedDate);
        } catch (ParseException e) {
            this.selectedDate = null;
        }
    }

    public void onSelectedDateChange() {
        System.out.println(this.selectedDate);
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
}
