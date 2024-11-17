using System.Threading.Tasks;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NUnit.Framework.Legacy;

namespace UnitTests.Pages.FlashcardAdmin
{
    /// <summary>
    /// Unit Tests for the CreateModel class.
    /// </summary>
    public class CreateModelTests
    {
        // Mocked instance of the JsonFileFlashcardService
        private readonly JsonFileFlashcardService _flashcardService;

        // Instance of CreateModel for testing
        private readonly CreateModel _createModel;

        /// <summary>
        /// Constructor initializes the service and CreateModel for testing.
        /// </summary>
        public CreateModelTests()
        {
            _flashcardService = TestHelper.FlashcardService;
            _createModel = new CreateModel(_flashcardService);
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

            // Assert - Check if Flashcard is initialized
            ClassicAssert.AreEqual(true, _createModel.Flashcard != null);
            ClassicAssert.AreEqual(typeof(PageResult), result.GetType());
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Test to verify that OnPost returns to the Create page if the model state is invalid.
        /// </summary>
        [Test]
        public void OnPost_Invalid_Model_State_Should_Return_Page_With_Errors()
        {
            // Arrange - Set up an invalid flashcard model
            _createModel.Flashcard = new FlashcardModel
            {
                Id = "123e4567-e89b-12d3-a456-426614174000",
                Question = "", // Invalid because Question is required
                Answer = "Sample Answer",
                CategoryId = "OOP",
                DifficultyLevel = 1
            };
            
            // Simulate model validation error for Question field
            _createModel.ModelState.AddModelError("Flashcard.Question", "The Question field is required.");

            // Act
            var result = _createModel.OnPost();

            // Assert 
            ClassicAssert.AreEqual(typeof(PageResult), result.GetType());
        }
        
        /// <summary>
        /// Test to verify that OnPost redirects to Index when ModelState is valid.
        /// </summary>
        [Test]
        public async Task OnPost_Valid_ModelState_Should_Redirect_To_Index()
        {
            // Arrange - Set up a valid flashcard model
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
            ClassicAssert.AreEqual(typeof(RedirectToPageResult), result.GetType(), "Result should be a RedirectToPageResult.");
            var redirectResult = result as RedirectToPageResult;
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", redirectResult.PageName, "Should redirect to the Index page.");
        }
        
        #endregion OnPost
    
    }
}