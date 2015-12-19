namespace UFO.Server.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class PerformanceServer : DatabaseObjectServer<Performance>,
                                     IBaseServer<Performance>,
                                     IPerformanceServer
    {
        internal PerformanceServer()
        {
        }

        #region IPerformanceServer Members

        public IEnumerable<Performance> GetByDate(DateTime dateTime)
        {
            return GetDAO().SelectByDate(dateTime);
        }
        

        public IEnumerable<Performance> GetByArtist(Artist artist)
        {
            return GetDAO().SelectUpcomingPerformancesByArtist(artist);
        }

        #endregion

        protected IPerformanceDAO GetDAO() => DALFactory.CreatePerformanceDAO(Server.GetDatabase());

        protected override IBaseDAO<Performance> GetDatabaseObjectDAO() => GetDAO();
    }
}
