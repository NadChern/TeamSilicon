using NUnit.Framework;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Pages
{
    /// <summary>
    /// Unit tests for the AboutUsModel class.
    /// </summary>
    public class Privacy
    {
        private PrivacyModel privacyModel;
        private Mock<ILogger<PrivacyModel>> mockLogger;

        #region OnGet
        /// <summary>
        /// Ensures OnGet executes without errors.
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_True()
        {
            // Arrange
            mockLogger = new Mock<ILogger<PrivacyModel>>();
            privacyModel = new PrivacyModel(mockLogger.Object);

            // Act
            privacyModel.OnGet();
            var result = privacyModel.IsLoaded;

            // Assert
            ClassicAssert.AreEqual(true, result);
        }
        #endregion OnGet
    }
}