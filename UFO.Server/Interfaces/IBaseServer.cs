namespace UFO.Server.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UFO.Domain;

    public interface IBaseServer<T>
        where T : DatabaseObject
    {
        T GetById(int id);

        bool Add(T o);

        void Remove(T o);

        bool Update(T o);

        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(int id);

        Task<bool> AddAsync(T o);

        Task RemoveAsync(T o);

        Task<bool> UpdateAsync(T o);

        Task<IEnumerable<T>> GetAllAsync();
    }
    
}
