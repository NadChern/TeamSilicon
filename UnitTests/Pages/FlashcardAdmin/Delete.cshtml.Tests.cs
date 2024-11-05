using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitTests.Pages.FlashcardAdmin
{
    /// <summary>
    /// Unit Tests for DeleteModel class.
    /// </summary>
    public class DeleteTests
    {
        private JsonFileFlashcardService flashcardService;
        private DeleteModel deleteModel;

        #region Setup
        [SetUp]
        public void Setup()
        {
            flashcardService = TestHelper.FlashcardService;
            deleteModel = new DeleteModel(flashcardService);
        }
        #endregion Setup

        #region OnGet
        /// <summary>
        /// Ensures OnGet redirects to index for an invalid ID.
        /// </summary>
        [Test]
        public void OnGet_Invalid_Id_Should_Redirect_To_Index()
        {
            // Arrange
            var invalidId = 9999;

            // Act
            var result = deleteModel.OnGet(invalidId) as RedirectToPageResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", result.PageName);
        }

        /// <summary>
        /// Ensures OnGet loads the flashcard for a valid ID.
        /// </summary>
        [Test]
        public void OnGet_Valid_Id_Should_Load_Flashcard()
        {
            // Arrange
            var validId = 1;

            // Act
            deleteModel.OnGet(validId);

            // Assert
            ClassicAssert.IsNotNull(deleteModel.Flashcard);
            ClassicAssert.AreEqual(validId, deleteModel.Flashcard.Id);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Ensures OnPost successfully deletes a flashcard with a valid ID and redirects to index.
        /// </summary>
        [Test]
        public void OnPost_Valid_Id_Should_Delete_Flashcard_And_Redirect_To_Index()
        {
            // Arrange
            var validId = 1;

            // Act
            var result = deleteModel.OnPost(validId) as RedirectToPageResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual("/FlashcardAdmin/Index", result.PageName);

            // Verify the flashcard was deleted
            var flashcard = flashcardService.GetById(validId);
            ClassicAssert.IsNull(flashcard);
        }

        /// <summary>
        /// Ensures OnPost with an invalid ID does not delete and returns page with error.
        /// </summary>
        [Test]
        public void OnPost_Invalid_Id_Should_Return_Page_With_Error()
        {
            // Arrange
            var invalidId = 9999;

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
