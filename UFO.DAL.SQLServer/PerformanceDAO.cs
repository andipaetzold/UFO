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

        public IEnumerable<Performance> SelectByDateTime(DateTime dateTime)
        {
            return SelectByCondition(new[] { new Tuple<string, string, object>("DateTime", "=", dateTime) });
        }

        public IEnumerable<Performance> SelectByVenueAndDateTime(Venue venue, DateTime dateTime)
        {
            return
                SelectByCondition(
                    new[]
                        {
                            new Tuple<string, string, object>("Venue_Id", "=", venue.Id),
                            new Tuple<string, string, object>("DateTime", "=", dateTime)
                        });
        }

        #endregion
    }
}
