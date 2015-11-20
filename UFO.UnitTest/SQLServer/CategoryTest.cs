namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;

    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DAOConstructorNullTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new CategoryDAO(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteNullTest()
        {
            GetDAO().Delete(null);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var categoryDAO = GetDAO();

            var category = UnitTestHelper.GetRandomCategory();
            Assert.IsTrue(categoryDAO.Insert(category));
            var orgId = category.Id;

            Assert.IsTrue(categoryDAO.Delete(category));
            Assert.IsNull(categoryDAO.SelectById(orgId));
        }

        [TestMethod]
        public void SelectAllTest()
        {
            var result = GetDAO().SelectAll();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void SelectByIdNullTest()
        {
            Assert.IsNull(GetDAO().SelectById(-1));
        }

        [TestMethod]
        public void SelectByIdTest()
        {
            var userDAO = GetDAO();

            var category = UnitTestHelper.GetRandomCategory();
            Assert.IsTrue(userDAO.Insert(category));

            var id = category.Id;
            var category2 = userDAO.SelectById(id);
            Assert.AreEqual(category, category2);
        }

        [TestMethod]
        public void InsertDuplicateTest()
        {
            var categoryDAO = GetDAO();

            var category = UnitTestHelper.GetRandomCategory();
            Assert.IsTrue(categoryDAO.Insert(category));
            Assert.IsFalse(categoryDAO.Insert(category));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNullTest()
        {
            GetDAO().Insert(null);
        }

        [TestMethod]
        public void InsertTest()
        {
            var category = UnitTestHelper.GetRandomCategory();
            Assert.IsTrue(GetDAO().Insert(category));

            var id = category.Id;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNullTest()
        {
            GetDAO().Update(null);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var categoryDAO = GetDAO();

            var category = UnitTestHelper.GetRandomCategory();
            Assert.IsTrue(categoryDAO.Insert(category));

            category.Name = UnitTestHelper.GetRandomString();
            Assert.IsTrue(categoryDAO.Update(category));
        }

        private static ICategoryDAO GetDAO() => DALFactory.CreateCategoryDAO(UnitTestHelper.GetUnitTestDatabase());
    }
}
