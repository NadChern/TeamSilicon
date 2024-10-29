using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;

namespace UnitTests.Services
{
    /// <summary>
    /// Unit Tests for the JsonFileFlashcardService class.
    /// </summary>
    public class JsonFileFlashcardServiceTests
    {
        /// <summary>
        /// Instance of JsonFileFlashcardService.
        /// </summary>
        private JsonFileFlashcardService flashcardService;

        #region GetAllData
        /// <summary>
        /// Test to verify that GetAllData method returns a non-empty list.
        /// </summary>
        [Test]
        public void GetAllData_Valid_Should_Return_NonEmpty_List()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;

            // Act
            var result = flashcardService.GetAllData();

            // Assert - Fast fail if result is null
            ClassicAssert.AreEqual(false, result == null);

            // Assert - Check if the result contains data
            ClassicAssert.AreEqual(true, result.Any());
        }
        #endregion GetAllData

        #region GetById
        /// <summary>
        /// Test to verify that GetById returns a flashcard for a valid ID.
        /// </summary>
        [Test]
        public void GetById_Valid_Id_Should_Return_Flashcard()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validId = "1"; // Set valid flashcard ID

            // Act
            var result = flashcardService.GetById(validId);

            // Assert - Fast fail if result is null
            ClassicAssert.AreEqual(false, result == null);

            // Assert - Verify that the returned flashcard matches the expected ID
            ClassicAssert.AreEqual(validId, result.Id);
        }

        /// <summary>
        /// Test to verify that GetById returns null for an invalid ID.
        /// </summary>
        [Test]
        public void GetById_Invalid_Id_Should_Return_Null()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var invalidId = "9999"; // Non-existing flashcard ID

            // Act
            var result = flashcardService.GetById(invalidId);

            // Assert - Fast fail, result should be null
            ClassicAssert.AreEqual(true, result == null);
        }
        #endregion GetById

        #region UpdateFlashcard
        /// <summary>
        /// Test to verify that UpdateFlashcard returns false for an invalid ID.
        /// </summary>
        [Test]
        public void UpdateFlashcard_Invalid_Id_Should_Return_False()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var nonExistentFlashcard = new FlashcardModel
            {
                Id = "invalid_id",
                Question = "Question",
                Answer = "Answer",
                CategoryId = "Python",
                DifficultyLevel = "Easy"
            };

            // Act
            var result = flashcardService.UpdateFlashcard(nonExistentFlashcard);

            // Assert - Fast fail, result should be false for invalid flashcard ID
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// Test to verify that UpdateFlashcard returns true for a valid ID.
        /// </summary>
        [Test]
        public void UpdateFlashcard_Valid_Id_Should_Return_True()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validFlashcard = new FlashcardModel
            {
                Id = "1", // valid flashcard ID
                Question = "Updated Question",
                Answer = "Updated Answer",
                CategoryId = "Python",
                DifficultyLevel = "Medium"
            };

            // Act
            var result = flashcardService.UpdateFlashcard(validFlashcard);

            // Assert - Fast fail if update fails for valid flashcard
            ClassicAssert.AreEqual(true, result);
        }
        #endregion UpdateFlashcard

        #region GetCountByCategoryId
        /// <summary>
        /// Test to verify that GetCountByCategoryId returns the correct count for a valid category ID.
        /// </summary>
        [Test]
        public void GetCountByCategoryId_Valid_Category_Should_Return_Correct_Count()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validCategoryId = "Python"; // Example valid category ID

            // Act
            var result = flashcardService.GetCountByCategoryId(validCategoryId);

            // Assert - Fast fail if count is incorrect
            ClassicAssert.AreEqual(false, result == 0);
        }

        /// <summary>
        /// Test to verify that GetCountByCategoryId returns 0 for an invalid category ID.
        /// </summary>
        [Test]
        public void GetCountByCategoryId_Invalid_Category_Should_Return_Zero()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var invalidCategoryId = "InvalidCategory"; // Non-existing category ID

            // Act
            var result = flashcardService.GetCountByCategoryId(invalidCategoryId);

            // Assert - Fast fail, count should be zero for invalid category ID
            ClassicAssert.AreEqual(true, result == 0);
        }
        #endregion GetCountByCategoryId
    }
}
