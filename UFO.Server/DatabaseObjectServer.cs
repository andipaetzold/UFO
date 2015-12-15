namespace UFO.Server
{
    using System;
    using System.Collections.Generic;
    using UFO.DAL.Common;
    using UFO.Domain;

    public abstract class DatabaseObjectServer<T> : IBaseServer<T>
        where T : DatabaseObject
    {
        #region IBaseServer<T> Members

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(T category)
        {
            GetDatabaseObjectDAO().Insert(category);
        }

        public void Remove(T o)
        {
            GetDatabaseObjectDAO().Delete(o);
        }

        #endregion

        public IEnumerable<T> GetAll()
        {
            return GetDatabaseObjectDAO().SelectAll();
        }

        public void Update(T o)
        {
            GetDatabaseObjectDAO().Update(o);
        }

        protected abstract IBaseDAO<T> GetDatabaseObjectDAO();
    }
}
