namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
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
                            new Tuple<string, string, object>("Artist_ID", "=", artist.Id),
                            new Tuple<string, string, object>("DateTime", ">", DateTime.Now)
                        });
        }

        #endregion
    }
}
