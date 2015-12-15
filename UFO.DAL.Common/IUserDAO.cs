namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface IUserDAO : IBaseDAO<User>
    {
        IEnumerable<User> SelectByUsernameAndPassword(string username, string password);
    }
}
