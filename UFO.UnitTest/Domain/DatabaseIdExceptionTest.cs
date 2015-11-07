namespace UFO.UnitTest.Domain
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class DatabaseIdExceptionTest
    {
        [TestMethod]
        public void Constructor1Test()
        {
            var exception = new DatabaseIdException();
            try
            {
                throw exception;
            }
            catch (InvalidOperationException e)
            {
                Assert.AreSame(exception, e);
            }
        }

        [TestMethod]
        public void Constructor2Test()
        {
            var exception = new DatabaseIdException("message");
            try
            {
                throw exception;
            }
            catch (InvalidOperationException e)
            {
                Assert.AreEqual("message", e.Message);
                Assert.AreSame(exception, e);
            }
        }
    }
}
