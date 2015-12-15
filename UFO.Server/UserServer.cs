namespace UFO.Server
{
    using System.Configuration;
    using System.Linq;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class UserServer : DatabaseObjectServer<User>,
                              IBaseServer<User>
    {
        internal UserServer()
        {
        }

        public bool CheckLoginData(string username, string password)
        {
            return GetDAO().SelectByUsernameAndPassword(username, password).Count() == 1;
        }

        protected IUserDAO GetDAO()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var database = DALFactory.CreateDatabase(connectionString);
            return DALFactory.CreateUserDAO(database);
        }

        protected override IBaseDAO<User> GetDatabaseObjectDAO()
        {
            return GetDAO();
        }
    }
}
