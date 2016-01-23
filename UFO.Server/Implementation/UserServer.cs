namespace UFO.Server.Implementation
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
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
            HashAlgorithm algorithm = MD5.Create();
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return GetDAO().SelectByUsernameAndPassword(username, sb.ToString()).Count() == 1;
        }

        public Task<bool> CheckLoginDataAsync(string username, string password)
        {
            return Task.Run(() => CheckLoginData(username, password));
        }

        #endregion

        protected IUserDAO GetDAO() => DALFactory.CreateUserDAO(Server.GetDatabase());

        protected override IBaseDAO<User> GetDatabaseObjectDAO() => GetDAO();
    }
}
