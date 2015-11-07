namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeclareParameterDuplicateTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();

            var command = db.CreateCommand("SELECT * FROM [A] WHERE [Id] = @Id");
            db.DeclareParameter(command, "Id", DbType.Int32);
            db.DeclareParameter(command, "Id", DbType.Int32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeclareParameterNullTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            db.DeclareParameter(null, "name", DbType.String);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefineParameterNullTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            db.DefineParameter(null, "name", DbType.String, "value");
        }

        [TestMethod]
        public void ExecuteNonQueryInvalidTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            Assert.IsTrue(db.ExecuteNonQuery(new SqlCommand("invalid sql")) == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteNonQueryNullTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            db.ExecuteNonQuery(null);
        }

        [TestMethod]
        public void ExecuteReaderInvalidTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            Assert.IsNull(db.ExecuteReader(new SqlCommand("invalid sql")));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteReaderNullTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            db.ExecuteReader(null);
        }

        [TestMethod]
        public void ExecuteScalarInvalidTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            Assert.IsNull(db.ExecuteScalar(new SqlCommand("invalid sql")));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteScalarNullTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            db.ExecuteScalar(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetParameterBeforeDeclareTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();

            var command = db.CreateCommand("SELECT * FROM [A] WHERE [Id] = @Id");
            db.SetParameter(command, "Id", 42);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetParameterNullTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();
            db.SetParameter(null, "name", "value");
        }

        [TestMethod]
        public void SetParameterTest()
        {
            var db = UnitTestHelper.GetUnitTestDatabase();

            var command = db.CreateCommand("SELECT * FROM [A] WHERE [Id] = @Id");
            db.DeclareParameter(command, "Id", DbType.Int32);
            db.SetParameter(command, "Id", 42);
        }
    }
}
