namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UFO.Domain;

    public interface ICountryDAO
    {
        bool Delete(Country country);

        ICollection<Country> GetAll();

        Country GetById(int id);

        bool Insert(Country country);

        bool Update(Country country);
    }
}
