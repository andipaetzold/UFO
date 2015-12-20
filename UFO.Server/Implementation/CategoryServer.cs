namespace UFO.Server.Implementation
{
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class CategoryServer : DatabaseObjectServer<Category>,
                                  ICategoryServer
    {
        internal CategoryServer()
        {
        }

        protected ICategoryDAO GetDAO() => DALFactory.CreateCategoryDAO(Server.GetDatabase());

        protected override IBaseDAO<Category> GetDatabaseObjectDAO() => GetDAO();
    }
}
