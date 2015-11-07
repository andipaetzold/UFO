namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IVenueDAO
    {
        bool Delete(Venue venue);

        ICollection<Venue> GetAll();

        Venue GetById(int id);

        bool Insert(Venue venue);

        bool Update(Venue venue);
    }
}
