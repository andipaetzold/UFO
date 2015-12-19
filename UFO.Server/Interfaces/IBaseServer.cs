namespace UFO.Server.Interfaces
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IBaseServer<T>
        where T : DatabaseObject
    {
        T GetById(int id);

        bool Add(T o);

        void Remove(T o);

        bool Update(T o);

        IEnumerable<T> GetAll();
    }
}
