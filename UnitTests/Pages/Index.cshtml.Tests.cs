using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages
{
    /// <summary>
    /// Unit tests for the IndexModel class.
    /// </summary>
    public class IndexTests
    {
        #region TestSetup
        
        /// <summary>
        /// The page model being tested, an instance of IndexModel.
        /// </summary>
        public static IndexModel pageModel;

        /// <summary>
        /// Initializes the test environment for each test case.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {   
            // Create a mock logger for IndexModel
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();

            // Instantiate IndexModel with the mock logger and test category service
            pageModel = new IndexModel(MockLoggerDirect, TestHelper.CategoryService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        
        /// <summary>
        /// Tests that the OnGet method populates Categories with data and returns a valid ModelState.
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.Categories.ToList().Any(), Is.EqualTo(true));
        }

        #endregion OnGet
    }
}