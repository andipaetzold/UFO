namespace UFO.Server
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class VenueServer : DatabaseObjectServer<Venue>,
                               IBaseServer<Venue>
    {
        internal VenueServer()
        {
        }

        protected IVenueDAO GetDAO() => DALFactory.CreateVenueDAO(Server.GetDatabase());

        protected override IBaseDAO<Venue> GetDatabaseObjectDAO() => GetDAO();
    }
}
