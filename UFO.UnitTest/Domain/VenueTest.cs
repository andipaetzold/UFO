namespace UFO.UnitTest.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class VenueTest
    {
        [TestMethod]
        public void CreateNoIdObjectTest()
        {
            var venue = new Venue("shortname", "name", null, null);

            Assert.IsFalse(venue.HasId);
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var venue = new Venue(13, "shortname", "name", 123.456m, null);

            Assert.AreEqual(13, venue.Id);
            Assert.AreEqual("shortname", venue.ShortName);
            Assert.AreEqual("name", venue.Name);

            Assert.IsNotNull(venue.Latitude);
            Assert.AreEqual(123.456m, venue.Latitude.Value);
            Assert.IsNull(venue.Longitude);
        }

       
    }
}
