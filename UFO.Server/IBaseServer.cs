namespace UFO.Server
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IBaseServer<T>
        where T : DatabaseObject
    {
        T GetById(int id);

        void Add(T o);

        void Remove(T o);

        void Update(T o);

        IEnumerable<T> GetAll();
    }
}
