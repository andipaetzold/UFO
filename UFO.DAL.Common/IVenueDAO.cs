namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IVenueDAO
    {
        IEnumerable<Venue> GetAll();

        Venue GetById(int id);

        bool Insert(Venue venue);

        bool Update(Venue venue);
    }
}
