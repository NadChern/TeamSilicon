using System.Collections.Generic;
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
            // Act
            var result = _createModel.OnGet();

            // Assert - Check if Flashcard is initialized
            ClassicAssert.AreEqual(true, _createModel.Flashcard != null, "Flashcard should be initialized.");
            ClassicAssert.AreEqual(typeof(PageResult), result.GetType(), "Result should be a PageResult.");
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Test to verify that OnPost returns to the Create page if the model state is invalid.
        /// </summary>
        [Test]
        public void OnPost_Invalid_ModelState_Should_Return_Page_With_Errors()
        {
            // Arrange - Set up an invalid flashcard model
            _createModel.Flashcard = new FlashcardModel
            {
                Id = 1,
                Question = "", // Invalid because Question is required
                Answer = "Sample Answer",
                CategoryId = "OOP",
                DifficultyLevel = "Easy"
            };
            
            // Simulate model validation error for Question field
            _createModel.ModelState.AddModelError("Flashcard.Question", "The Question field is required.");

            // Act
            var result = _createModel.OnPost();

            // Assert - Verify it returns the Page result with validation errors
            ClassicAssert.AreEqual(typeof(PageResult), result.GetType());
        }
        
        /// <summary>
        /// Test to verify that OnPost redirects to Index when ModelState is valid.
        /// </summary>
        [Test]
        public void OnPost_Valid_ModelState_Should_Redirect_To_Index()
        {
            // Arrange - Set up a valid flashcard model
            _createModel.Flashcard = new FlashcardModel
            {
                Id = 1,
                Question = "Sample Question",
                Answer = "Sample Answer",
                CategoryId = "OOP",
                DifficultyLevel = "Easy"
            };

            // Ensure ModelState is clear
            _createModel.ModelState.Clear();

            // Act
            var result = _createModel.OnPost();

            // Assert - Verify that the result is a redirection to the Index page
            Assert.That(result, Is.TypeOf<RedirectToPageResult>(), "Result should be a RedirectToPageResult.");
            var redirectResult = result as RedirectToPageResult;
            Assert.That(redirectResult.PageName, Is.EqualTo("/FlashcardAdmin/Index"), "Should redirect to Index page.");
        }
        
        #endregion OnPost
        
        #region Categories and DifficultyLevels

        /// <summary>
        /// Test that Categories list contains the expected options.
        /// </summary>
        [Test]
        public void Categories_Should_Contain_Expected_Options()
        {
            // Arrange - Expected categories
            var expectedCategories = new List<string> { "OOP", "Python", "C#", "C++", "Mobile", "DS" };

            // Act
            var actualCategories = _createModel.Categories;

            // Assert - Verify each category matches expected value
            Assert.That(actualCategories, Is.EqualTo(expectedCategories));
        }

        /// <summary>
        /// Test that DifficultyLevels list contains the expected options.
        /// </summary>
        [Test]
        public void DifficultyLevels_Should_Contain_Expected_Options()
        {
            // Arrange - Expected difficulty levels
            var expectedDifficultyLevels = new List<string> { "Easy", "Medium", "Hard" };

            // Act
            var actualDifficultyLevels = _createModel.DifficultyLevels;

            // Assert - Verify each difficulty level matches expected value
            Assert.That(actualDifficultyLevels, Is.EqualTo(expectedDifficultyLevels));
        }

        #endregion Categories and DifficultyLevels
        
    }
}

