using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Services
{
    public class JsonFileCategoryServiceTests
    {
        private JsonFileCategoryService _categoryService;

        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
            // Use the CategoryService initialized through TestHelper
            _categoryService = TestHelper.CategoryService;
        }

        #endregion TestSetup

        #region GetAllData Tests

        [Test]
        public void GetAllData_ShouldReturnNonEmptyList()
        {
            // Act
            var result = _categoryService.GetAllData();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Any(), Is.True);
        }

        #endregion GetAllData Tests

        #region GetCategoryColorById Tests

        [Test]
        public void GetCategoryColorById_ValidId_ShouldReturnCorrectColor()
        {
            // Arrange
            var firstCategory = _categoryService.GetAllData().First();
            var validId = firstCategory.Id;

            // Act
            var color = _categoryService.GetCategoryColorById(validId);

            // Assert
            Assert.That(color, Is.EqualTo(firstCategory.CategoryColor));
        }

        //[Test]
        //public void GetCategoryColorById_InvalidId_ShouldReturnNull()
        //{
        //    // Act
        //    var color = _categoryService.GetCategoryColorById("non-existent-id");

        //    // Assert
        //    Assert.That(color, Is.Null);
        //}

        #endregion GetCategoryColorById Tests

        #region UpdateData Tests

        [Test]
        public void UpdateData_ValidData_ShouldUpdateAndReturnData()
        {
            // Arrange
            var category = _categoryService.GetAllData().First();
            var updatedData = new CategoryModel
            {
                Id = category.Id,
                Title = "Updated Title",
                Image = "updated-image.png"
            };

            // Act
            var result = _categoryService.UpdateData(updatedData);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Updated Title"));
            Assert.That(result.Image, Is.EqualTo("updated-image.png"));
        }

        [Test]
        public void UpdateData_InvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidData = new CategoryModel
            {
                Id = "non-existent-id",
                Title = "New Title",
                Image = "new-image.png"
            };

            // Act
            var result = _categoryService.UpdateData(invalidData);

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion UpdateData Tests

        #region CreateData Tests

        [Test]
        public void CreateData_ShouldCreateAndReturnNewCategory()
        {
            // Act
            var newCategory = _categoryService.CreateData();

            // Assert
            Assert.That(newCategory, Is.Not.Null);
            Assert.That(newCategory.Title, Is.EqualTo("Enter Title"));
            Assert.That(newCategory.Image, Is.EqualTo(""));
        }

        #endregion CreateData Tests

        #region DeleteData Tests

        [Test]
        public void DeleteData_ValidId_ShouldRemoveAndReturnCategory()
        {
            // Arrange
            var category = _categoryService.CreateData();

            // Act
            var result = _categoryService.DeleteData(category.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(category.Id));
        }

        [Test]
        public void DeleteData_InvalidId_ShouldReturnNull()
        {
            // Act
            var result = _categoryService.DeleteData("non-existent-id");

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion DeleteData Tests
    }
}
