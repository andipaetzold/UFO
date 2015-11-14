namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class ArtistCountryDAO : IArtistCountryDAO
    {
        private const string SQLDelete = @"
            DELETE FROM
                [Artist_Country]
            WHERE
                [Artist_Id] = @Artist_Id
            AND
                [Country_Id] = @Country_Id;";
        private const string SQLGetByArtistId = @"
            SELECT
                [Country].[Id],
                [Country].[Name]
            FROM
                [Country]
            JOIN
                [Artist_Country] ON ([Country].[Id] = [Artist_Country].[Country_Id])
            WHERE
                [Artist_Country].[Artist_Id]=@Id;";
        private const string SQLGetByCountryId = @"
            SELECT
                [Artist].[Id],
                [Artist].[Name],
                [Artist].[ImageFileName],
                [Artist].[Email],
                [Artist].[VideoUrl]
            FROM
                [Artist]
            JOIN
                [Artist_Country] ON ([Artist].[Id] = [Artist_Country].[Artist_Id])
            WHERE
                [Artist_Country].[Country_Id]=@Id;";
        private const string SQLInsert = @"
            INSERT INTO
                [Artist_Country]
            (
                [Artist_Id],
                [Country_Id]
            )
            VALUES
            (
                @Artist_Id,
                @Country_Id
            );";

        #region Fields

        private readonly IDatabase database;

        #endregion

        public ArtistCountryDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            // set
            this.database = database;
        }

        #region IArtistCountryDAO Members

        public ICollection<Country> GetByArtist(Artist artist)
        {
            // check parameter
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            // get countries
            using (var command = CreateGetByArtistIdCommand(artist.Id))
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

        public bool Insert(Artist artist, Country country)
        {
            // check parameter
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            // insert
            using (var command = CreateInsertCommand(artist.Id, country.Id))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        public bool Delete(Artist artist, Country country)
        {
            // check parameter
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            // insert
            using (var command = CreateDeleteCommand(artist.Id, country.Id))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        public ICollection<Artist> GetByCountry(Country country)
        {
            // check parameter
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            // get artists
            using (var command = CreateGetByountryIdCommand(country.Id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    IList<Artist> result = new List<Artist>();
                    while (reader.Read())
                    {
                        // Category
                        Category category = null;

                        // image
                        byte[] image = null;

                        result.Add(
                            new Artist(
                                (int)reader["Id"],
                                (string)reader["Name"],
                                category,
                                image,
                                (reader["Email"] == DBNull.Value) ? null : (string)reader["Email"],
                                (reader["VideoUrl"] == DBNull.Value) ? null : (string)reader["VideoUrl"]));
                    }
                    return result;
                }
            }
        }

        #endregion

        private DbCommand CreateDeleteCommand(int artistId, int countryId)
        {
            var deleteCommand = database.CreateCommand(SQLDelete);
            database.DefineParameter(deleteCommand, "Artist_Id", DbType.Int32, artistId);
            database.DefineParameter(deleteCommand, "Country_Id", DbType.Int32, countryId);
            return deleteCommand;
        }

        private DbCommand CreateGetByArtistIdCommand(int id)
        {
            var getByArtistIdCommand = database.CreateCommand(SQLGetByArtistId);
            database.DefineParameter(getByArtistIdCommand, "Id", DbType.Int32, id);
            return getByArtistIdCommand;
        }

        private DbCommand CreateGetByountryIdCommand(int id)
        {
            var getByCountryIdCommand = database.CreateCommand(SQLGetByCountryId);
            database.DefineParameter(getByCountryIdCommand, "Id", DbType.Int32, id);
            return getByCountryIdCommand;
        }

        private DbCommand CreateInsertCommand(int artistId, int countryId)
        {
            var insertCommand = database.CreateCommand(SQLInsert);
            database.DefineParameter(insertCommand, "Artist_Id", DbType.Int32, artistId);
            database.DefineParameter(insertCommand, "Country_Id", DbType.Int32, countryId);
            return insertCommand;
        }
    }
}
