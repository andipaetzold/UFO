namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class CategoryDAO : ICategoryDAO
    {
        private const string SQLGetAll = @"
            SELECT 
                [Id], 
                [Name] 
            FROM 
                [Category];";
        private const string SQLGetById = @"
            SELECT 
                [Id], 
                [Name] 
            FROM 
                [Category] 
            WHERE 
                [Id] = @Id;";
        private const string SQLInsert = @"
            INSERT INTO 
                [Category] 
            (
                [Name]
            ) 
            OUTPUT [Inserted].[Id] 
            VALUES 
            (
                @Name
            );";
        private const string SQLUpdateById = @"
            UPDATE 
                [Category] 
            SET 
                [Name]=@Name 
            WHERE 
                [Id]=@Id;";

        #region Fields

        private readonly IDatabase database;

        #endregion

        public CategoryDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            // set
            this.database = database;
        }

        #region ICategoryDAO Members

        public IEnumerable<Category> GetAll()
        {
            using (var command = CreateGetAllCommand())
            {
                using (var reader = database.ExecuteReader(command))
                {
                    IList<Category> result = new List<Category>();
                    while (reader.Read())
                    {
                        result.Add(new Category((int)reader["Id"], (string)reader["Name"]));
                    }
                    return result;
                }
            }
        }

        public Category GetById(int id)
        {
            using (var command = CreateGetByIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        return new Category((int)reader["Id"], (string)reader["Name"]);
                    }
                    return null;
                }
            }
        }

        public bool Insert(Category category)
        {
            // check parameter
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            // insert
            using (var command = CreateInsertCommand(category.Name))
            {
                var id = database.ExecuteScalar(command) as int?;

                if (id.HasValue)
                {
                    category.Id = id.Value;
                    return true;
                }
                return false;
            }
        }

        public bool Update(Category category)
        {
            // check parameter
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            // update
            using (var command = CreateUpdateByIdCommand(category.Id, category.Name))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        #endregion

        private DbCommand CreateGetAllCommand()
        {
            return database.CreateCommand(SQLGetAll);
        }

        private DbCommand CreateGetByIdCommand(int id)
        {
            var getByIdCommenCommand = database.CreateCommand(SQLGetById);
            database.DefineParameter(getByIdCommenCommand, "Id", DbType.Int32, id);
            return getByIdCommenCommand;
        }

        private DbCommand CreateInsertCommand(string name)
        {
            var insertCommand = database.CreateCommand(SQLInsert);
            database.DefineParameter(insertCommand, "Name", DbType.String, name);
            return insertCommand;
        }

        private DbCommand CreateUpdateByIdCommand(int id, string name)
        {
            var updateByIdCommand = database.CreateCommand(SQLUpdateById);
            database.DefineParameter(updateByIdCommand, "Id", DbType.Int32, id);
            database.DefineParameter(updateByIdCommand, "Name", DbType.String, name);
            return updateByIdCommand;
        }
    }
}
