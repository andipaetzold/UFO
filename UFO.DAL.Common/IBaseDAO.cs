namespace UFO.DAL.Common
{
    using System.Collections.Generic;

    public interface IBaseDAO<T>
    {
        bool Delete(T o);

        ICollection<T> GetAll();

        T GetById(int id);

        bool Insert(T o);

        bool Update(T o);
    }
}
