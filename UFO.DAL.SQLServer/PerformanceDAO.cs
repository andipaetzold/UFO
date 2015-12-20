namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class PerformanceDAO : DatabaseObjectDAO<Performance>,
                                  IPerformanceDAO
    {
        public PerformanceDAO(IDatabase database)
            : base(database)
        {
        }

        #region IPerformanceDAO Members

        public IEnumerable<Performance> SelectByDate(DateTime dateTime)
        {
            var from = dateTime.Date;
            var to = from.AddDays(1);
            return
                SelectByCondition(
                    new[]
                        {
                            new Tuple<string, string, object>("DateTime", ">=", from),
                            new Tuple<string, string, object>("DateTime", "<", to)
                        });
        }

        public IEnumerable<Performance> SelectUpcomingPerformancesByArtist(Artist artist)
        {
            return
                SelectByCondition(
                    new[]
                        {
                            new Tuple<string, string, object>("Artist_Id", "=", artist.Id),
                            new Tuple<string, string, object>("DateTime", ">=", DateTime.Now.Date)
                        });
        }

        public Performance SelectByVenueAndDateTime(Venue venue, DateTime dateTime)
        {
            return
                SelectByCondition(
                    new[]
                        {
                            new Tuple<string, string, object>("Venue_ID", "=", venue.Id),
                            new Tuple<string, string, object>("DateTime", "=", dateTime)
                        }).FirstOrDefault();
        }

        public IEnumerable<Performance> SelectByArtist(Artist artist)
        {
            return SelectByCondition(new[] { new Tuple<string, string, object>("Artist_Id", "=", artist.Id) });
        }

        #endregion
    }
}
