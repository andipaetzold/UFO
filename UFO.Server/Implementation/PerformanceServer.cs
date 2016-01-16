namespace UFO.Server.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class PerformanceServer : DatabaseObjectServer<Performance>,
                                     IPerformanceServer
    {
        internal PerformanceServer()
        {
        }

        #region IPerformanceServer Members

        public override bool Update(Performance o)
        {
            return ValidateNewValue(o) && base.Update(o);
        }

        public override bool Add(Performance o)
        {
            return ValidateNewValue(o) && base.Add(o);
        }

        public IEnumerable<Performance> GetByDate(DateTime dateTime)
        {
            return GetDAO().SelectByDate(dateTime);
        }

        public IEnumerable<Performance> GetUpcomingByArtist(Artist artist)
        {
            return GetDAO().SelectUpcomingPerformancesByArtist(artist);
        }

        public Task<IEnumerable<Performance>> GetByDateAsync(DateTime dateTime)
        {
            return Task.Run(() => GetByDate(dateTime));
        }

        public Task<IEnumerable<Performance>> GetUpcomingByArtistAsync(Artist artist)
        {
            return Task.Run(() => GetUpcomingByArtist(artist));
        }

        public IEnumerable<Performance> GetByArtist(Artist artist)
        {
            return GetDAO().SelectByArtist(artist);
        }

        public Task<IEnumerable<Performance>> GetByArtistAsync(Artist artist)
        {
            return Task.Run(() => GetByArtist(artist));
        }

        public IEnumerable<DateTime> GetDatesWithPerformances()
        {
            return GetAll().Select(p => p.DateTime.Date).Distinct();
        }

        public Task<IEnumerable<DateTime>> GetDatesWithPerformancesAsync()
        {
            return Task.Run(() => GetDatesWithPerformances());
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
