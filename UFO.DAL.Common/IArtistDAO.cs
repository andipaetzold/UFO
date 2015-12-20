namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IArtistDAO : IBaseDAO<Artist>
    {
        IEnumerable<Artist> SelectAllButDeleted(); 
    }
}
