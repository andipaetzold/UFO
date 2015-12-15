namespace UFO.Server
{
    using System.Configuration;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class ArtistServer : DatabaseObjectServer<Artist>,
                                IBaseServer<Artist>
    {
        internal ArtistServer()
        {
        }

        protected IBaseDAO<Artist> GetDAO()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var database = DALFactory.CreateDatabase(connectionString);
            return DALFactory.CreateArtistDAO(database);
        }

        protected override IBaseDAO<Artist> GetDatabaseObjectDAO()
        {
            return GetDAO();
        }
    }
}
