namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class PerformanceDAO : IPerformanceDAO
    {
        private const string SQLGetAll = @"
            SELECT
                [Id],
                [DateTime],
                [Artist_Id],
                [Venue_Id]
            FROM
                [Performance]";
        private const string SQLGetById = @"
            SELECT
                [Id],
                [DateTime],
                [Artist_Id],
                [Venue_Id]
            FROM 
                [Performance]
            WHERE
                [Id] = @Id;";
        private const string SQLInsert = @"
            INSERT INTO 
                [Performance] 
            (
                [DateTime],
                [Artist_Id],
                [Venue_Id]
            ) 
            OUTPUT [Inserted].[Id] 
            VALUES 
            (
                @DateTime, 
                @Artist_Id, 
                @Venue_Id
            );";
        private const string SQLUpdateById = @"
            UPDATE
                [Performance]
            SET
                [DateTime] = @DateTime,
                [Artist_Id] = @Artist_Id,
                [Venue_Id] = @Venue_Id
            WHERE
                [Id] = @Id;";

        #region Fields

        private readonly IDatabase database;
        private readonly string SQLDeleteById = @"
            DELETE FROM
                [Performance]
            WHERE
                [Id] = @Id;";

        #endregion

        public PerformanceDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            // set
            this.database = database;
        }

        #region IPerformanceDAO Members

        public bool Delete(Performance performance)
        {
            // check parameter
            if (performance == null)
            {
                throw new ArgumentNullException(nameof(performance));
            }

            // insert
            using (var command = CreateDeleteByIdCommand(performance.Id))
            {
                if (database.ExecuteNonQuery(command) == 1)
                {
                    performance.DeletedFromDatabase();
                    return true;
                }
                return false;
            }
        }

        public ICollection<Performance> GetAll()
        {
            using (var command = CreateGetAllCommand())
            {
                using (var reader = database.ExecuteReader(command))
                {
                    IList<Performance> result = new List<Performance>();
                    while (reader.Read())
                    {
                        var artistId = (int)reader["Artist_Id"];
                        var artistDAO = new ArtistDAO(database);
                        var artist = artistDAO.GetById(artistId);

                        var venueId = (int)reader["Venue_Id"];
                        var venueDAO = new VenueDAO(database);
                        var venue = venueDAO.GetById(venueId);

                        result.Add(new Performance((int)reader["Id"], (DateTime)reader["DateTime"], artist, venue));
                    }
                    return result;
                }
            }
        }

        public Performance GetById(int id)
        {
            using (var command = CreateGetByIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        var artistId = (int)reader["Artist_Id"];
                        var artistDAO = new ArtistDAO(database);
                        var artist = artistDAO.GetById(artistId);

                        var venueId = (int)reader["Venue_Id"];
                        var venueDAO = new VenueDAO(database);
                        var venue = venueDAO.GetById(venueId);

                        return new Performance((int)reader["Id"], (DateTime)reader["DateTime"], artist, venue);
                    }
                    return null;
                }
            }
        }

        public bool Insert(Performance performance)
        {
            // check parameter
            if (performance == null)
            {
                throw new ArgumentNullException(nameof(performance));
            }

            // insert
            using (var command = CreateInsertCommand(performance.DateTime, performance.Artist.Id, performance.Venue.Id))
            {
                var id = database.ExecuteScalar(command) as int?;

                if (id.HasValue)
                {
                    performance.InsertedInDatabase(id.Value);
                    return true;
                }
                return false;
            }
        }

        public bool Update(Performance performance)
        {
            // check parameter
            if (performance == null)
            {
                throw new ArgumentNullException(nameof(performance));
            }

            // update
            using (
                var command = CreateUpdateByIdCommand(
                    performance.Id,
                    performance.DateTime,
                    performance.Artist.Id,
                    performance.Venue.Id))
            {
                return database.ExecuteNonQuery(command) == 1;
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

        private DbCommand CreateInsertCommand(DateTime dateTime, int artistId, int venueId)
        {
            var insertCommand = database.CreateCommand(SQLInsert);
            database.DefineParameter(insertCommand, "DateTime", DbType.DateTime, dateTime);
            database.DefineParameter(insertCommand, "Artist_Id", DbType.Int32, artistId);
            database.DefineParameter(insertCommand, "Venue_Id", DbType.Int32, venueId);
            return insertCommand;
        }

        private DbCommand CreateUpdateByIdCommand(int id, DateTime dateTime, int artistId, int venueId)
        {
            var updateByIdCommand = database.CreateCommand(SQLUpdateById);
            database.DefineParameter(updateByIdCommand, "Id", DbType.Int32, id);
            database.DefineParameter(updateByIdCommand, "DateTime", DbType.DateTime, dateTime);
            database.DefineParameter(updateByIdCommand, "Artist_Id", DbType.Int32, artistId);
            database.DefineParameter(updateByIdCommand, "Venue_Id", DbType.Int32, venueId);
            return updateByIdCommand;
        }
    }
}
