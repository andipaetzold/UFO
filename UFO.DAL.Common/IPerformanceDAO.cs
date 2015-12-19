namespace UFO.DAL.Common
{
    using System;
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IPerformanceDAO : IBaseDAO<Performance>
    {
        IEnumerable<Performance> SelectByDate(DateTime dateTime);

        IEnumerable<Performance> SelectUpcomingPerformancesByArtist(Artist artist);

        Performance SelectByVenueAndDateTime(Venue venue, DateTime dateTime);
    }
}
