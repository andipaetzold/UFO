namespace UFO.Server.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UFO.Domain;

    public interface IPerformanceServer : IBaseServer<Performance>
    {
        IEnumerable<Performance> GetByDate(DateTime dateTime);

        IEnumerable<Performance> GetUpcomingByArtist(Artist artist);
    }
    public interface IPerformanceServerAsync : IBaseServerAsync<Performance>
    {
        Task<IEnumerable<Performance>> GetByDateAsync(DateTime dateTime);

        Task<IEnumerable<Performance>> GetUpcomingByArtistAsync(Artist artist);
    }
}
