namespace UFO.Server.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UFO.Domain;

    public interface IArtistServer : IBaseServer<Artist>
    {
        void SendNotificationEmail(IEnumerable<Artist> artists);
    }

    public interface IArtistServerAsync : IBaseServerAsync<Artist>
    {
        Task SendNotificationEmailAsync(IEnumerable<Artist> artists);
    }
}
