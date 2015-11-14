namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class ArtistDAO : IArtistDAO
    {
        private const string SQLDeleteById = @"
            DELETE FROM
                [Artist]
            WHERE 
                [Id] = @Id;";
        private const string SQLGetAll = @"
            SELECT
                [Id],
                [Name],
                [ImageFileName],
                [Email],
                [VideoUrl]
            FROM
                [Artist]";
        private const string SQLGetById = @"
            SELECT
                [Id],
                [Name],
                [ImageFileName],
                [Email],
                [VideoUrl]
            FROM
                [Artist]
            WHERE
                [Id] = @Id;";
        private const string SQLInsert = @"
            INSERT INTO
                [Artist]
            (
                [Name],
                [ImageFileName],
                [Email],
                [VideoUrl]
            )
            OUTPUT
                [Inserted].[Id] 
            VALUES 
            (
                @Name, 
                @ImageFileName, 
                @Email,
                @VideoUrl
            );";
        private const string SQLUpdateById = @"
            UPDATE
                [Artist]
            SET
                [Name] = @Name,
                [ImageFileName] = @ImageFileName,
                [Email] = @Email,
                [VideoUrl] = @VideoUrl
            WHERE 
                [Id] = @Id;";

        #region Fields

        private readonly IDatabase database;

        #endregion

        public ArtistDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            // set
            this.database = database;
        }

        #region IArtistDAO Members

        public bool Delete(Artist artist)
        {
            // check parameter
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            // update
            using (var command = CreateDeleteByIdCommand(artist.Id))
            {
                if (database.ExecuteNonQuery(command) == 1)
                {
                    artist.DeletedFromDatabase();
                    return true;
                }
                return false;
            }
        }

        public ICollection<Artist> GetAll()
        {
            using (var command = CreateGetAllCommand())
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

        public bool Insert(Artist artist)
        {
            // check parameter
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            // insert
            using (
                var command = CreateInsertCommand(
                    artist.Name,
                    artist.Category,
                    artist.Image,
                    artist.Email,
                    artist.VideoUrl))
            {
                var id = database.ExecuteScalar(command) as int?;

                if (id.HasValue)
                {
                    artist.InsertedInDatabase(id.Value);
                    return true;
                }
                return false;
            }
        }

        public bool Update(Artist artist)
        {
            // check parameter
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            // update
            using (
                var command = CreateUpdateByIdCommand(
                    artist.Id,
                    artist.Name,
                    artist.Category,
                    artist.Image,
                    artist.Email,
                    artist.VideoUrl))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }

        public Artist GetById(int id)
        {
            using (var command = CreateGetByIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        // Category
                        Category category = null;

                        // image
                        byte[] image = null;

                        return new Artist(
                            (int)reader["Id"],
                            (string)reader["Name"],
                            category,
                            image,
                            (reader["Email"] == DBNull.Value) ? null : (string)reader["Email"],
                            (reader["VideoUrl"] == DBNull.Value) ? null : (string)reader["VideoUrl"]);
                    }
                    return null;
                }
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

        private DbCommand CreateInsertCommand(
            string name,
            Category category,
            byte[] image,
            string email,
            string videoUrl)
        {
            var insertCommand = database.CreateCommand(SQLInsert);
            database.DefineParameter(insertCommand, "Name", DbType.String, name);
            database.DefineParameter(insertCommand, "Category_Id", DbType.Int32, category.Id);
            database.DefineParameter(insertCommand, "Image", DbType.Binary, image);
            database.DefineParameter(insertCommand, "Email", DbType.String, email);
            database.DefineParameter(insertCommand, "VideoUrl", DbType.String, videoUrl);
            return insertCommand;
        }

        private DbCommand CreateUpdateByIdCommand(
            int id,
            string name,
            Category category,
            byte[] image,
            string email,
            string videoUrl)
        {
            var updateByIdCommand = database.CreateCommand(SQLUpdateById);
            database.DefineParameter(updateByIdCommand, "Id", DbType.Int32, id);
            database.DefineParameter(updateByIdCommand, "Name", DbType.String, name);
            database.DefineParameter(updateByIdCommand, "Category_Id", DbType.Int32, category.Id);
            database.DefineParameter(updateByIdCommand, "Image", DbType.Binary, image);
            database.DefineParameter(updateByIdCommand, "Email", DbType.String, email);
            database.DefineParameter(updateByIdCommand, "VideoUrl", DbType.String, videoUrl);
            return updateByIdCommand;
        }
    }
}
