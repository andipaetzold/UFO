namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;
    using UFO.Domain;

    [TestClass]
    public class CountryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DAOConstructorNullTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new CountryDAO(null);
        }

        [TestMethod]
        public void DeleteInvalidTest()
        {
            var country = UnitTestHelper.GetRandomCountry();
            country.InsertedInDatabase(-1);
            Assert.IsFalse(GetDAO().Delete(country));
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
            var countryDAO = GetDAO();

            var country = UnitTestHelper.GetRandomCountry();
            Assert.IsTrue(countryDAO.Insert(country));
            var orgId = country.Id;

            Assert.IsTrue(countryDAO.Delete(country));
            Assert.IsNull(countryDAO.GetById(orgId));

            // ReSharper disable once UnusedVariable
            var r = countryDAO.GetById(country.Id);
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
            var countryDAO = GetDAO();

            var country = UnitTestHelper.GetRandomCountry();
            Assert.IsTrue(countryDAO.Insert(country));

            var id = country.Id;
            var country2 = countryDAO.GetById(id);
            Assert.AreEqual(country, country2);
        }

        private static ICountryDAO GetDAO()
        {
            var database = UnitTestHelper.GetUnitTestDatabase();
            return new CountryDAO(database);
        }

        [TestMethod]
        public void InsertDuplicateTest()
        {
            var country = UnitTestHelper.GetRandomCountry();
            var countryDAO = GetDAO();
            Assert.IsTrue(countryDAO.Insert(country));
            Assert.IsFalse(countryDAO.Insert(country));
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
            var country = UnitTestHelper.GetRandomCountry();
            Assert.IsTrue(GetDAO().Insert(country));

            // ReSharper disable once UnusedVariable
            var id = country.Id;
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
            var countryDAO = GetDAO();

            var country = UnitTestHelper.GetRandomCountry();
            Assert.IsTrue(countryDAO.Insert(country));

            country.Name = UnitTestHelper.GetRandomString();
            Assert.IsTrue(countryDAO.Update(country));
        }
    }
}
