namespace UFO.Server.Implementation
{
    using System.Linq;
    using System.Threading.Tasks;
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class UserServer : DatabaseObjectServer<User>,
                              IUserServer
    {
        internal UserServer()
        {
        }

        #region IUserServer Members

        public bool CheckLoginData(string username, string password)
        {
            return GetDAO().SelectByUsernameAndPassword(username, password).Count() == 1;
        }

        #endregion

        #region IUserServerAsync Members

        public Task<bool> CheckLoginDataAsync(string username, string password)
        {
            return Task.Run(() => CheckLoginData(username, password));
        }

        #endregion

        protected IUserDAO GetDAO() => DALFactory.CreateUserDAO(Server.GetDatabase());

        protected override IBaseDAO<User> GetDatabaseObjectDAO() => GetDAO();
    }
}
