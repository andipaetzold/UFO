namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IArtistCategoryDAO
    {
        bool Delete(Artist artist, Category category);

        ICollection<Category> GetByArtist(Artist artist);

        ICollection<Artist> GetByCategory(Category category);

        bool Insert(Artist artist, Category category);
    }
}
