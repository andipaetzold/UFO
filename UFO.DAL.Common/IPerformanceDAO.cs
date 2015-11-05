namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IPerformanceDAO
    {
        IEnumerable<Performance> GetAll();

        Performance GetById(int id);

        bool Insert(Performance performance);

        bool Update(Performance performance);
    }
}
