namespace UFO.Server
{
    using System.Configuration;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class VenueServer : DatabaseObjectServer<Venue>,
                               IBaseServer<Venue>
    {
        internal VenueServer()
        {
        }

        protected IVenueDAO GetDAO()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var database = DALFactory.CreateDatabase(connectionString);
            return DALFactory.CreateVenueDAO(database);
        }

        protected override IBaseDAO<Venue> GetDatabaseObjectDAO()
        {
            return GetDAO();
        }
    }
}
