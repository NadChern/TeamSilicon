using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Error
{
    /// <summary>
    /// Unit tests for the ErrorModel class.
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup

        /// <summary>
        /// The page model being tested, an instance of ErrorModel.
        /// </summary>
        public static ErrorModel pageModel;

        /// <summary>
        /// Initializes the test environment for each test case.
        /// Sets up a mock logger and assigns context data.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Create a mock logger for ErrorModel
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            // Instantiate ErrorModel with mock logger and test context/TempData
            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests that OnGet method correctly sets RequestId when an Activity is started.
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange
            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.RequestId, Is.EqualTo(activity.Id));
        }

        /// <summary>
        /// Tests that OnGet method sets RequestId to TraceIdentifier if no Activity is available.
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.RequestId, Is.EqualTo("trace"));
            Assert.That(pageModel.ShowRequestId, Is.EqualTo(true));
        }

        /// <summary>
        /// Tests that ShowRequestId is true if RequestId has a value.
        /// </summary>
        [Test]
        public void OnGet_RequestId_NotEmpty_Should_Set_ShowRequestId_To_True()
        {
            // Arrange
            pageModel.RequestId = "12345";

            // Act
            pageModel.OnGet();

            // Assert
            Assert.That(pageModel.ShowRequestId, Is.EqualTo(true));
        }

        #endregion OnGet
    }
}
