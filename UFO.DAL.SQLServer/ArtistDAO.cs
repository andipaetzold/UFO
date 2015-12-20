namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class ArtistDAO : DatabaseObjectDAO<Artist>,
                             IArtistDAO
    {
        public ArtistDAO(IDatabase database)
            : base(database)
        {
        }

        #region IArtistDAO Members

        public IEnumerable<Artist> SelectAllButDeleted()
        {
            return SelectByCondition(new[] { new Tuple<string, string, object>("IsDeleted", "=", false) });
        }

        #endregion
    }
}
