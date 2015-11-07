namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UFO.Domain;

    public interface IPerformanceDAO
    {
        bool Delete(Performance performance);

        ICollection<Performance> GetAll();

        Performance GetById(int id);

        bool Insert(Performance performance);

        bool Update(Performance performance);
    }
}
