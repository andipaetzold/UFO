namespace UFO.Server.Implementation
{
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class CountryServer : DatabaseObjectServer<Country>,
                                 ICountryServer,
                                 ICountryServerAsync
    {
        internal CountryServer()
        {
        }

        protected ICountryDAO GetDAO() => DALFactory.CreateCountryDAO(Server.GetDatabase());

        protected override IBaseDAO<Country> GetDatabaseObjectDAO() => GetDAO();
    }
}
