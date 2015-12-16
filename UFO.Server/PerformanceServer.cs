namespace UFO.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class PerformanceServer : DatabaseObjectServer<Performance>,
                                     IBaseServer<Performance>
    {
        internal PerformanceServer()
        {
        }

        public IEnumerable<Performance> GetByDateTime(DateTime dateTime)
        {
            return GetDAO().SelectByDateTime(dateTime);
        }

        public Performance GetByVenueAndDateTime(Venue venue, DateTime dateTime)
        {
            return GetDAO().SelectByVenueAndDateTime(venue, dateTime).FirstOrDefault();
        }

        public IEnumerable<Performance> GetByArtist(Artist artist)
        {
            return GetDAO().SelectUpcomingPerformancesByArtist(artist);
        }

        protected IPerformanceDAO GetDAO() => DALFactory.CreatePerformanceDAO(Server.GetDatabase());

        protected override IBaseDAO<Performance> GetDatabaseObjectDAO() => GetDAO();
    }
}
