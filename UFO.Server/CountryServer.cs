namespace UFO.Server
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class CountryServer : DatabaseObjectServer<Country>,
                                 IBaseServer<Country>
    {
        internal CountryServer()
        {
        }

        protected ICountryDAO GetDAO() => DALFactory.CreateCountryDAO(Server.GetDatabase());

        protected override IBaseDAO<Country> GetDatabaseObjectDAO() => GetDAO();
    }
}
