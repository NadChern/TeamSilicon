using System.Threading.Tasks;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework.Legacy;


namespace UnitTests.Pages.FlashcardAdmin
{
    /// <summary>
    /// Unit Tests for the CreateModel class.
    /// </summary>
    public class CreateModelTests
    {
        // Mocked instance of  JsonFileFlashcardService
        private Mock<JsonFileFlashcardService> _mockFlashcardService;

        // Instance of CreateModel for testing
        private CreateModel _createModel;

        /// <summary>
        /// Constructor initializes the service and CreateModel for testing.
        /// </summary>
        public CreateModelTests()
        {
            // Initialize mocked service
            _mockFlashcardService = new Mock<JsonFileFlashcardService>(TestHelper.MockWebHostEnvironment.Object);

            // Use mocked service to create CreateModel instance
            _createModel = new CreateModel(_mockFlashcardService.Object);
        }

        #region OnGet

        /// <summary>
        /// Test to verify that OnGet initializes a new FlashcardModel.
        /// </summary>
        [Test]
        public void OnGet_Should_Initialize_New_FlashcardModel()
        {
            // Arrange

            // Act
            var result = _createModel.OnGet();

            // Assert 
            ClassicAssert.AreEqual(true, _createModel.Flashcard != null);
            ClassicAssert.AreEqual(typeof(PageResult), result.GetType());
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Test to verify that OnPost returns to the Create page if the model state is invalid.
        /// </summary>
        [Test]
        public async Task OnPost_Invalid_ModelState_Should_Return_Page_With_Errors()
        {
            // Arrange
            _createModel.Flashcard = new FlashcardModel
            {
                Id = "123e4567-e89b-12d3-a456-426614174000",
                Question = "", // Invalid because Question is required
                Answer = "Sample Answer",
                CategoryId = "OOP",
                DifficultyLevel = 1
            };

            // Simulate model validation error for Question field
            _createModel.ModelState.AddModelError("Flashcard.Question",
                "The Question field is required.");

            // Act
            var result = await _createModel.OnPost();

            // Assert
            ClassicAssert.AreEqual(typeof(PageResult), result.GetType());
        }

        /// <summary>
        /// Test to verify that OnPost redirects to Index when ModelState is valid.
        /// </summary>
        [Test]
        public async Task OnPost_Valid_ModelState_Should_Redirect_To_Index()
        {
            // Arrange 
            _createModel.Flashcard = new FlashcardModel
            {
                Id = "123e4567-e89b-12d3-a456-426614174000",
                Question = "Sample Question",
                Answer = "Sample Answer",
                CategoryId = "OOP",
                DifficultyLevel = 2
            };

            // Ensure ModelState is clear
            _createModel.ModelState.Clear();

            // Act
            var result = await _createModel.OnPost();

            // Assert - Verify that the result is a redirection to the Index page
            ClassicAssert.AreEqual(typeof(RedirectToPageResult), result.GetType());
            var redirectResult = result as RedirectToPageResult;
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", redirectResult.PageName);
        }

        /// <summary>
        /// Test to verify that OnPost returns to the Create page with errors when the URL is invalid.
        /// </summary>
        [Test]
        public async Task OnPost_Invalid_Url_Should_Return_Page_With_Error()
        {
            // Arrange
            // Simulate application's hosting environment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(env => env.WebRootPath).Returns("wwwroot");

            // Mock for JsonFileFlashcardService
            var mockFlashcardService = new Mock<JsonFileFlashcardService>(mockWebHostEnvironment.Object);
            mockFlashcardService.Setup(service => service.ValidateUrlAsync(It.IsAny<string>()))
                .ReturnsAsync(false); 

            // Create an instance of CreateModel and inject mocked FlashcardService
            var createModel = new CreateModel(mockFlashcardService.Object)
            {
                // Initialize Flashcard with test data, including invalid URL
                Flashcard = new FlashcardModel
                {
                    Id = "123e4567-e89b-12d3-a456-426614174000",
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = 1,
                    Url = "https://invalid-url.com" 
                }
            };

            // Initialize ViewData properly
            // ViewData is used to pass error messages back to the view.
            var modelState = new ModelStateDictionary();
            createModel.PageContext = new PageContext
            {
                // Set up ViewData with metadata provider and the model state.
                ViewData = new ViewDataDictionary(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: modelState
                )
            };

            // Act
            var result = await createModel.OnPost();

            // Assert
            ClassicAssert.AreEqual(typeof(PageResult), result.GetType());

            // Validate ViewData["UrlError"] contents
            ClassicAssert.AreEqual(true, createModel.ViewData.ContainsKey("UrlError"));
            ClassicAssert.AreEqual("The provided URL does not exist or cannot be reached. Please use another URL",
                createModel.ViewData["UrlError"]);
        }

        /// <summary>
        /// Test to verify that OnPost proceeds to create a flashcard when the URL is valid.
        /// </summary>
        [Test]
        public async Task OnPost_Valid_Url_Should_Redirect_To_Index()
        {
            // Arrange
            _createModel.Flashcard = new FlashcardModel
            {
                Id = "123e4567-e89b-12d3-a456-426614174000",
                Question = "Sample Question",
                Answer = "Sample Answer",
                CategoryId = "OOP",
                DifficultyLevel = 2,
                Url = "https://valid-url.com" // Valid URL
            };

            // Mock ValidateUrlAsync to return true
            _mockFlashcardService
                .Setup(service => service.ValidateUrlAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            _createModel.ModelState.Clear();

            // Act
            var result = await _createModel.OnPost();

            // Assert
            ClassicAssert.AreEqual(typeof(RedirectToPageResult), result.GetType());
            var redirectResult = result as RedirectToPageResult;
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", redirectResult.PageName);
        }

        /// <summary>
        /// Test to verify that OnPost skips URL validation when the URL is null or empty.
        /// </summary>
        [Test]
        public async Task OnPost_Empty_Url_Should_Skip_Validation_And_Redirect_To_Index()
        {
            // Arrange
            _createModel.Flashcard = new FlashcardModel
            {
                Id = "123e4567-e89b-12d3-a456-426614174000",
                Question = "Sample Question",
                Answer = "Sample Answer",
                CategoryId = "OOP",
                DifficultyLevel = 2,
                Url = null // No URL provided
            };

            _createModel.ModelState.Clear();

            // Act
            var result = await _createModel.OnPost();

            // Assert
            ClassicAssert.AreEqual(typeof(RedirectToPageResult), result.GetType());
            var redirectResult = result as RedirectToPageResult;
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", redirectResult.PageName);

            // Verify that ValidateUrlAsync is never called
            _mockFlashcardService.Verify(service => service.ValidateUrlAsync(It.IsAny<string>()), Times.Never);
        }

        #endregion OnPost
    }
}