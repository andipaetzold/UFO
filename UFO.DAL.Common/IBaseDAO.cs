namespace UFO.DAL.Common
{
    using System.Collections.Generic;

    public interface IBaseDAO<T>
    {
        bool Delete(T o);

        ICollection<T> SelectAll();

        T SelectById(int id);

        bool Insert(T o);

        bool Update(T o);
    }
}
