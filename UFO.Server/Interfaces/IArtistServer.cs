namespace UFO.Server.Interfaces
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IArtistServer : IBaseServer<Artist>
    {
        void SendNotificationEmail(IEnumerable<Artist> artists);
        
    }
}
