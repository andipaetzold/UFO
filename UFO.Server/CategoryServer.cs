namespace UFO.Server
{
    using System.Configuration;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class CategoryServer : DatabaseObjectServer<Category>,
                                  IBaseServer<Category>
    {
        internal CategoryServer()
        {
        }

        protected ICategoryDAO GetDAO()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            var database = DALFactory.CreateDatabase(connectionString);
            return DALFactory.CreateCategoryDAO(database);
        }

        protected override IBaseDAO<Category> GetDatabaseObjectDAO()
        {
            return GetDAO();
        }
    }
}
