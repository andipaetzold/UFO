namespace UFO.Server.Implementation
{
    using System.Configuration;
    using UFO.DAL.Common;
    using UFO.Server.Interfaces;

    public static class Server
    {
        #region Properties

        public static IArtistServerAsync ArtistServer { get; } = new ArtistServer();
        public static ICategoryServerAsync CategoryServer { get; } = new CategoryServer();
        public static ICountryServerAsync CountryServer { get; } = new CountryServer();
        public static IPerformanceServerAsync PerformanceServer { get; } = new PerformanceServer();
        public static IUserServerAsync UserServer { get; } = new UserServer();
        public static IVenueServerAsync VenueServer { get; } = new VenueServer();

        #endregion

        internal static IDatabase GetDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            return DALFactory.CreateDatabase(connectionString);
        }
    }
}
