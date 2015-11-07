namespace UFO.UnitTest.Domain
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class PerformanceTest
    {
        [TestMethod]
        [ExpectedException(typeof(DatabaseIdException))]
        public void CreateNoIdObjectTest()
        {
            var performance = new Performance(
                DateTime.Now,
                new Artist("name", "imagefilename", "email", "videourl"),
                new Venue("shortname", "name", null, null));

            var id = performance.Id;
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var artist = new Artist("name", "imagefilename", "email", "videourl");
            var venue = new Venue("shortname", "name", null, null);
            var performance = new Performance(13, DateTime.Now, artist, venue);

            Assert.AreEqual(13, performance.Id);
            Assert.AreEqual(artist, performance.Artist);
            Assert.AreEqual(venue, performance.Venue);
        }
    }
}
