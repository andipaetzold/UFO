package beans;

import services.Artist;
import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;

import javax.faces.bean.ApplicationScoped;
import javax.faces.bean.ManagedBean;
import java.util.List;

@ManagedBean(name = "artists")
@ApplicationScoped
public class ArtistsBean {
    public List<Artist> getAll() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        return ufo.getAllButDeletedArtists().getArtist();
    }
}
