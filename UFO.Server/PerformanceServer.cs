namespace UFO.Server
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
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

        protected IPerformanceDAO GetDAO()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var database = DALFactory.CreateDatabase(connectionString);
            return DALFactory.CreatePerformanceDAO(database);
        }

        protected override IBaseDAO<Performance> GetDatabaseObjectDAO()
        {
            return GetDAO();
        }
    }
}
