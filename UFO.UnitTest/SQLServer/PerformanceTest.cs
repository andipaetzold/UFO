namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;
    using UFO.Domain;

    [TestClass]
    public class PerformanceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DAOConstructorNullTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new PerformanceDAO(null);
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
            var performanceDAO = GetDAO();

            var performance = UnitTestHelper.GetRandomPerformance();
            Assert.IsTrue(performanceDAO.Insert(performance));
            var orgId = performance.Id;

            Assert.IsTrue(performanceDAO.Delete(performance));
            Assert.IsNull(performanceDAO.GetById(orgId));
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
            var performanceDAO = GetDAO();

            var performance = UnitTestHelper.GetRandomPerformance();
            Assert.IsTrue(performanceDAO.Insert(performance));

            var id = performance.Id;
            var performance2 = performanceDAO.GetById(id);

            Assert.AreEqual(performance, performance2);
        }

        private static IPerformanceDAO GetDAO()
        {
            var database = UnitTestHelper.GetUnitTestDatabase();
            return new PerformanceDAO(database);
        }

        [TestMethod]
        public void InsertDuplicateTest()
        {
            var performanceDAO = GetDAO();

            var performance = UnitTestHelper.GetRandomPerformance();

            Assert.IsTrue(performanceDAO.Insert(performance));
            Assert.IsFalse(performanceDAO.Insert(performance));
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
            var performance = UnitTestHelper.GetRandomPerformance();
            Assert.IsTrue(GetDAO().Insert(performance));

            // ReSharper disable once UnusedVariable
            var id = performance.Id;
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
            var performanceDAO = GetDAO();

            var performance = UnitTestHelper.GetRandomPerformance();
            Assert.IsTrue(performanceDAO.Insert(performance));

            performance.DateTime = UnitTestHelper.GetRandomDateTime();
            Assert.IsTrue(performanceDAO.Update(performance));
        }
    }
}
