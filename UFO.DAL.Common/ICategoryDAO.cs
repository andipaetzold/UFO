namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using UFO.Domain;

    public interface ICategoryDAO
    {
        IEnumerable<Category> GetAll();

        Category GetById(int id);

        bool Insert(Category category);

        bool Update(Category category);
    }
}
