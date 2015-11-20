namespace UFO.UnitTest.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class ArtistTest
    {
        [TestMethod]
        public void CreateNoIdObjectTest()
        {
            var artist = new Artist("name", null, null, null, "email", "videourl", false);

            Assert.IsFalse(artist.HasId);
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var artist = new Artist(13, "name", null, null, null, "email", "videourl", false);
            Assert.AreEqual(13, artist.Id);
            Assert.AreEqual("name", artist.Name);
            Assert.AreEqual(null, artist.Category);
            Assert.AreEqual(null, artist.Country);
            Assert.AreEqual(null, artist.Image);
            Assert.AreEqual("email", artist.Email);
            Assert.AreEqual("videourl", artist.VideoUrl);
            Assert.AreEqual(false, artist.IsDeleted);
        }
    }
}
