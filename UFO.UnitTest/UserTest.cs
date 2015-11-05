namespace UFO.UnitTest
{
    using System;
    using System.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;
    using UFO.Domain;

    [TestClass]
    public class UserTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateNoIdObjectTest()
        {
            var user = new User("username", "password", "email", false);

            Assert.AreEqual("username", user.Username);
            Assert.AreEqual("password", user.Password);
            Assert.AreEqual("email", user.Email);
            Assert.AreEqual(false, user.IsAdmin);

            var id = user.Id;
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var user = new User(13, "username", "password", "email", false);

            Assert.AreEqual(13, user.Id);
            Assert.AreEqual("username", user.Username);
            Assert.AreEqual("password", user.Password);
            Assert.AreEqual("email", user.Email);
            Assert.AreEqual(false, user.IsAdmin);
        }

        [TestMethod]
        public void FindByIdUserTest()
        {
            var user = new User(Guid.NewGuid().ToString().GetHashCode().ToString("X"), "password", "email", false);

            var userDAO = GetDAO();

            Assert.IsTrue(userDAO.Insert(user));

            var id = user.Id;
            var user2 = userDAO.GetById(id);
            Assert.AreEqual(user, user2);
        }

        private static IUserDAO GetDAO()
        {
            IDatabase database = new Database(ConfigurationManager.ConnectionStrings["UnitTest"].ConnectionString);
            return new UserDAO(database);
        }

        [TestMethod]
        public void InsertDuplicateUserTest()
        {
            var user = new User(Guid.NewGuid().ToString().GetHashCode().ToString("X"), "password", "email", false);

            var userDAO = GetDAO();

            Assert.IsTrue(userDAO.Insert(user));
            Assert.IsFalse(userDAO.Insert(user));
        }

        [TestMethod]
        public void InsertUserTest()
        {
            var user = new User(Guid.NewGuid().ToString().GetHashCode().ToString("X"), "password", "email", false);

            var userDAO = GetDAO();

            Assert.IsTrue(userDAO.Insert(user));

            var id = user.Id;
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var user = new User(Guid.NewGuid().ToString().GetHashCode().ToString("X"), "password", "email", false);

            var userDAO = GetDAO();

            Assert.IsTrue(userDAO.Insert(user));

            user.Username = Guid.NewGuid().ToString().GetHashCode().ToString("X");
            Assert.IsTrue(userDAO.Update(user));
        }
    }
}
