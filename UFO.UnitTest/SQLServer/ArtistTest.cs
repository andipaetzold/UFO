namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;

    [TestClass]
    public class ArtistTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DAOConstructorNullTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new ArtistDAO(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteNullTest()
        {
            GetDAO().Delete(null);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var artistDAO = GetDAO();

            var artist = UnitTestHelper.GetRandomArtist();
            Assert.IsTrue(artistDAO.Insert(artist));
            var orgId = artist.Id;

            Assert.IsTrue(artistDAO.Delete(artist));
            Assert.IsNull(artistDAO.SelectById(orgId));
            Assert.IsFalse(artist.HasId);
        }

        [TestMethod]
        public void SelectAllTest()
        {
            var result = GetDAO().SelectAll();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void SelectByIdNullTest()
        {
            Assert.IsNull(GetDAO().SelectById(-1));
        }

        [TestMethod]
        public void SelectByIdTest()
        {
            var artistDAO = GetDAO();

            var artist = UnitTestHelper.GetRandomArtist();
            Assert.IsTrue(artistDAO.Insert(artist));

            var id = artist.Id;
            var artist2 = artistDAO.SelectById(id);
            Assert.AreEqual(artist, artist2);
        }

        [TestMethod]
        public void InsertDuplicateTest()
        {
            var artistDAO = GetDAO();

            var artist = UnitTestHelper.GetRandomArtist();
            Assert.IsTrue(artistDAO.Insert(artist));
            Assert.IsFalse(artistDAO.Insert(artist));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNullTest()
        {
            GetDAO().Insert(null);
        }

        [TestMethod]
        public void InsertTest()
        {
            var artist = UnitTestHelper.GetRandomArtist();
            Assert.IsTrue(GetDAO().Insert(artist));

            // ReSharper disable once UnusedVariable
            var id = artist.Id;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNullTest()
        {
            GetDAO().Update(null);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var artistDAO = GetDAO();

            var artist = UnitTestHelper.GetRandomArtist();
            Assert.IsTrue(artistDAO.Insert(artist));

            artist.Name = UnitTestHelper.GetRandomString();
            Assert.IsTrue(artistDAO.Update(artist));
        }

        private static IArtistDAO GetDAO() => DALFactory.CreateArtistDAO(UnitTestHelper.GetUnitTestDatabase());
    }
}
