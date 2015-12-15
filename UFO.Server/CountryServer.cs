namespace UFO.Server
{
    using System.Configuration;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class CountryServer : DatabaseObjectServer<Country>,
                                 IBaseServer<Country>
    {
        internal CountryServer()
        {
        }

        protected ICountryDAO GetDAO()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var database = DALFactory.CreateDatabase(connectionString);
            return DALFactory.CreateCountryDAO(database);
        }

        protected override IBaseDAO<Country> GetDatabaseObjectDAO()
        {
            return GetDAO();
        }
    }
}
