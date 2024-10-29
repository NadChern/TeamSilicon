using System;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using ContosoCrafts.WebSite.Services;
using System.Linq;

namespace UnitTests.Pages.FlashcardAdmin
{
   /// <summary>
    /// Unit Tests for the IndexModel class.
    /// </summary>
    public class IndexModelTests
    {
        // Mocked instance of the JsonFileFlashcardService
        private readonly JsonFileFlashcardService _flashcardService;

        // Instance of IndexModel for testing
        private readonly IndexModel _indexModel;

        /// <summary>
        /// Constructor initializes the service and IndexModel for testing.
        /// </summary>
        public IndexModelTests()
        {
            // Retrieve a mock service from TestHelper and initialize the model
            _flashcardService = TestHelper.FlashcardService;
            _indexModel = new IndexModel(_flashcardService);
        }

        /// <summary>
        /// Test that OnGet returns all flashcards successfully.
        /// </summary>
        [Test]
        public void OnGet_Should_Return_All_Flashcards()
        {
            // Arrange
            
            // Act
            _indexModel.OnGet();

            // Assert
            Assert.That(_indexModel.Flashcards, Is.Not.Null);
            Assert.That(_indexModel.Flashcards, Is.Not.Empty);
        }

        /// <summary>
        /// Test that OnGet retrieves the expected data from the service.
        /// </summary>
        [Test]
        public void OnGet_Should_Match_Expected_Data()
        {
            // Arrange
            var expectedFlashcards = _flashcardService.GetAllData().ToList();

            // Act
            _indexModel.OnGet();
            var result = _indexModel.Flashcards.ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(expectedFlashcards.Count));

            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(expectedFlashcards[i].Id));
                Assert.That(result[i].CategoryId, Is.EqualTo(expectedFlashcards[i].CategoryId));
                Assert.That(result[i].Question, Is.EqualTo(expectedFlashcards[i].Question));
                Assert.That(result[i].Answer, Is.EqualTo(expectedFlashcards[i].Answer));
                Assert.That(result[i].DifficultyLevel, Is.EqualTo(expectedFlashcards[i].DifficultyLevel));
                Assert.That(result[i].Url, Is.EqualTo(expectedFlashcards[i].Url));
            }
        }

        /// <summary>
        /// Test that the model throws an exception when the service is not available.
        /// </summary>
        [Test]
        public void Constructor_When_Service_Is_Null_Should_Throw_ArgumentNullException()
        {
            // Assert: Verify ArgumentNullException is thrown
            Assert.Throws<ArgumentNullException>(() => { new IndexModel(null); });
        }

    }
}