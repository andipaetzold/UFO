package beans;

import services.*;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ViewScoped;
import javax.faces.context.FacesContext;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

@ManagedBean(name = "artistsBean")
@ViewScoped
public class ArtistsBean {
    private Artist artist;

    private List<Artist> artists = new ArrayList<>();
    private List<Artist> filteredArtists = new ArrayList<>();

    private List<Category> categories = new ArrayList<>();
    private List<Country> countries = new ArrayList<>();

    @PostConstruct
    public void init() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

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

        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();
        artist = ufo.getArtistById(id);
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
