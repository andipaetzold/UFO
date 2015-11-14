namespace UFO.DAL.SQLServer
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class CategoryDAO : DatabaseObjectDAO<Category>,
                               ICategoryDAO
    {
        public CategoryDAO(IDatabase database)
            : base(database)
        {
        }
    }
}
