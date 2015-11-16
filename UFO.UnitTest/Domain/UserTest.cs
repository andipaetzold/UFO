namespace UFO.UnitTest.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateNoIdObjectTest()
        {
            var user = new User("username", "password", "email", false);

            Assert.IsFalse(user.HasId);
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
