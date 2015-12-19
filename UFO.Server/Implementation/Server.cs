namespace UFO.Server.Implementation
{
    using System.Configuration;
    using UFO.DAL.Common;
    using UFO.Server.Interfaces;

    public static class Server
    {
        #region Properties

        public static IArtistServer ArtistServer { get; } = new ArtistServer();
        public static ICategoryServer CategoryServer { get; } = new CategoryServer();
        public static ICountryServer CountryServer { get; } = new CountryServer();
        public static IPerformanceServer PerformanceServer { get; } = new PerformanceServer();
        public static IUserServer UserServer { get; } = new UserServer();
        public static IVenueServer VenueServer { get; } = new VenueServer();

        #endregion

        internal static IDatabase GetDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            return DALFactory.CreateDatabase(connectionString);
        }
    }
}
