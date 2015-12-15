namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class UserDAO : DatabaseObjectDAO<User>,
                           IUserDAO
    {
        public UserDAO(IDatabase database)
            : base(database)
        {
        }

        public IEnumerable<User> SelectByUsernameAndPassword(string username, string password)
        {
            return
                SelectByCondition(
                    new[]
                        {
                            new Tuple<string, string, object>("Username", "=", username),
                            new Tuple<string, string, object>("Password", "=", password)
                        });
        }
    }
}
