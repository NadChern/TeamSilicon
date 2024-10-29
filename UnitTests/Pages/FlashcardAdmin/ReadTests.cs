using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;

namespace UnitTests.Pages.FlashcardAdmin
{
    /// <summary>
    /// Unit Tests for JsonFlascardService class.
    /// </summary>
    public class ReadTests
    {
        private JsonFileFlashcardService flashcardService;
        private ReadModel readModel;

        #region OnGet
        /// <summary>
        /// Ensures OnGet returns false for an invalid ID.
        /// </summary>
        [Test]
        public void OnGet_Invalid_Id_Should_Return_False()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            readModel = new ReadModel(flashcardService);
            var invalidId = "9999";

            // Act
            readModel.OnGet(invalidId);
            var result = readModel.IsFlashcardLoaded;

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// OnGet should return true for valid ID
        /// </summary>
        [Test]
        public void OnGet_Valid_Id_Should_Return_True()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            readModel = new ReadModel(flashcardService);
            var validId = "1";

            // Act
            readModel.OnGet(validId);
            var result = readModel.IsFlashcardLoaded;

            // Assert
            ClassicAssert.AreEqual(true, result);
        }
        #endregion OnGet

    }

}
