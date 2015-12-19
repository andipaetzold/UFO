namespace UFO.Server.Implementation
{
    using System.Collections.Generic;
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public abstract class DatabaseObjectServer<T> : IBaseServer<T>
        where T : DatabaseObject
    {
        #region IBaseServer<T> Members

        public T GetById(int id) => GetDatabaseObjectDAO().SelectById(id);

        public bool Add(T category) => GetDatabaseObjectDAO().Insert(category);

        public void Remove(T o) => GetDatabaseObjectDAO().Delete(o);

        public IEnumerable<T> GetAll() => GetDatabaseObjectDAO().SelectAll();

        public bool Update(T o) => GetDatabaseObjectDAO().Update(o);

        #endregion

        protected abstract IBaseDAO<T> GetDatabaseObjectDAO();
    }
}
