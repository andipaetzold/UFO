namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface ICountryDAO
    {
        IEnumerable<Country> GetAll();

        Country GetById(int id);

        bool Insert(Country country);

        bool Update(Country country);
    }
}
