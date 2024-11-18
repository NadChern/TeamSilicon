using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace UnitTests.Pages.FlashcardAdmin
{
    /// <summary>
    /// Unit Tests for UpdateModel class.
    /// </summary>
    public class UpdateTests
    {
        // Service responsible for managing flashcards 
        private JsonFileFlashcardService flashcardService;

        // Instance of UpdateModel for testing the update functionality
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

            // Invalid Id
            var invalidId = "00000000-0000-0000-0000-000000000000";

            // Act
            updateModel.OnGet(invalidId);

            // Check whether flashcard was loaded successfully
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

            // Valid ID for testing
            var validId = "4cce8136-84c3-4e69-abfb-51fedae8432b";

            // Act
            updateModel.OnGet(validId);

            // Check whether flashcard was loaded successfully
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
            // Arrange
            flashcardService = TestHelper.FlashcardService;

            // Create UpdateModel instance with mocked service
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

            // Simulate a validation error for the required Question field
            updateModel.ModelState.AddModelError("Flashcard.Question",
                "*Required");

            // Act
            updateModel.OnPost();

            // Check if flashcard was updated
            var result = updateModel.IsFlashcardUpdated;

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// OnPost should return true for a valid model
        /// </summary>
        [Test]
        public void OnPost_Valid_Model_Should_Return_True()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;

            // Create UpdateModel instance with mocked service
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

            // Act
            updateModel.OnPost();

            // Check if the flashcard was updated
            var result = updateModel.IsFlashcardUpdated;

            // Assert
            ClassicAssert.AreEqual(true, result);
        }

        /// <summary>
        /// Test when OpenCount matches the existing OpenCount, should update successfully.
        /// </summary>
        [Test]
        public void OnPost_OpenCount_Matches_Existing_OpenCount_Should_Return_True()
        {
            // Arrange
            updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = "742fcdc1-f9ab-4f51-bb91-9d79f95a6ccf",
                    Question = "Sample Question Updated",
                    Answer = "Sample Answer Updated",
                    CategoryId = "Python",
                    DifficultyLevel = 1,
                    OpenCount = 1 // Match the existing OpenCount
                }
            };

            // Act
            updateModel.OnPost();

            // Check if the flashcard was updated
            var result = updateModel.IsFlashcardUpdated;

            // Assert
            ClassicAssert.AreEqual(true, result);
        }

        /// <summary>
        /// Test when invalid OpenCount entered, should return false.
        /// </summary>
        [Test]
        public void OnPost_Invalid_OpenCount_Should_Return_False()
        {
            // Arrange
            updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = "e0264da2-8c97-426a-8af2-0fb1bb64c243",
                    Question = "Sample Question Updated",
                    Answer = "Sample Answer Updated",
                    CategoryId = "OOP",
                    DifficultyLevel = 1,
                    OpenCount = 5555 // invalid OpenCount
                }
            };

            // Act
            updateModel.OnPost();

            // Check if the flashcard was updated
            var result = updateModel.IsFlashcardUpdated;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// Test to verify that OnPost returns false and sets the appropriate
        /// model error when the URL is invalid.
        /// </summary>
        [Test]
        public async Task OnPost_Invalid_Url_Should_Return_False_And_Set_Error()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;

            // Mock service
            var mockFlashcardService = new Mock<JsonFileFlashcardService>(TestHelper.MockWebHostEnvironment.Object);

            // Mock ValidateUrlAsync to simulate an invalid URL
            mockFlashcardService
                .Setup(service => service.ValidateUrlAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Create UpdateModel with mock service
            var updateModel = new UpdateModel(mockFlashcardService.Object)
            {
                Flashcard = new FlashcardModel
                {
                    Id = "123e4567-e89b-12d3-a456-426614174000",
                    Question = "Sample Question", // Valid question
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = 1,
                    Url = "https://invalid-url.com" // Invalid URL
                }
            };

            // Initialize ModelState and ViewData
            updateModel.PageContext = new PageContext
            {
                ViewData = new ViewDataDictionary(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary()
                )
            };

            // Act
            // Call OnPost asynchronously
            var result = await updateModel.OnPost();

            // Assert
            ClassicAssert.AreEqual(typeof(PageResult), result.GetType());
            ClassicAssert.AreEqual(false, updateModel.IsFlashcardUpdated);
            ClassicAssert.AreEqual(true, updateModel.ModelState.ContainsKey("Flashcard.Url"));
            ClassicAssert.AreEqual("The provided URL does not exist or cannot be reached. Please use another URL",
                updateModel.ModelState["Flashcard.Url"].Errors.First().ErrorMessage);
        }

        /// <summary>
        /// Test to verify that OnPost proceeds successfully when the URL is valid.
        /// </summary>
        [Test]
        public async Task OnPost_Valid_Url_Should_Return_True_And_Update_Flashcard()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            
            // Mock service
            var mockFlashcardService = new Mock<JsonFileFlashcardService>(TestHelper.MockWebHostEnvironment.Object);

            // Mock ValidateUrlAsync to simulate a valid URL
            mockFlashcardService
                .Setup(service => service.ValidateUrlAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Create UpdateModel with mock service
            var updateModel = new UpdateModel(mockFlashcardService.Object)
            {
                Flashcard = new FlashcardModel
                {
                    Id = "123e4567-e89b-12d3-a456-426614174000",
                    Question = "Sample Question", // Valid question
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = 1,
                    Url = "https://valid-url.com" // Valid URL
                }
            };

            // Initialize ModelState
            updateModel.ModelState.Clear();

            // Act
            // Call OnPost asynchronously
            var result = await updateModel.OnPost();

            // Assert
            ClassicAssert.AreEqual(typeof(RedirectToPageResult), result.GetType());
            
            // Cast the result to RedirectToPageResult to access its properties
            var redirectResult = result as RedirectToPageResult;
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", redirectResult.PageName);
            ClassicAssert.AreEqual(true, updateModel.IsFlashcardUpdated);
        }

        /// <summary>
        /// Test to verify that OnPost skips URL validation if no URL is provided.
        /// </summary>
        [Test]
        public async Task OnPost_Empty_Url_Should_Skip_Validation_And_Update_Flashcard()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            
            // Mock service
            var mockFlashcardService = new Mock<JsonFileFlashcardService>(TestHelper.MockWebHostEnvironment.Object);

            // Create UpdateModel with mock service
            var updateModel = new UpdateModel(mockFlashcardService.Object)
            {
                Flashcard = new FlashcardModel
                {
                    Id = "123e4567-e89b-12d3-a456-426614174000",
                    Question = "Sample Question", // Valid question
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = 1,
                    Url = null // No URL provided
                }
            };

            // Initialize ModelState
            updateModel.ModelState.Clear();

            // Act
            // Call OnPost asynchronously
            var result = await updateModel.OnPost();

            // Asser
            ClassicAssert.AreEqual(typeof(RedirectToPageResult), result.GetType());
            
            // Cast the result to RedirectToPageResult to access its properties
            var redirectResult = result as RedirectToPageResult;
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", redirectResult.PageName);
            ClassicAssert.AreEqual(true, updateModel.IsFlashcardUpdated);

            // Verify that ValidateUrlAsync is not called
            mockFlashcardService.Verify(service => service.ValidateUrlAsync(It.IsAny<string>()), Times.Never);
        }

        #endregion OnPost
    }
}