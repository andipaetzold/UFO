namespace UFO.Server
{
    using System.Configuration;
    using UFO.DAL.Common;

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

        internal static IDatabase GetDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            return DALFactory.CreateDatabase(connectionString);
        }
    }
}
