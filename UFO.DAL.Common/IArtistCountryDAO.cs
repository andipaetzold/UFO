namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UFO.Domain;

    public interface IArtistCountryDAO
    {
        bool Delete(Artist artist, Country country);

        ICollection<Country> GetByArtist(Artist artist);

        ICollection<Artist> GetByCountry(Country country);

        bool Insert(Artist artist, Country country);
    }
}
