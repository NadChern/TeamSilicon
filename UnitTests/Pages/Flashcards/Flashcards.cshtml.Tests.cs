using System.Linq;
using ContosoCrafts.WebSite.Pages.Flashcards;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework;

namespace UnitTests.Pages.Flashcards
{
    /// <summary>
    /// Unit Tests for the FlashcardsModel class.
    /// </summary>
    public class FlashcardsModelTests
    {
         /// <summary>
        /// Instance of FlashcardsModel.
        /// </summary>
        private FlashcardsModel flashcardsModel;

        /// <summary>
        /// Instance of JsonFileFlashcardService.
        /// </summary>
        private JsonFileFlashcardService flashcardService;

        #region TestSetup

        /// <summary>
        /// Setup method to initialize FlashcardsModel and service.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            flashcardService = TestHelper.FlashcardService;
            flashcardsModel = new FlashcardsModel(flashcardService);
        }

        #endregion TestSetup

        #region ConstructorTests

        /// <summary>
        /// Test to ensure that the constructor initializes FlashcardService and Flashcards correctly.
        /// </summary>
        [Test]
        public void Constructor_Should_Initialize_FlashcardService_And_Empty_Flashcards()
        {
            // Arrange & Act
            var model = new FlashcardsModel(flashcardService);

            // Assert
            Assert.That(model.FlashcardService, Is.Not.Null);
            Assert.That(model.Flashcards, Is.Empty);
        }

        #endregion ConstructorTests

        #region CategoryIdTests

        /// <summary>
        /// Test to ensure that CategoryId is null upon initialization.
        /// </summary>
        [Test]
        public void CategoryId_Should_Be_Null_On_Initialization()
        {
            // Arrange & Act
            var model = new FlashcardsModel(flashcardService);

            // Assert
            Assert.That(model.CategoryId, Is.Null);
        }

        /// <summary>
        /// Test to verify that CategoryId can assign and retrieve values correctly.
        /// </summary>
        [Test]
        public void CategoryId_Should_Assign_And_Retrieve_Value()
        {
            // Arrange
            var categoryId = "OOP";
            var model = new FlashcardsModel(flashcardService);

            // Act
            model.CategoryId = categoryId;

            // Assert
            Assert.That(model.CategoryId, Is.EqualTo(categoryId));
        }

        /// <summary>
        /// Test to simulate query string binding by assigning a value to CategoryId.
        /// </summary>
        [Test]
        public void BindProperty_Should_Assign_CategoryId_From_QueryString()
        {
            // Arrange
            var queryStringValue = "Algorithms";

            // Act
            flashcardsModel.CategoryId = queryStringValue;

            // Assert
            Assert.That(flashcardsModel.CategoryId, Is.EqualTo(queryStringValue));
        }

        #endregion CategoryIdTests

        #region OnGet

        /// <summary>
        /// Test to verify that OnGet filters flashcards correctly by category ID.
        /// </summary>
        [Test]
        public void OnGet_Valid_CategoryId_Should_Return_Filtered_Flashcards()
        {
            // Arrange
            var validCategory = "OOP";
            var expectedFlashcards = flashcardService.GetAllData()
                .Where(f => f.CategoryId.Equals(validCategory, 
                    System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Act
            flashcardsModel.OnGet(validCategory);
            var result = flashcardsModel.Flashcards.ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(expectedFlashcards.Count));
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(expectedFlashcards[i].Id));
                Assert.That(result[i].CategoryId, Is.EqualTo(expectedFlashcards[i].CategoryId));
                Assert.That(result[i].Question, Is.EqualTo(expectedFlashcards[i].Question));
                Assert.That(result[i].Answer, Is.EqualTo(expectedFlashcards[i].Answer));
            }
        }

        /// <summary>
        /// Test to verify that OnGet returns an empty collection for an invalid category ID.
        /// </summary>
        [Test]
        public void OnGet_Invalid_CategoryId_Should_Return_Empty_Collection()
        {
            // Arrange
            var invalidCategory = "NonExistentCategory";

            // Act
            flashcardsModel.OnGet(invalidCategory);
            var result = flashcardsModel.Flashcards;

            // Assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// Test to verify that OnGet returns all flashcards when no category ID is provided.
        /// </summary>
        [Test]
        public void OnGet_No_CategoryId_Should_Return_All_Flashcards()
        {
            // Arrange
            var allFlashcards = flashcardService.GetAllData().ToList();

            // Act
            flashcardsModel.OnGet(null);
            var result = flashcardsModel.Flashcards.ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(allFlashcards.Count));
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(allFlashcards[i].Id));
                Assert.That(result[i].CategoryId, Is.EqualTo(allFlashcards[i].CategoryId));
                Assert.That(result[i].Question, Is.EqualTo(allFlashcards[i].Question));
                Assert.That(result[i].Answer, Is.EqualTo(allFlashcards[i].Answer));
            }
        }

        /// <summary>
        /// Test to verify that OnGet returns all flashcards when the category ID is empty.
        /// </summary>
        [Test]
        public void OnGet_Empty_CategoryId_Should_Return_All_Flashcards()
        {
            // Arrange
            var allFlashcards = flashcardService.GetAllData().ToList();

            // Act
            flashcardsModel.OnGet(string.Empty);
            var result = flashcardsModel.Flashcards.ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(allFlashcards.Count));
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(allFlashcards[i].Id));
                Assert.That(result[i].CategoryId, Is.EqualTo(allFlashcards[i].CategoryId));
                Assert.That(result[i].Question, Is.EqualTo(allFlashcards[i].Question));
                Assert.That(result[i].Answer, Is.EqualTo(allFlashcards[i].Answer));
            }
        }

        /// <summary>
        /// Test to verify that the OnGet method correctly assigns the category ID to the CategoryId property.
        /// </summary>
        [Test]
        public void OnGet_Should_Assign_CategoryId_Property()
        {
            // Arrange
            var category = "Python";

            // Act
            flashcardsModel.OnGet(category);

            // Assert
            Assert.That(flashcardsModel.CategoryId, Is.EqualTo(category));
        }

        #endregion OnGet

    }
}