namespace UFO.UnitTest.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class UserTest
    {
        [TestMethod]
        [ExpectedException(typeof(DatabaseIdException))]
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
    }
}
