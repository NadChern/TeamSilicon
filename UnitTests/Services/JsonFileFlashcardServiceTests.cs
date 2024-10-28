using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;

namespace UnitTests.Services
{
    /// <summary>
    /// Unit Tests for JsonFlascardService class.
    /// </summary>
    public class JsonFileFlashcardServiceTests
    {
        private JsonFileFlashcardService flashcardService;

        #region GetAllData
        [Test]
        public void GetAllData_Valid_Should_Return_NonEmpty_List()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;

            // Act
            var result = flashcardService.GetAllData();

            // Assert
            ClassicAssert.AreEqual(false, result == null);
            ClassicAssert.AreEqual(true, result.Any());
        }
        #endregion GetAllData

        #region GetById
        [Test]
        public void GetById_Valid_Id_Should_Return_Flashcard()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validId = "1";

            // Act
            var result = flashcardService.GetById(validId);

            // Assert
            ClassicAssert.AreEqual(false, result == null);
            ClassicAssert.AreEqual(validId, result.Id);
        }

        [Test]
        public void GetById_Invalid_Id_Should_Return_Null()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var invalidId = "9999"; //ID doesn't exist

            // Act
            var result = flashcardService.GetById(invalidId);

            // Assert
            ClassicAssert.AreEqual(true, result == null);
        }
        #endregion GetById

        #region UpdateFlashcard
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

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        [Test]
        public void UpdateFlashcard_Valid_Id_Should_Return_True()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validFlashcard = new FlashcardModel
            {
                Id = "1", // valid id
                Question = "Updated Question",
                Answer = "Updated Answer",
                CategoryId = "Python",
                DifficultyLevel = "Medium"
            };

            // Act
            var result = flashcardService.UpdateFlashcard(validFlashcard);

            // Assert
            ClassicAssert.AreEqual(true, result);
        }

        #endregion UpdateFlashcard
    }
}
