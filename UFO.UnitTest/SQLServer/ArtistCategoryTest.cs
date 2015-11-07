namespace UFO.UnitTest.SQLServer
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;

    [TestClass]
    public class ArtistCategoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DAOConstructorNullTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new ArtistCategoryDAO(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteNull1Test()
        {
            GetDAO().Delete(null, UnitTestHelper.GetRandomCategory());
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
            var category1 = UnitTestHelper.GetRandomInsertedCategory();
            var category2 = UnitTestHelper.GetRandomInsertedCategory();
            var category3 = UnitTestHelper.GetRandomInsertedCategory();

            var dao = GetDAO();
            dao.Insert(artist, category1);
            dao.Insert(artist, category2);

            var list = dao.GetByArtist(artist);

            Assert.IsTrue(list.Contains(category1));
            Assert.IsTrue(list.Contains(category2));
            Assert.IsFalse(list.Contains(category3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByCategoryNullTest()
        {
            GetDAO().GetByCategory(null);
        }

        [TestMethod]
        public void GetByCategoryTest()
        {
            var category = UnitTestHelper.GetRandomInsertedCategory();
            var artist1 = UnitTestHelper.GetRandomInsertedArtist();
            var artist2 = UnitTestHelper.GetRandomInsertedArtist();
            var artist3 = UnitTestHelper.GetRandomInsertedArtist();

            var dao = GetDAO();
            dao.Insert(artist1, category);
            dao.Insert(artist2, category);

            var list = dao.GetByCategory(category);

            Assert.IsTrue(list.Contains(artist1));
            Assert.IsTrue(list.Contains(artist2));
            Assert.IsFalse(list.Contains(artist3));
        }

        private static IArtistCategoryDAO GetDAO()
        {
            var database = UnitTestHelper.GetUnitTestDatabase();
            return new ArtistCategoryDAO(database);
        }

        [TestMethod]
        public void InsertDeleteTest()
        {
            var artist = UnitTestHelper.GetRandomInsertedArtist();
            var category = UnitTestHelper.GetRandomInsertedCategory();

            var dao = GetDAO();

            Assert.IsTrue(dao.Insert(artist, category));
            Assert.IsFalse(dao.Insert(artist, category));
            Assert.IsTrue(dao.Delete(artist, category));
            Assert.IsFalse(dao.Delete(artist, category));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNull1Test()
        {
            GetDAO().Insert(null, UnitTestHelper.GetRandomCategory());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNull2Test()
        {
            GetDAO().Insert(UnitTestHelper.GetRandomArtist(), null);
        }
    }
}
