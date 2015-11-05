namespace UFO.UnitTest
{
    using System;
    using System.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;
    using UFO.Domain;

    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateNoIdObjectTest()
        {
            var category = new Category("name");

            Assert.AreEqual("name", category.Name);

            var id = category.Id;
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var category = new Category(13, "name");

            Assert.AreEqual(13, category.Id);
            Assert.AreEqual("name", category.Name);
        }

        [TestMethod]
        public void FindByIdCategoryTest()
        {
            var category = new Category(Guid.NewGuid().ToString().GetHashCode().ToString("X"));

            var userDAO = GetDAO();

            Assert.IsTrue(userDAO.Insert(category));

            var id = category.Id;
            var category2 = userDAO.GetById(id);
            Assert.AreEqual(category, category2);
        }

        private static ICategoryDAO GetDAO()
        {
            IDatabase database = new Database(ConfigurationManager.ConnectionStrings["UnitTest"].ConnectionString);
            return new CategoryDAO(database);
        }

        [TestMethod]
        public void InsertCategoryTest()
        {
            var category = new Category(Guid.NewGuid().ToString().GetHashCode().ToString("X"));

            var userDAO = GetDAO();

            Assert.IsTrue(userDAO.Insert(category));

            var id = category.Id;
        }

        [TestMethod]
        public void InsertDuplicateCategoryTest()
        {
            var category = new Category(Guid.NewGuid().ToString().GetHashCode().ToString("X"));

            var userDAO = GetDAO();

            Assert.IsTrue(userDAO.Insert(category));
            Assert.IsFalse(userDAO.Insert(category));
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var category = new Category(Guid.NewGuid().ToString().GetHashCode().ToString("X"));

            var categoryDAO = GetDAO();

            Assert.IsTrue(categoryDAO.Insert(category));

            category.Name = Guid.NewGuid().ToString().GetHashCode().ToString("X");
            Assert.IsTrue(categoryDAO.Update(category));
        }
    }
}
