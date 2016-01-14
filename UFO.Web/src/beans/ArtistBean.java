package beans;

import services.Artist;
import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;
import javax.faces.context.ExternalContext;
import javax.faces.context.FacesContext;
import java.util.Map;

@ManagedBean(name = "artistBean")
@RequestScoped
public class ArtistBean {
    private Artist artist = null;

    @PostConstruct
    public void init() {
        ExternalContext context = FacesContext.getCurrentInstance().getExternalContext();

        Map<String, String> requestParameterMap = context.getRequestParameterMap();

        if (!requestParameterMap.containsKey("id")) {
            return;
        }

        int id;
        try {
            id = Integer.parseInt(requestParameterMap.get("id"));
        } catch (NumberFormatException e) {
            return;
        }

        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();
        artist = ufo.getArtistById(id);

        if (artist.isIsDeleted()) {
            artist = null;
        }
    }

    public Artist getArtist() {
        return artist;
    }
}
