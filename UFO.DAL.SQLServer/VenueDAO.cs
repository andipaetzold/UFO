namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class VenueDAO : IVenueDAO
    {
        private const string SQLGetAll = @"
            SELECT
                [Id],
                [ShortName],
                [Name],
                [Latitude],
                [Longitude]
            FROM
                [Venue];";
        private const string SQLGetById = @"
            SELECT
                [Id],
                [ShortName],
                [Name],
                [Latitude],
                [Longitude]
            FROM
                [Venue]
            WHERE
                [Id]=@Id;";
        private const string SQLInsert = @"
            INSERT INTO
                [Venue]
            (
                [ShortName],
                [Name],
                [Latitude],
                [Longitude]
            )
            OUTPUT [Inserted].[Id]
            VALUES
            (
                @ShortName,
                @Name,
                @Langitude,
                @Longitude
            );";
        private const string SQLUpdateById = @"
            UPDATE
                [Venue]
            SET
                [ShortName] = @ShortName,
                [Name] = @Name,
                [Latitude] = @Latitude,
                [Longitude] = @Longitude
            WHERE
                [Id]=@Id;";

        #region Fields

        private readonly IDatabase database;

        #endregion

        public VenueDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            // set
            this.database = database;
        }

        #region IVenueDAO Members

        public IEnumerable<Venue> GetAll()
        {
            using (var command = CreateGetAllCommand())
            {
                using (var reader = database.ExecuteReader(command))
                {
                    IList<Venue> result = new List<Venue>();
                    while (reader.Read())
                    {
                        result.Add(
                            new Venue(
                                (int)reader["Id"],
                                (string)reader["ShortName"],
                                (string)reader["Name"],
                                (decimal)reader["Latitude"],
                                (decimal)reader["Longitude"]));
                    }
                    return result;
                }
            }
        }

        public Venue GetById(int id)
        {
            using (var command = CreateGetByIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        return new Venue(
                            (int)reader["Id"],
                            (string)reader["ShortName"],
                            (string)reader["Name"],
                            (decimal)reader["Latitude"],
                            (decimal)reader["Longitude"]);
                    }
                    return null;
                }
            }
        }

        public bool Update(Venue venue)
        {
            // check parameter
            if (venue == null)
            {
                throw new ArgumentNullException(nameof(venue));
            }

            // update
            using (
                var command = CreateUpdateByIdCommand(
                    venue.Id,
                    venue.ShortName,
                    venue.Name,
                    venue.Latitude,
                    venue.Longitude))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        public bool Insert(Venue venue)
        {
            // check parameter
            if (venue == null)
            {
                throw new ArgumentNullException(nameof(venue));
            }

            // insert
            using (var command = CreateInsertCommand(venue.ShortName, venue.Name, venue.Latitude, venue.Longitude))
            {
                var id = database.ExecuteScalar(command) as int?;

                if (id.HasValue)
                {
                    venue.Id = id.Value;
                    return true;
                }
                return false;
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

        private DbCommand CreateInsertCommand(string shortName, string name, decimal? latitude, decimal? longitude)
        {
            var insertCommand = database.CreateCommand(SQLInsert);
            database.DefineParameter(insertCommand, "ShortName", DbType.String, shortName);
            database.DefineParameter(insertCommand, "Name", DbType.String, name);
            database.DefineParameter(insertCommand, "Latitude", DbType.Decimal, latitude);
            database.DefineParameter(insertCommand, "Longitude", DbType.Decimal, longitude);
            return insertCommand;
        }

        private DbCommand CreateUpdateByIdCommand(
            int id,
            string shortName,
            string name,
            decimal? latitude,
            decimal? longitude)
        {
            var updateByIdCommand = database.CreateCommand(SQLUpdateById);
            database.DefineParameter(updateByIdCommand, "Id", DbType.Int32, id);
            database.DefineParameter(updateByIdCommand, "ShortName", DbType.String, shortName);
            database.DefineParameter(updateByIdCommand, "Name", DbType.String, name);
            database.DefineParameter(updateByIdCommand, "Latitude", DbType.Decimal, latitude);
            database.DefineParameter(updateByIdCommand, "Longitude", DbType.Decimal, longitude);
            return updateByIdCommand;
        }
    }
}
