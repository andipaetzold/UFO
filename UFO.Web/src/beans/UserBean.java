package beans;

import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;

import javax.faces.application.FacesMessage;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.faces.context.FacesContext;

@ManagedBean(name = "userBean")
@SessionScoped
public class UserBean {
    private String username;
    private String password;
    private boolean loggedIn = false;

    public void login() {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        UltimateFestivalOrganizerSoap ufo = service.getUltimateFestivalOrganizerSoap();

        loggedIn = ufo.checkLogin(username, password);

        if (!loggedIn) {
            FacesContext facesContext = FacesContext.getCurrentInstance();
            FacesMessage facesMessage = new FacesMessage(FacesMessage.SEVERITY_ERROR, "Error", "Invalid username or password.");
            facesContext.addMessage(null, facesMessage);
        }
    }

    public void logout() {
        loggedIn = false;
    }

    public String getUsername() {
        return "";
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return "";
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public boolean getLoggedIn() {
        return loggedIn;
    }
}
