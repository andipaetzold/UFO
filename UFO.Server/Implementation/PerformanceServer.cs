namespace UFO.Server.Implementation
{
    using System;
    using System.Collections.Generic;
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

        #region IBaseServer<Performance> Members

        public override bool Update(Performance o)
        {
            return ValidateNewValue(o) && base.Update(o);
        }

        public override bool Add(Performance o)
        {
            return ValidateNewValue(o) && base.Add(o);
        }

        #endregion

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

        private bool ValidateNewValue(Performance o)
        {
            var prevDateTime = o.DateTime.AddHours(-1);
            var nextDateTime = o.DateTime.AddHours(1);

            if (GetDAO().SelectByVenueAndDateTime(o.Venue, prevDateTime) == null
                && GetDAO().SelectByVenueAndDateTime(o.Venue, nextDateTime) == null)
            {
                return true;
            }
            return false;
        }
    }
}
