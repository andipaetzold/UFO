namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class CountryDAO : ICountryDAO
    { 
        private const string SQLGetById = @"
            SELECT 
                [Id], 
                [Name] 
            FROM 
                [Country] 
            WHERE 
                [Id]=@Id;";
        private const string SQLGetAll = @"
            SELECT 
                [Id], 
                [Name] 
            FROM 
                [Country];";
        private const string SQLInsert = @"
            INSERT INTO 
                [Country] 
            (
                [Name]
            ) 
            OUTPUT [Inserted].[Id] 
            VALUES (
                @Name
            );";
        private const string SQLUpdateById = @"
            UPDATE 
                [Country] 
            SET 
                [Name]=@Name 
            WHERE 
                [Id]=@Id;";

        #region Fields

        private readonly IDatabase database;

        #endregion

        public CountryDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            // set
            this.database = database;
        }

        #region ICountryDAO Members

        public IEnumerable<Country> GetAll()
        {
            using (var command = CreateGetAllCommand())
            {
                using (var reader = database.ExecuteReader(command))
                {
                    IList<Country> result = new List<Country>();

                    while (reader.Read())
                    {
                        result.Add(new Country((int)reader["Id"], (string)reader["Name"]));
                    }

                    return result;
                }
            }
        }

        public Country GetById(int id)
        {
            using (var command = CreateGetByIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        return new Country((int)reader["Id"], (string)reader["Name"]);
                    }
                    return null;
                }
            }
        }

        public bool Insert(Country country)
        {
            // check parameter
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            // insert
            using (var command = CreateInsertCommand(country.Name))
            {
                var id = database.ExecuteScalar(command) as int?;

                if (id.HasValue)
                {
                    country.Id = id.Value;
                    return true;
                }
                return false;
            }
        }

        public bool Update(Country country)
        {
            // check parameter
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            // update
            using (var command = CreateUpdateByIdCommand(country.Id, country.Name))
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
            var getByIdCommand = database.CreateCommand(SQLGetById);
            database.DefineParameter(getByIdCommand, "Id", DbType.Int32, id);
            return getByIdCommand;
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
