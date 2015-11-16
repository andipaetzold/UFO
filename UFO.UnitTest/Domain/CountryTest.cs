namespace UFO.UnitTest.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class CountryTest
    {
        [TestMethod]
        public void CreateNoIdObjectTest()
        {
            var country = new Country("name");

            Assert.IsFalse(country.HasId);
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
