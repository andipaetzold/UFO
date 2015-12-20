namespace UFO.Server.Implementation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public abstract class DatabaseObjectServer<T> : IBaseServer<T>,
                                                    IBaseServerAsync<T>
        where T : DatabaseObject
    {
        #region IBaseServer<T> Members

        public T GetById(int id) => GetDatabaseObjectDAO().SelectById(id);

        public virtual bool Add(T category) => GetDatabaseObjectDAO().Insert(category);

        public void Remove(T o) => GetDatabaseObjectDAO().Delete(o);

        public IEnumerable<T> GetAll() => GetDatabaseObjectDAO().SelectAll();

        public virtual bool Update(T o) => GetDatabaseObjectDAO().Update(o);

        #endregion

        #region IBaseServerAsync<T> Members

        public Task<T> GetByIdAsync(int id)
        {
            return Task.Run(() => GetById(id));
        }

        public Task<bool> AddAsync(T o)
        {
            return Task.Run(() => Add(o));
        }

        public Task RemoveAsync(T o)
        {
            return Task.Run(() => Remove(o));
        }

        public Task<bool> UpdateAsync(T o)
        {
            return Task.Run(() => Update(o));
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        #endregion

        protected abstract IBaseDAO<T> GetDatabaseObjectDAO();
    }
}
