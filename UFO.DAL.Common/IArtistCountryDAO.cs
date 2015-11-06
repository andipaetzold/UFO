namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IArtistCountryDAO
    {
        bool Delete(Artist artist, Country country);

        IEnumerable<Country> GetByArtistId(int id);

        IEnumerable<Artist> GetByCountryId(int id);

        bool Insert(Artist artist, Country country);
    }
}
