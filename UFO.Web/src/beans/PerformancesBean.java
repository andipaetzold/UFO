package beans;

import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.xml.datatype.XMLGregorianCalendar;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.stream.Collectors;

@ManagedBean(name = "performancesBean")
@SessionScoped
public class PerformancesBean {
    private List<Date> dates = new ArrayList<>();
    private Date selectedDate = null;
    private DateFormat format = new SimpleDateFormat("dd.MM.yyy");

    @PostConstruct
    public void init() {
        // load dates
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        List<XMLGregorianCalendar> gregDates = ufo.getDatesWithPerformances().getDateTime();
        dates.addAll(gregDates.stream().map(date -> date.toGregorianCalendar().getTime()).collect(Collectors.toList()));
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
}
