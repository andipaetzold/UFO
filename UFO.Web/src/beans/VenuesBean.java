package beans;

import services.Venue;
import util.UFOService;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
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
        venues = UFOService.getInstance().getAllVenues().getVenue();
    }

    public void updateDialog() {
        Map<String, String> requestParams = FacesContext.getCurrentInstance().getExternalContext().getRequestParameterMap();
        if (!requestParams.containsKey("id")) {
            return;
        }
        int id = Integer.valueOf(requestParams.get("id"));

        venue = UFOService.getInstance().getVenueById(id);
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
