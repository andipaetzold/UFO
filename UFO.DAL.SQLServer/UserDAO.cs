namespace UFO.DAL.SQLServer
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class UserDAO : DatabaseObjectDAO<User>,
                           IUserDAO
    {
        public UserDAO(IDatabase database)
            : base(database)
        {
        }
    }
}
