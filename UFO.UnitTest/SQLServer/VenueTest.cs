namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;

    [TestClass]
    public class VenueTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DAOConstructorNullTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new VenueDAO(null);
        }

        [TestMethod]
        public void DeleteInvalidTest()
        {
            var venue = UnitTestHelper.GetRandomVenue();
            Assert.IsFalse(GetDAO().Delete(venue));
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
            var venueDAO = GetDAO();

            var venue = UnitTestHelper.GetRandomVenue();
            Assert.IsTrue(venueDAO.Insert(venue));
            var orgId = venue.Id;

            Assert.IsTrue(venueDAO.Delete(venue));
            Assert.IsNull(venueDAO.GetById(orgId));
        }

        [TestMethod]
        public void GetAllTest()
        {
            var result = GetDAO().GetAll();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetByIdNullTest()
        {
            Assert.IsNull(GetDAO().GetById(-1));
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var venueDAO = GetDAO();

            var venue = UnitTestHelper.GetRandomVenue();
            Assert.IsTrue(venueDAO.Insert(venue));

            var id = venue.Id;
            var venue2 = venueDAO.GetById(id);
            Assert.AreEqual(venue, venue2);
        }

        [TestMethod]
        public void InsertDuplicateTest()
        {
            var venueDAO = GetDAO();

            var venue = UnitTestHelper.GetRandomVenue();
            Assert.IsTrue(venueDAO.Insert(venue));
            Assert.IsFalse(venueDAO.Insert(venue));
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
            var venue = UnitTestHelper.GetRandomVenue();
            Assert.IsTrue(GetDAO().Insert(venue));
            Assert.IsTrue(venue.HasId);
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
            var venueDAO = GetDAO();

            var venue = UnitTestHelper.GetRandomVenue();
            Assert.IsTrue(venueDAO.Insert(venue));

            venue.Name = UnitTestHelper.GetRandomString();
            Assert.IsTrue(venueDAO.Update(venue));
        }

        private static IVenueDAO GetDAO() => DALFactory.CreateVenueDAO(UnitTestHelper.GetUnitTestDatabase());
    }
}
