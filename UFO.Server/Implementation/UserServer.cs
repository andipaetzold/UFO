namespace UFO.Server.Implementation
{
    using System.Linq;
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class UserServer : DatabaseObjectServer<User>,
                              IBaseServer<User>,
                              IUserServer
    {
        internal UserServer()
        {
        }

        public bool CheckLoginData(string username, string password)
        {
            return GetDAO().SelectByUsernameAndPassword(username, password).Count() == 1;
        }

        protected IUserDAO GetDAO() => DALFactory.CreateUserDAO(Server.GetDatabase());

        protected override IBaseDAO<User> GetDatabaseObjectDAO() => GetDAO();
    }
}
