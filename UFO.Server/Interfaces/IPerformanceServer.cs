namespace UFO.Server.Interfaces
{
    using System;
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IPerformanceServer : IBaseServer<Performance>
    {
        IEnumerable<Performance> GetByDate(DateTime dateTime);

        Performance GetByVenueAndDateTime(Venue venue, DateTime dateTime);

        IEnumerable<Performance> GetByArtist(Artist artist);
    }
}
