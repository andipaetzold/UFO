namespace UFO.Server.Implementation
{
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class VenueServer : DatabaseObjectServer<Venue>,
                               IVenueServer,
                               IVenueServerAsync
    {
        internal VenueServer()
        {
        }

        protected IVenueDAO GetDAO() => DALFactory.CreateVenueDAO(Server.GetDatabase());

        protected override IBaseDAO<Venue> GetDatabaseObjectDAO() => GetDAO();
    }
}
