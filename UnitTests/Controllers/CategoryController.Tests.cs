using NUnit.Framework;
using ContosoCrafts.WebSite.Controllers;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace UnitTests.Controllers
{
    public class CategoryControllerTests
    {
        private JsonFileCategoryService categoryService;

        /// <summary>
        /// Get should return correct properties
        /// </summary>
        [Test]
        public void Get_Should_Return_Correct_Properties()
        {
            // Arrange
            categoryService = TestHelper.CategoryService;
            var controller = new CategoryController(categoryService);

            // Act
            var result = controller.Get().First().Id;

            // Assert
            ClassicAssert.AreEqual("OOP", result);
        }
    }
}
