namespace UFO.UnitTest.Domain
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class PerformanceTest
    {
        [TestMethod]
        public void CreateNoIdObjectTest()
        {
            var performance = new Performance(
                DateTime.Now,
                new Artist("name", null, null, null, "email", "videourl", false),
                new Venue("shortname", "name", null, null));

            Assert.IsFalse(performance.HasId);
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var artist = new Artist("name", null, null, null, "email", "videourl", false);
            var venue = new Venue("shortname", "name", null, null);
            var performance = new Performance(13, DateTime.Now, artist, venue);

            Assert.AreEqual(13, performance.Id);
            Assert.AreEqual(artist, performance.Artist);
            Assert.AreEqual(venue, performance.Venue);
        }
    }
}
