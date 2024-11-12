using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;

namespace UnitTests.Pages.FlashcardAdmin
{
    /// <summary>
    /// Unit Tests for UpdateModel class.
    /// </summary>
    public class UpdateTests
    {
        private JsonFileFlashcardService flashcardService;
        private UpdateModel updateModel;

        #region OnGet
        /// <summary>
        /// OnGet should return false for invalid id
        /// </summary>
        [Test]
        public void OnGet_Invalid_Id_Should_Return_False()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            updateModel = new UpdateModel(flashcardService);
            var invalidId = "00000000-0000-0000-0000-000000000000";

            // Act
            updateModel.OnGet(invalidId);
            var result = updateModel.IsFlashcardLoaded;

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// OnGet should return true for valid Id
        /// </summary>
        [Test]
        public void OnGet_Valid_Id_Should_Return_True()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            updateModel = new UpdateModel(flashcardService);
            var validId = "4cce8136-84c3-4e69-abfb-51fedae8432b";

            // Act
            updateModel.OnGet(validId);
            var result = updateModel.IsFlashcardLoaded;

            // Assert
            ClassicAssert.AreEqual(true, result);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// OnPost should return false for an invalid flashcard model
        /// </summary>
        [Test]
        public void OnPost_Invalid_Model_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = "123e4567-e89b-12d3-a456-426614174000",
                    Question = "", // Invalid model
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = 1
                }
            };

            // Note: [Required(ErrorMessage = "*Required")] for Question in FlashcardModel.cs
            // It does not automatically execute validation in unit test,
            // so we manually add error to mimic the behavior.
            // Simulate validation error. Question is Required
            updateModel.ModelState.AddModelError("Flashcard.Question", "*Required");

            // Act:
            updateModel.OnPost();
            var result = updateModel.IsFlashcardUpdated;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// OnPost should return true for a valid model
        /// </summary>
        [Test]
        public void OnPost_Valid_Model_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = "123e4567-e89b-12d3-a456-426614174000",
                    Question = "Sample Question", // Valid model
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = 2
                }
            };

            // Act:
            updateModel.OnPost();
            var result = updateModel.IsFlashcardUpdated;

            // Assert:
            ClassicAssert.AreEqual(true, result);
        }

        /// <summary>
        /// Test when OpenCount matches the existing OpenCount, should update successfully.
        /// </summary>
        [Test]
        public void OnPost_OpenCount_Matches_Existing_OpenCount_Should_Return_True()
        {
            // Arrange:
            updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = "e0264da2-8c97-426a-8af2-0fb1bb64c243",
                    Question = "Sample Question Updated",
                    Answer = "Sample Answer Updated",
                    CategoryId = "OOP",
                    DifficultyLevel = 1,
                    OpenCount = 3 // Match the existing OpenCount
                }
            };

            // Act:
            updateModel.OnPost();
            var result = updateModel.IsFlashcardUpdated;           

            // Assert:
            ClassicAssert.AreEqual(true, result); // Flashcard should be updated
        }
        #endregion OnPost

    }
}