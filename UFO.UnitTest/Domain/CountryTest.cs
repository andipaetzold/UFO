namespace UFO.UnitTest.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class CountryTest
    {
        [TestMethod]
        [ExpectedException(typeof(DatabaseIdException))]
        public void CreateNoIdObjectTest()
        {
            var country = new Country("name");

            var id = country.Id;
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var country = new Country(13, "name");

            Assert.AreEqual(13, country.Id);
            Assert.AreEqual("name", country.Name);
        }
    }
}
