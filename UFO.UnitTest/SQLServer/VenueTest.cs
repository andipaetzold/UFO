namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;
    using UFO.Domain;

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
            venue.InsertedInDatabase(-1);
            Assert.IsFalse(GetDAO().Delete(venue));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteNullTest()
        {
            GetDAO().Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseIdException))]
        public void DeleteTest()
        {
            var venueDAO = GetDAO();

            var venue = UnitTestHelper.GetRandomVenue();
            Assert.IsTrue(venueDAO.Insert(venue));
            var orgId = venue.Id;

            Assert.IsTrue(venueDAO.Delete(venue));
            Assert.IsNull(venueDAO.GetById(orgId));

            // ReSharper disable once UnusedVariable
            var r = venueDAO.GetById(venue.Id);
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

        private static IVenueDAO GetDAO()
        {
            var database = UnitTestHelper.GetUnitTestDatabase();
            return new VenueDAO(database);
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

            // ReSharper disable once UnusedVariable
            var id = venue.Id;
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
    }
}
