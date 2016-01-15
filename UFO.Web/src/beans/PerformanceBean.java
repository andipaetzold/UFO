package beans;

import services.Artist;
import services.Performance;
import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;
import javax.faces.context.ExternalContext;
import javax.faces.context.FacesContext;
import java.util.Map;

@ManagedBean(name = "performanceBean")
@RequestScoped
public class PerformanceBean {
    private Performance performance = null;

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
        performance = ufo.getPerformanceById(id);
    }

    public Performance getPerformance() {
        return performance;
    }
}
