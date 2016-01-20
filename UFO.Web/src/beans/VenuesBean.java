package beans;

import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;
import services.Venue;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

@ManagedBean(name = "venuesBean")
@ViewScoped
public class VenuesBean {
    private Venue venue;

    private List<Venue> venues;
    private List<Venue> filteredVenues;

    @PostConstruct
    public void init() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        venues = ufo.getAllVenues().getVenue();
    }

    public void updateDialog() {
        Map<String, String> requestParams = FacesContext.getCurrentInstance().getExternalContext().getRequestParameterMap();
        if (!requestParams.containsKey("id")) {
            return;
        }
        int id = Integer.valueOf(requestParams.get("id"));

        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();
        venue = ufo.getVenueById(id);
    }

    public Venue getVenue() {
        return venue;
    }

    public List<Venue> getVenues() {
        return venues;
    }

    public List<Venue> getFilteredVenues() {
        return filteredVenues;
    }

    public void setFilteredVenues(List<Venue> filteredVenues) {
        this.filteredVenues = filteredVenues;
    }
}
