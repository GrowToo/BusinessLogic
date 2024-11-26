using NUnit.Framework;
using DAL.Concrete;
using DTO;

namespace DAL.Tests
{
    [TestFixture]
    public class CategoryDalTest
    {
        [Test]
        public void GetAllCategories_ShouldReturnCategories()
        {
            var categoryDal = new CategoryDal();
            var categories = categoryDal.GetAllCategories();
            Assert.IsNotEmpty(categories);
        }
    }
}
