package util;

import javax.faces.context.FacesContext;
import javax.faces.event.PhaseEvent;
import javax.faces.event.PhaseId;
import javax.faces.event.PhaseListener;

public class WebServiceAvailableCheck implements PhaseListener {
    @Override
    public void afterPhase(PhaseEvent phaseEvent) {
    }

    @Override
    public void beforePhase(PhaseEvent phaseEvent) {
        try {
            new UltimateFestivalOrganizer();
        } catch (Exception e) {
            FacesContext facesContext = FacesContext.getCurrentInstance();
            facesContext.getApplication().getNavigationHandler().handleNavigation(facesContext, null, "error");
        }
    }

    @Override
    public PhaseId getPhaseId() {
        return PhaseId.RESTORE_VIEW;
    }
}
