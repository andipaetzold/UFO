namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IUserDAO
    {
        bool Delete(User user);

        ICollection<User> GetAll();

        User GetById(int id);

        bool Insert(User user);

        bool Update(User user);
    }
}
