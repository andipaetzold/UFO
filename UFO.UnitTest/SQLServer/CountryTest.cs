namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;

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
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteNullTest()
        {
            GetDAO().Delete(null);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var countryDAO = GetDAO();

            var country = UnitTestHelper.GetRandomCountry();
            Assert.IsTrue(countryDAO.Insert(country));
            var orgId = country.Id;

            Assert.IsTrue(countryDAO.Delete(country));
            Assert.IsNull(countryDAO.SelectById(orgId));
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
            var countryDAO = GetDAO();

            var country = UnitTestHelper.GetRandomCountry();
            Assert.IsTrue(countryDAO.Insert(country));

            var id = country.Id;
            var country2 = countryDAO.SelectById(id);
            Assert.AreEqual(country, country2);
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

        private static ICountryDAO GetDAO() => DALFactory.CreateCountryDAO(UnitTestHelper.GetUnitTestDatabase());
    }
}
