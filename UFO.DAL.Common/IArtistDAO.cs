namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IArtistDAO
    {
        IEnumerable<Artist> GetAll();

        Artist GetById(int id);

        bool Insert(Artist artist);

        bool Update(Artist artist);
    }
}
