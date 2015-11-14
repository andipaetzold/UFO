namespace UFO.DAL.SQLServer
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class VenueDAO : DatabaseObjectDAO<Venue>,
                            IVenueDAO
    {
        public VenueDAO(IDatabase database)
            : base(database)
        {
        }
    }
}
