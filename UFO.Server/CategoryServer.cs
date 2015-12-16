namespace UFO.Server
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class CategoryServer : DatabaseObjectServer<Category>,
                                  IBaseServer<Category>
    {
        internal CategoryServer()
        {
        }

        protected ICategoryDAO GetDAO() => DALFactory.CreateCategoryDAO(Server.GetDatabase());

        protected override IBaseDAO<Category> GetDatabaseObjectDAO() => GetDAO();
    }
}
