namespace UFO.UnitTest.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UFO.Domain;

    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        public void CreateNoIdObjectTest()
        {
            var category = new Category("name");

            Assert.IsFalse(category.HasId);
        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var category = new Category(13, "name");

            Assert.AreEqual(13, category.Id);
            Assert.AreEqual("name", category.Name);
        }
    }
}
