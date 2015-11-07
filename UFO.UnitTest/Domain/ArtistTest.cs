﻿namespace UFO.UnitTest.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class ArtistTest
    {
        [TestMethod]
        [ExpectedException(typeof(DatabaseIdException))]
        public void CreateNoIdObjectTest()
        {
            var artist = new Artist("name", "filename", "email", "videourl");

            var id = artist.Id;
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var artist = new Artist(13, "name", "filename", "email", "videourl");

            Assert.AreEqual(13, artist.Id);
            Assert.AreEqual("name", artist.Name);
            Assert.AreEqual("filename", artist.ImageFileName);
            Assert.AreEqual("email", artist.Email);
            Assert.AreEqual("videourl", artist.VideoUrl);
        }
    }
}