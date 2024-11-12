using NUnit.Framework;
using ContosoCrafts.WebSite.Controllers;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace UnitTests.Controllers
{
    public class FlashcardControllerTests
    {
        private JsonFileFlashcardService flashcardService;
        /// <summary>
        /// Get should return correct properties
        /// </summary>
        [Test]
        public void Get_Should_Return_Correct_Properties()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var controller = new FlashcardController(flashcardService);

            // Act
            var result = controller.Get().First().Question;

            // Assert
            ClassicAssert.AreEqual("What are the four pillars of OOP?", result);
        }
    }
}
