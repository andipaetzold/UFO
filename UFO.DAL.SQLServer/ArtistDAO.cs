namespace UFO.DAL.SQLServer
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class ArtistDAO : DatabaseObjectDAO<Artist>,
                             IArtistDAO
    {
        public ArtistDAO(IDatabase database)
            : base(database)
        {
        }
    }
}
