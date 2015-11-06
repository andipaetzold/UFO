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
                [Artist] 
            SET 
                [Name]=@Name 
            WHERE 
                [Id]=@Id;";

        #region Fields

        private readonly IDatabase database;

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

        public IEnumerable<Performance> GetAll()
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
                    performance.Id = id.Value;
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

        private DbCommand CreateInsertCommand(DateTime dateTime, int artist, int venue)
        {
            var insertCommand = database.CreateCommand(SQLInsert);
            database.DefineParameter(insertCommand, "Name", DbType.DateTime, dateTime);
            database.DefineParameter(insertCommand, "Artist", DbType.Int32, artist);
            database.DefineParameter(insertCommand, "Venue", DbType.Int32, venue);
            return insertCommand;
        }

        private DbCommand CreateUpdateByIdCommand(int id, DateTime dateTime, int artist, int venue)
        {
            var updateByIdCommand = database.CreateCommand(SQLUpdateById);
            database.DefineParameter(updateByIdCommand, "Id", DbType.Int32, id);
            database.DefineParameter(updateByIdCommand, "Name", DbType.DateTime, dateTime);
            database.DefineParameter(updateByIdCommand, "Artist", DbType.Int32, artist);
            database.DefineParameter(updateByIdCommand, "Venue", DbType.Int32, venue);
            return updateByIdCommand;
        }
    }
}
