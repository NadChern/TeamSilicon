using NUnit.Framework;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;

namespace UnitTests.Pages.FlashcardAdmin
{
    /// <summary>
    /// Unit Tests for the DeleteModel class, which manages 
    /// the deletion of flashcards in the FlashcardAdmin page.
    /// </summary>
    public class DeleteTests
    {
        /// <summary>
        /// Service for managing flashcards data.
        /// </summary>
        private JsonFileFlashcardService flashcardService;

        /// <summary>
        /// The DeleteModel page model being tested.
        /// </summary>
        private DeleteModel deleteModel;

        #region Setup

        /// <summary>
        /// Initializes necessary services and page model instances
        /// before each test is executed.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            flashcardService = TestHelper.FlashcardService;
            deleteModel = new DeleteModel(flashcardService);
        }

        #endregion Setup

        #region OnGet

        /// <summary>
        /// Verifies that calling OnGet with an invalid ID redirects to 
        /// the index page, ensuring non-existent flashcards are handled appropriately.
        /// </summary>
        [Test]
        public void OnGet_Invalid_Id_Should_Redirect_To_Index()
        {
            // Arrange
            var invalidId = "00000000-0000-0000-0000-000000000000";

            // Act
            var result = deleteModel.OnGet(invalidId) as RedirectToPageResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", result.PageName);
        }

        /// <summary>
        /// Verifies that calling OnGet with a valid ID successfully 
        /// loads the flashcard data, ensuring correct flashcard retrieval.
        /// </summary>
        [Test]
        public void OnGet_Valid_Id_Should_Load_Flashcard()
        {
            // Arrange
            var validId = "123e4567-e89b-12d3-a456-426614174000";

            // Act
            deleteModel.OnGet(validId);

            // Assert
            ClassicAssert.IsNotNull(deleteModel.Flashcard);
            ClassicAssert.AreEqual(validId, deleteModel.Flashcard.Id);
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Verifies that calling OnPost with a valid ID deletes the flashcard
        /// and redirects to the index page, ensuring successful deletion.
        /// </summary>
        [Test]
        public void OnPost_Valid_Id_Should_Delete_Flashcard_And_Redirect_To_Index()
        {
            // Arrange
            // Mock the IWebHostEnvironment dependency
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            
            // Create a mock for the JsonFileFlashcardService and pass the mocked environment
            var mockFlashcardService = new Mock<JsonFileFlashcardService>(mockEnvironment.Object);

            // Define a valid flashcard ID for the test
            var validId = "123e4567-e89b-12d3-a456-426614174000";

            // Set up the mock to return true for the valid ID
            mockFlashcardService.Setup(s => s.RemoveFlashcard(validId)).Returns(true);

            // Create an instance of the DeleteModel page model
            var deleteModel = new DeleteModel(mockFlashcardService.Object);

            // Act
            var result = deleteModel.OnPost(validId) as RedirectToPageResult;

            // Assert
            ClassicAssert.IsNotNull(result, "The result should not be null.");
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", result.PageName, "The result should redirect to the correct page.");

            // Verify that RemoveFlashcard was called once
            mockFlashcardService.Verify(s => s.RemoveFlashcard(validId), Times.Once);
        }
        
        /// <summary>
        /// Verifies that calling OnPost with an invalid ID does not delete 
        /// any flashcard and returns the current page with an error message.
        /// </summary>
        [Test]
        public void OnPost_Invalid_Id_Should_Return_Page_With_Error()
        {
            // Arrange
            var invalidId = "00000000-0000-0000-0000-000000000000";

            // Act
            var result = deleteModel.OnPost(invalidId) as PageResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.IsTrue(deleteModel.ModelState.ContainsKey(string.Empty));
            ClassicAssert.IsFalse(deleteModel.ModelState.IsValid);
        }

        #endregion OnPost

    }
}