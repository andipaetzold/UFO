namespace UFO.Server
{
    public class Server
    {
        #region Properties

        public ArtistServer ArtistServer { get; } = new ArtistServer();
        public CategoryServer CategoryServer { get; } = new CategoryServer();
        public CountryServer CountryServer { get; } = new CountryServer();
        public PerformanceServer PerformanceServer { get; } = new PerformanceServer();
        public UserServer UserServer { get; } = new UserServer();
        public VenueServer VenueServer { get; } = new VenueServer();

        #endregion
    }
}
