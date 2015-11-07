namespace UFO.UnitTest.SQLServer
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;

    [TestClass]
    public class ArtistCountryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DAOConstructorNullTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new ArtistCountryDAO(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteNull1Test()
        {
            GetDAO().Delete(null, UnitTestHelper.GetRandomCountry());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteNull2Test()
        {
            GetDAO().Delete(UnitTestHelper.GetRandomArtist(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByArtistNullTest()
        {
            GetDAO().GetByArtist(null);
        }

        [TestMethod]
        public void GetByArtistTest()
        {
            var artist = UnitTestHelper.GetRandomInsertedArtist();
            var country1 = UnitTestHelper.GetRandomInsertedCountry();
            var country2 = UnitTestHelper.GetRandomInsertedCountry();
            var country3 = UnitTestHelper.GetRandomInsertedCountry();

            var dao = GetDAO();
            dao.Insert(artist, country1);
            dao.Insert(artist, country2);

            var list = dao.GetByArtist(artist);

            Assert.IsTrue(list.Contains(country1));
            Assert.IsTrue(list.Contains(country2));
            Assert.IsFalse(list.Contains(country3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByCountryNullTest()
        {
            GetDAO().GetByCountry(null);
        }

        [TestMethod]
        public void GetByCountryTest()
        {
            var country = UnitTestHelper.GetRandomInsertedCountry();
            var artist1 = UnitTestHelper.GetRandomInsertedArtist();
            var artist2 = UnitTestHelper.GetRandomInsertedArtist();
            var artist3 = UnitTestHelper.GetRandomInsertedArtist();

            var dao = GetDAO();
            dao.Insert(artist1, country);
            dao.Insert(artist2, country);

            var list = dao.GetByCountry(country);

            Assert.IsTrue(list.Contains(artist1));
            Assert.IsTrue(list.Contains(artist2));
            Assert.IsFalse(list.Contains(artist3));
        }

        private static IArtistCountryDAO GetDAO()
        {
            var database = UnitTestHelper.GetUnitTestDatabase();
            return new ArtistCountryDAO(database);
        }

        [TestMethod]
        public void InsertDeleteTest()
        {
            var artist = UnitTestHelper.GetRandomInsertedArtist();
            var country = UnitTestHelper.GetRandomInsertedCountry();

            var dao = GetDAO();

            Assert.IsTrue(dao.Insert(artist, country));
            Assert.IsFalse(dao.Insert(artist, country));
            Assert.IsTrue(dao.Delete(artist, country));
            Assert.IsFalse(dao.Delete(artist, country));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNull1Test()
        {
            GetDAO().Insert(null, UnitTestHelper.GetRandomCountry());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNull2Test()
        {
            GetDAO().Insert(UnitTestHelper.GetRandomArtist(), null);
        }
    }
}
