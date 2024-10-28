using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;

namespace UnitTests.Services
{
    /// <summary>
    /// Unit Tests for JsonFileCategoryService class.
    /// </summary>
    public class JsonFileCategoryServiceTests
    {
        /// <summary>
        /// The service instance under test, set up in each test.
        /// </summary>
        private JsonFileCategoryService _categoryService;

        #region TestSetup

        /// <summary>
        /// Sets up the service instance using the TestHelper.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Arrange: Initialize the service instance
            _categoryService = TestHelper.CategoryService;
        }

        #endregion TestSetup

        #region GetAllData Tests

        /// <summary>
        /// Ensures GetAllData returns a non-empty list.
        /// </summary>
        [Test]
        public void GetAllData_Should_Return_NonEmpty_List()
        {
            // Act: Call GetAllData
            var result = _categoryService.GetAllData();

            // Assert: Validate the list is not null and not empty
            ClassicAssert.AreEqual(true, result != null);
            ClassicAssert.AreEqual(true, result.Any());
        }

        #endregion GetAllData Tests

        #region GetCategoryColorById Tests

        /// <summary>
        /// Validates GetCategoryColorById returns the correct color for a valid ID.
        /// </summary>
        [Test]
        public void GetCategoryColorById_ValidId_Should_Return_Correct_Color()
        {
            // Arrange: Get the first category
            var firstCategory = _categoryService.GetAllData().First();
            var validId = firstCategory.Id;

            // Act: Call GetCategoryColorById with valid ID
            var color = _categoryService.GetCategoryColorById(validId);

            // Assert: Validate the returned color
            ClassicAssert.AreEqual(firstCategory.CategoryColor, color);
        }

        ///// <summary>
        ///// Ensures GetCategoryColorById returns null for an invalid ID.
        ///// </summary>
        //[Test]
        //public void GetCategoryColorById_InvalidId_Should_Return_Null()
        //{
        //    // Act: Call GetCategoryColorById with an invalid ID
        //    var color = _categoryService.GetCategoryColorById("invalid-id");

        //    // Assert: Validate the color is null
        //    ClassicAssert.AreEqual(true, color == null);
        //}

        #endregion GetCategoryColorById Tests

        #region UpdateData Tests

        /// <summary>
        /// Validates UpdateData updates the category data correctly.
        /// </summary>
        [Test]
        public void UpdateData_ValidData_Should_Update_And_Return_Data()
        {
            // Arrange: Prepare existing category and new data
            var category = _categoryService.GetAllData().First();
            var updatedData = new CategoryModel
            {
                Id = category.Id,
                Title = "Updated Title",
                Image = "updated-image.png"
            };

            // Act: Update the category with new data
            var result = _categoryService.UpdateData(updatedData);

            // Assert: Validate the data was updated
            ClassicAssert.AreEqual(true, result != null);
            ClassicAssert.AreEqual("Updated Title", result.Title);
            ClassicAssert.AreEqual("updated-image.png", result.Image);
        }

        /// <summary>
        /// Ensures UpdateData returns null for a non-existent category.
        /// </summary>
        [Test]
        public void UpdateData_InvalidId_Should_Return_Null()
        {
            // Arrange: Create data with a non-existent ID
            var invalidData = new CategoryModel
            {
                Id = "non-existent-id",
                Title = "Title",
                Image = "image.png"
            };

            // Act: Attempt to update the non-existent category
            var result = _categoryService.UpdateData(invalidData);

            // Assert: Validate the result is null
            ClassicAssert.AreEqual(true, result == null);
        }

        #endregion UpdateData Tests

        #region CreateData Tests

        /// <summary>
        /// Validates CreateData creates and returns a new category.
        /// </summary>
        [Test]
        public void CreateData_Should_Create_And_Return_New_Category()
        {
            // Act: Create a new category
            var newCategory = _categoryService.CreateData();

            // Assert: Validate the category data
            ClassicAssert.AreEqual(true, newCategory != null);
            ClassicAssert.AreEqual("Enter Title", newCategory.Title);
            ClassicAssert.AreEqual("", newCategory.Image);
        }

        #endregion CreateData Tests

        #region DeleteData Tests

        /// <summary>
        /// Validates DeleteData removes and returns the category for a valid ID.
        /// </summary>
        [Test]
        public void DeleteData_ValidId_Should_Remove_And_Return_Category()
        {
            // Arrange: Create a new category to delete
            var category = _categoryService.CreateData();

            // Act: Delete the category
            var result = _categoryService.DeleteData(category.Id);

            // Assert: Validate the category was removed
            ClassicAssert.AreEqual(category.Id, result.Id);
        }

        /// <summary>
        /// Ensures DeleteData returns null for an invalid ID.
        /// </summary>
        [Test]
        public void DeleteData_InvalidId_Should_Return_Null()
        {
            // Act: Attempt to delete a non-existent category
            var result = _categoryService.DeleteData("invalid-id");

            // Assert: Validate the result is null
            ClassicAssert.AreEqual(true, result == null);
        }

        #endregion DeleteData Tests
    }
}
