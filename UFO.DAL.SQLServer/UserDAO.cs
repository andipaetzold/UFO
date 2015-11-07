namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class UserDAO : IUserDAO
    {
        private const string SQLDeleteById = @"
            DELETE FROM
                [User]
            WHERE
                [Id]=@Id;";
        private const string SQLGetAll = @"
            SELECT
                [Id],
                [Username],
                [Password],
                [Email],
                [IsAdmin]
            FROM
                [User];";
        private const string SQLGetById = @"
            SELECT
                [Id],
                [Username],
                [Password],
                [Email],
                [IsAdmin]
            FROM
                [User]
            WHERE
                [Id]=@Id;";
        private const string SQLInsert = @"
            INSERT INTO
                [User]
            (
                [Username],
                [Password],
                [Email],
                [IsAdmin]
            )
            OUTPUT [Inserted].[Id]
            VALUES
            (
                @Username,
                @Password,
                @Email,
                @IsAdmin
            );";
        private const string SQLUpdateById = @"
            UPDATE
                [User]
            SET
                [Username] = @Username,
                [Password] = @Password,
                [Email] = @Email,
                [IsAdmin] = @IsAdmin
            WHERE
                [Id]=@Id;";

        #region Fields

        private readonly IDatabase database;

        #endregion

        public UserDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            // set
            this.database = database;
        }

        #region IUserDAO Members

        public bool Delete(User user)
        {
            // check parameter
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // insert
            using (var command = CreateDeleteByIdCommand(user.Id))
            {
                if (database.ExecuteNonQuery(command) == 1)
                {
                    user.DeletedFromDatabase();
                    return true;
                }
                return false;
            }
        }

        public ICollection<User> GetAll()
        {
            using (var command = CreateGetAllCommand())
            {
                using (var reader = database.ExecuteReader(command))
                {
                    IList<User> result = new List<User>();
                    while (reader.Read())
                    {
                        result.Add(
                            new User(
                                (int)reader["Id"],
                                (string)reader["Username"],
                                (string)reader["Password"],
                                (string)reader["Email"],
                                (bool)reader["IsAdmin"]));
                    }
                    return result;
                }
            }
        }

        public User GetById(int id)
        {
            using (var command = CreateGetByIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        return new User(
                            (int)reader["Id"],
                            (string)reader["Username"],
                            (string)reader["Password"],
                            (string)reader["Email"],
                            (bool)reader["IsAdmin"]);
                    }
                    return null;
                }
            }
        }

        public bool Update(User user)
        {
            // check parameter
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // update
            using (
                var command = CreateUpdateByIdCommand(user.Id, user.Username, user.Password, user.Email, user.IsAdmin))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        public bool Insert(User user)
        {
            // check parameter
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // insert
            using (var command = CreateInsertCommand(user.Username, user.Password, user.Email, user.IsAdmin))
            {
                var id = database.ExecuteScalar(command) as int?;

                if (id.HasValue)
                {
                    user.InsertedInDatabase(id.Value);
                    return true;
                }
                return false;
            }
        }

        #endregion

        private DbCommand CreateDeleteByIdCommand(int id)
        {
            var deleteByIdCommand = database.CreateCommand(SQLDeleteById);
            database.DefineParameter(deleteByIdCommand, "Id", DbType.Int32, id);
            return deleteByIdCommand;
        }

        private DbCommand CreateGetAllCommand()
        {
            return database.CreateCommand(SQLGetAll);
        }

        private DbCommand CreateGetByIdCommand(int id)
        {
            var getByIdCommand = database.CreateCommand(SQLGetById);
            database.DefineParameter(getByIdCommand, "Id", DbType.Int32, id);
            return getByIdCommand;
        }

        private DbCommand CreateInsertCommand(string username, string password, string email, bool isAdmin)
        {
            var insertCommand = database.CreateCommand(SQLInsert);
            database.DefineParameter(insertCommand, "Username", DbType.String, username);
            database.DefineParameter(insertCommand, "Password", DbType.String, password);
            database.DefineParameter(insertCommand, "Email", DbType.String, email);
            database.DefineParameter(insertCommand, "IsAdmin", DbType.Boolean, isAdmin);
            return insertCommand;
        }

        private DbCommand CreateUpdateByIdCommand(int id, string username, string password, string email, bool isAdmin)
        {
            var updateByIdCommand = database.CreateCommand(SQLUpdateById);
            database.DefineParameter(updateByIdCommand, "Id", DbType.Int32, id);
            database.DefineParameter(updateByIdCommand, "Username", DbType.String, username);
            database.DefineParameter(updateByIdCommand, "Password", DbType.String, password);
            database.DefineParameter(updateByIdCommand, "Email", DbType.String, email);
            database.DefineParameter(updateByIdCommand, "IsAdmin", DbType.Boolean, isAdmin);
            return updateByIdCommand;
        }
    }
}
