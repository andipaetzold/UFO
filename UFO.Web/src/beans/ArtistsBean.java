package beans;

import services.Artist;
import services.Category;
import services.Country;
import services.UltimateFestivalOrganizerSoap;
import util.UFOService;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
import java.util.List;
import java.util.Map;

@ManagedBean(name = "artistsBean")
@ViewScoped
public class ArtistsBean {
    private Artist artist;

    private List<Artist> artists;
    private List<Artist> filteredArtists;

    private List<Category> categories;
    private List<Country> countries;

    @PostConstruct
    public void init() {
        UltimateFestivalOrganizerSoap ufo = UFOService.getInstance();

        artists = ufo.getAllButDeletedArtists().getArtist();
        categories = ufo.getAllCategories().getCategory();
        countries = ufo.getAllCountries().getCountry();
    }

    public void updateDialog() {
        Map<String, String> requestParams = FacesContext.getCurrentInstance().getExternalContext().getRequestParameterMap();
        if (!requestParams.containsKey("id")) {
            return;
        }
        int id = Integer.valueOf(requestParams.get("id"));

        artist = UFOService.getInstance().getArtistById(id);
    }


    public List<Artist> getArtists() {
        return artists;
    }

    public Artist getArtist() {
        return artist;
    }

    public List<Artist> getFilteredArtists() {
        return filteredArtists;
    }

    public void setFilteredArtists(List<Artist> filteredArtists) {
        this.filteredArtists = filteredArtists;
    }

    public List<Category> getCategories() {
        return categories;
    }

    public List<Country> getCountries() {
        return countries;
    }
}
