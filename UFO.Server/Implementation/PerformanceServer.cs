namespace UFO.Server.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class PerformanceServer : DatabaseObjectServer<Performance>,
                                     IPerformanceServer,
                                     IPerformanceServerAsync
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

        #endregion

        #region IPerformanceServerAsync Members

        public Task<IEnumerable<Performance>> GetByDateAsync(DateTime dateTime)
        {
            return Task.Run(() => GetByDate(dateTime));
        }

        public Task<IEnumerable<Performance>> GetUpcomingByArtistAsync(Artist artist)
        {
            return Task.Run(() => GetUpcomingByArtist(artist));
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
