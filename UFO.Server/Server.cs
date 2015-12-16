namespace UFO.Server
{
    using System.Configuration;
    using UFO.DAL.Common;

    public static class Server
    {
        #region Properties

        public static ArtistServer ArtistServer { get; } = new ArtistServer();
        public static CategoryServer CategoryServer { get; } = new CategoryServer();
        public static CountryServer CountryServer { get; } = new CountryServer();
        public static PerformanceServer PerformanceServer { get; } = new PerformanceServer();
        public static UserServer UserServer { get; } = new UserServer();
        public static VenueServer VenueServer { get; } = new VenueServer();

        #endregion

        internal static IDatabase GetDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            return DALFactory.CreateDatabase(connectionString);
        }
    }
}
