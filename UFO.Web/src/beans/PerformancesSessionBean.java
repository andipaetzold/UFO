package beans;

import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;
import javax.faces.bean.SessionScoped;
import javax.xml.datatype.XMLGregorianCalendar;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

@ManagedBean(name = "performancesSessionBean")
@SessionScoped
public class PerformancesSessionBean {
    private Date selectedDate = null;

    private boolean editMode;

    @ManagedProperty(value = "#{userBean}")
    private UserBean userBean;

    @PostConstruct
    public void init() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        // load dates
        List<Date> dates = new ArrayList<>();
        List<XMLGregorianCalendar> gregDates = ufo.getDatesWithPerformances().getDateTime();
        for (XMLGregorianCalendar d : gregDates) {
            if (d.getYear() >= Calendar.getInstance().get(Calendar.YEAR)) {
                dates.add(d.toGregorianCalendar().getTime());
            }
        }

        // set selected date
        if (dates.size() > 0) {
            selectedDate = dates.get(0);
        }

        // edit mode
        editMode = false;
    }

    public Date getSelectedDate() {
        return selectedDate;
    }

    public void setSelectedDate(Date selectedDate) {
        this.selectedDate = selectedDate;
    }

    public boolean isEditMode() {
        if (userBean.getLoggedIn()) {
            return editMode;
        } else {
            return false;
        }
    }

    public void setEditMode(boolean editMode) {
        if (userBean.getLoggedIn()) {
            this.editMode = editMode;
        } else {
            this.editMode = false;
        }
    }

    public void setUserBean(UserBean userBean) {
        this.userBean = userBean;
    }
}
