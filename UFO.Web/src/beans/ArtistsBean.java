package beans;

import services.Artist;
import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;
import java.util.List;

@ManagedBean(name = "artistsBean")
@RequestScoped
public class ArtistsBean {
    public List<Artist> getAll() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        return ufo.getAllButDeletedArtists().getArtist();
    }
}
