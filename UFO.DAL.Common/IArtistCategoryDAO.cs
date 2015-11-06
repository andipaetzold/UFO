namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IArtistCategoryDAO
    {
        bool Delete(Artist artist, Category category);

        IEnumerable<Category> GetByArtistId(int id);

        IEnumerable<Artist> GetByCategoryId(int id);

        bool Insert(Artist artist, Category category);
    }
}
