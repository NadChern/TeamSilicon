using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using ContosoCrafts.WebSite.Services;
using System.Linq;
using ContosoCrafts.WebSite.Models;
using Moq;
using System.Collections.Generic;

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

        #region OnGet Tests

        /// <summary>
        /// Verifies that OnGet initializes Flashcards property with data.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_NonNull_Flashcards()
        {
            // Arrange

            // Act
            _indexModel.OnGet();

            // Assert
            Assert.That(_indexModel.Flashcards, Is.Not.Null);
        }

        /// <summary>
        /// Verifies that OnGet initializes Flashcards property with a non-empty collection.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_NonEmpty_Flashcards()
        {
            // Arrange

            // Act
            _indexModel.OnGet();

            // Assert
            Assert.That(_indexModel.Flashcards, Is.Not.Empty);
        }

        /// <summary>
        /// Verifies that OnGet retrieves the correct count of flashcards.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_Correct_Count()
        {
            // Arrange
            var expectedCount = _flashcardService.GetAllData().Count();

            // Act
            _indexModel.OnGet();
            var resultCount = _indexModel.Flashcards.Count();

            // Assert
            Assert.That(resultCount, Is.EqualTo(expectedCount));
        }

        /// <summary>
        /// Verifies that OnGet retrieves the correct IDs for flashcards.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_Matching_Flashcard_Ids()
        {
            // Arrange
            var expectedFlashcards = _flashcardService.GetAllData().ToList();

            // Act
            _indexModel.OnGet();
            var resultFlashcards = _indexModel.Flashcards.ToList();

            // Assert
            for (int i = 0; i < resultFlashcards.Count; i++)
            {
                Assert.That(resultFlashcards[i].Id, Is.EqualTo(expectedFlashcards[i].Id));
            }
        }

        /// <summary>
        /// Verifies that OnGet retrieves the correct Category IDs for flashcards.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_Matching_Category_Ids()
        {
            // Arrange
            var expectedFlashcards = _flashcardService.GetAllData().ToList();

            // Act
            _indexModel.OnGet();
            var resultFlashcards = _indexModel.Flashcards.ToList();

            // Assert
            for (int i = 0; i < resultFlashcards.Count; i++)
            {
                Assert.That(resultFlashcards[i].CategoryId, Is.EqualTo(expectedFlashcards[i].CategoryId));
            }
        }

        /// <summary>
        /// Verifies that OnGet retrieves the correct Questions for flashcards.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_Matching_Questions()
        {
            // Arrange
            var expectedFlashcards = _flashcardService.GetAllData().ToList();

            // Act
            _indexModel.OnGet();
            var resultFlashcards = _indexModel.Flashcards.ToList();

            // Assert
            for (int i = 0; i < resultFlashcards.Count; i++)
            {
                Assert.That(resultFlashcards[i].Question, Is.EqualTo(expectedFlashcards[i].Question));
            }
        }

        /// <summary>
        /// Verifies that OnGet retrieves the correct Answers for flashcards.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_Matching_Answers()
        {
            // Arrange
            var expectedFlashcards = _flashcardService.GetAllData().ToList();

            // Act
            _indexModel.OnGet();
            var resultFlashcards = _indexModel.Flashcards.ToList();

            // Assert
            for (int i = 0; i < resultFlashcards.Count; i++)
            {
                Assert.That(resultFlashcards[i].Answer, Is.EqualTo(expectedFlashcards[i].Answer));
            }
        }

        /// <summary>
        /// Verifies that OnGet retrieves the correct Difficulty Levels for flashcards.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_Matching_Difficulty_Levels()
        {
            // Arrange
            var expectedFlashcards = _flashcardService.GetAllData().ToList();

            // Act
            _indexModel.OnGet();
            var resultFlashcards = _indexModel.Flashcards.ToList();

            // Assert
            for (int i = 0; i < resultFlashcards.Count; i++)
            {
                Assert.That(resultFlashcards[i].DifficultyLevel, Is.EqualTo(expectedFlashcards[i].DifficultyLevel));
            }
        }

        /// <summary>
        /// Verifies that OnGet retrieves the correct URLs for flashcards.
        /// </summary>
        [Test]
        public void OnGet_Valid_Test_Should_Return_Matching_Urls()
        {
            // Arrange
            var expectedFlashcards = _flashcardService.GetAllData().ToList();

            // Act
            _indexModel.OnGet();
            var resultFlashcards = _indexModel.Flashcards.ToList();

            // Assert
            for (int i = 0; i < resultFlashcards.Count; i++)
            {
                Assert.That(resultFlashcards[i].Url, Is.EqualTo(expectedFlashcards[i].Url));
            }
        }
        /// <summary>
        /// Verifies that searching for a specific term returns flashcards with that term in it
        /// </summary>
        [Test]
        public void OnGet_Valid_SearchTerm_Should_Return_Correct_Flashcards()
        {
            // Arrange
            var indexModel = new IndexModel(TestHelper.FlashcardService);

            // Set the search term to "python"
            indexModel.SearchTerm = "python";

            // Act
            indexModel.OnGet();
            var resultCount = indexModel.Flashcards.Count();

            // Assert
            Assert.That(resultCount, Is.EqualTo(5)); // Expecting 5 flashcards with "python"
        }

        #endregion OnGet Tests

    }
}