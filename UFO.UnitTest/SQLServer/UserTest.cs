namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;

    [TestClass]
    public class UserTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DAOConstructorNullTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new UserDAO(null);
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
            var userDAO = GetDAO();

            var user = UnitTestHelper.GetRandomUser();
            Assert.IsTrue(userDAO.Insert(user));
            var orgId = user.Id;

            Assert.IsTrue(userDAO.Delete(user));
            Assert.IsNull(userDAO.GetById(orgId));
        }

        [TestMethod]
        public void FindByIdTest()
        {
            var userDAO = GetDAO();

            var user = UnitTestHelper.GetRandomUser();
            Assert.IsTrue(userDAO.Insert(user));

            var id = user.Id;
            var user2 = userDAO.GetById(id);
            Assert.AreEqual(user, user2);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var result = GetDAO().GetAll();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetByIdNullTest()
        {
            Assert.IsNull(GetDAO().GetById(-1));
        }

        [TestMethod]
        public void InsertDuplicateTest()
        {
            var userDAO = GetDAO();

            var user = UnitTestHelper.GetRandomUser();
            Assert.IsTrue(userDAO.Insert(user));
            Assert.IsFalse(userDAO.Insert(user));
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
            var userDAO = GetDAO();

            var user = UnitTestHelper.GetRandomUser();
            Assert.IsTrue(userDAO.Insert(user));

            // ReSharper disable once UnusedVariable
            var id = user.Id;
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
            var userDAO = GetDAO();

            var user = UnitTestHelper.GetRandomUser();
            Assert.IsTrue(userDAO.Insert(user));

            user.Username = UnitTestHelper.GetRandomString();
            Assert.IsTrue(userDAO.Update(user));
        }

        private static IUserDAO GetDAO() => DALFactory.CreateUserDAO(UnitTestHelper.GetUnitTestDatabase());
    }
}
