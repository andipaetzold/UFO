namespace UFO.Server.Interfaces
{
    using System;
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IPerformanceServer : IBaseServer<Performance>
    {
        IEnumerable<Performance> GetByDate(DateTime dateTime);

        IEnumerable<Performance> GetUpcomingByArtist(Artist artist);
    }
}
