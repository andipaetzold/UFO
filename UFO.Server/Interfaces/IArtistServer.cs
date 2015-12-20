namespace UFO.Server.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UFO.Domain;

    public interface IArtistServer : IBaseServer<Artist>
    {
        void SendNotificationEmail(IEnumerable<Artist> artists);

        IEnumerable<Artist> GetAllButDeleted();
        Task SendNotificationEmailAsync(IEnumerable<Artist> artists);

        Task<IEnumerable<Artist>> GetAllButDeletedAsync();
    }
    
}
