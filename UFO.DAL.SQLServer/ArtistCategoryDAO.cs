namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class ArtistCategoryDAO : IArtistCategoryDAO
    {
        private const string SQLDelete = @"
            DELETE FROM
                [Artist_Category]
            WHERE
                [Artist_Id] = @Artist_Id
            AND
                [Category_Id] = @Category_Id;";
        private const string SQLGetByArtistId = @"
            SELECT
                [Category].[Id],
                [Category].[Name]
            FROM
                [Category]
            JOIN
                [Artist_Category] ON ([Category].[Id] = [Artist_Category].[Category_Id])
            WHERE
                [Artist_Category].[Artist_Id]=@Id;";
        private const string SQLGetByCategoryId = @"
            SELECT
                [Artist].[Id],
                [Artist].[Name],
                [Artist].[ImageFileName],
                [Artist].[Email],
                [Artist].[VideoUrl]
            FROM
                [Artist]
            JOIN
                [Artist_Category] ON ([Artist].[Id] = [Artist_Category].[Artist_Id])
            WHERE
                [Artist_Category].[Category_Id]=@Id;";
        private const string SQLInsert = @"
            INSERT INTO
                [Artist_Category]
            (
                [Artist_Id],
                [Category_Id]
            )
            VALUES
            (
                @Artist_Id,
                @Category_Id
            );";

        #region Fields

        private readonly IDatabase database;

        #endregion

        public ArtistCategoryDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            // set
            this.database = database;
        }

        #region IArtistCategoryDAO Members

        public IEnumerable<Artist> GetByCategoryId(int id)
        {
            using (var command = CreateGetByCategoryIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    IList<Artist> result = new List<Artist>();
                    while (reader.Read())
                    {
                        result.Add(
                            new Artist(
                                (int)reader["Id"],
                                (string)reader["Name"],
                                (reader["ImageFileName"] == DBNull.Value) ? null : (string)reader["ImageFileName"],
                                (reader["Email"] == DBNull.Value) ? null : (string)reader["Email"],
                                (reader["VideoUrl"] == DBNull.Value) ? null : (string)reader["VideoUrl"]));
                    }
                    return result;
                }
            }
        }

        public IEnumerable<Category> GetByArtistId(int id)
        {
            using (var command = CreateGetByArtistIdCommand(id))
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

        public bool Insert(Artist artist, Category category)
        {
            // check parameter
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            // insert
            using (var command = CreateInsertCommand(artist.Id, category.Id))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        public bool Delete(Artist artist, Category category)
        {
            // check parameter
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            // insert
            using (var command = CreateDeleteCommand(artist.Id, category.Id))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        #endregion

        private DbCommand CreateDeleteCommand(int artistId, int categoryId)
        {
            var deleteCommand = database.CreateCommand(SQLDelete);
            database.DefineParameter(deleteCommand, "Artist_Id", DbType.Int32, artistId);
            database.DefineParameter(deleteCommand, "Category_Id", DbType.Int32, categoryId);
            return deleteCommand;
        }

        private DbCommand CreateGetByArtistIdCommand(int id)
        {
            var getByArtistIdCommand = database.CreateCommand(SQLGetByArtistId);
            database.DefineParameter(getByArtistIdCommand, "Id", DbType.Int32, id);
            return getByArtistIdCommand;
        }

        private DbCommand CreateGetByCategoryIdCommand(int id)
        {
            var getByCategoryIdCommand = database.CreateCommand(SQLGetByCategoryId);
            database.DefineParameter(getByCategoryIdCommand, "Id", DbType.Int32, id);
            return getByCategoryIdCommand;
        }

        private DbCommand CreateInsertCommand(int artistId, int categoryId)
        {
            var insertCommand = database.CreateCommand(SQLInsert);
            database.DefineParameter(insertCommand, "Artist_Id", DbType.Int32, artistId);
            database.DefineParameter(insertCommand, "Category_Id", DbType.Int32, categoryId);
            return insertCommand;
        }
    }
}
