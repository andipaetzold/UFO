package util;

import services.UltimateFestivalOrganizer;
import services.UltimateFestivalOrganizerSoap;

public class UFOService {
    private static UltimateFestivalOrganizerSoap instance;

    public static UltimateFestivalOrganizerSoap getInstance() {
        return instance;
    }

    static {
        UltimateFestivalOrganizer service = new UltimateFestivalOrganizer();
        instance = service.getUltimateFestivalOrganizerSoap();
    }
}
