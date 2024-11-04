using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Pages
{
    /// <summary>
    /// Unit tests for the AboutUsModel class.
    /// </summary>
    public class AboutUs
    {
        private AboutUsModel aboutUsModel;
        private Mock<ILogger<AboutUsModel>> mockLogger;

        #region OnGet
        /// <summary>
        /// Ensures OnGet executes without errors.
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_True()
        {
            // Arrange
            mockLogger = new Mock<ILogger<AboutUsModel>>();
            aboutUsModel = new AboutUsModel(mockLogger.Object);

            // Act
            aboutUsModel.OnGet();
            var result = aboutUsModel.IsLoaded;

            // Assert
            ClassicAssert.AreEqual(true, result);
        }
        #endregion OnGet
    }
}
