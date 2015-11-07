namespace UFO.DAL.Common
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UFO.Domain;

    public interface ICategoryDAO
    {
        bool Delete(Category category);

        ICollection<Category> GetAll();

        Category GetById(int id);

        bool Insert(Category category);

        bool Update(Category category);
    }
}
