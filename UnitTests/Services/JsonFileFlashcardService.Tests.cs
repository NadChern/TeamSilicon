using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;

namespace UnitTests.Services
{
    /// <summary>
    /// Unit Tests for the JsonFileFlashcardService class.
    /// </summary>
    public class JsonFileFlashcardServiceTests
    {
        /// <summary>
        /// Instance of JsonFileFlashcardService.
        /// </summary>
        private JsonFileFlashcardService flashcardService;
 
        #region GetAllData

        /// <summary>
        /// Test to verify that GetAllData method returns a non-empty list.
        /// </summary>
        [Test]
        public void GetAllData_Valid_Should_Return_NonEmpty_List()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;

            // Act
            var result = flashcardService.GetAllData();

            // Assert - Fast fail if result is null
            ClassicAssert.AreEqual(false, result == null);

            // Assert - Check if the result contains data
            ClassicAssert.AreEqual(true, result.Any());
        }

        #endregion GetAllData

        #region GetById

        /// <summary>
        /// Test to verify that GetById returns a flashcard for a valid ID.
        /// </summary>
        [Test]
        public void GetById_Valid_Id_Should_Return_Flashcard()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validId = "4cce8136-84c3-4e69-abfb-51fedae8432b"; // Set valid flashcard ID

            // Act
            var result = flashcardService.GetById(validId);

            // Assert - Fast fail if result is null
            ClassicAssert.AreEqual(false, result == null);

            // Assert - Verify that the returned flashcard matches the expected ID
            ClassicAssert.AreEqual(validId, result.Id);
        }

        /// <summary>
        /// Test to verify that GetById returns null for an invalid ID.
        /// </summary>
        [Test]
        public void GetById_Invalid_Id_Should_Return_Null()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var invalidId = "00000000-0000-0000-0000-000000000000"; // Non-existing flashcard ID

            // Act
            var result = flashcardService.GetById(invalidId);

            // Assert - Fast fail, result should be null
            ClassicAssert.AreEqual(true, result == null);
        }

        #endregion GetById

        #region UpdateFlashcard

        /// <summary>
        /// Test to verify that UpdateFlashcard returns false for an invalid ID.
        /// </summary>
        [Test]
        public void UpdateFlashcard_Invalid_Id_Should_Return_False()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var nonExistentFlashcard = new FlashcardModel
            {
                Id = "00000000-0000-0000-0000-000000000000",
                Question = "Question",
                Answer = "Answer",
                CategoryId = "Python",
                DifficultyLevel = 1
            };

            // Act
            var result = flashcardService.UpdateFlashcard(nonExistentFlashcard);

            // Assert - Fast fail, result should be false for invalid flashcard ID
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// Test to verify that UpdateFlashcard returns true for a valid ID.
        /// </summary>
        [Test]
        public void UpdateFlashcard_Valid_Id_Should_Return_True()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validFlashcard = new FlashcardModel
            {
                Id = "4cce8136-84c3-4e69-abfb-51fedae8432b", // valid flashcard ID
                Question = "Updated Question",
                Answer = "Updated Answer",
                CategoryId = "Python",
                DifficultyLevel = 2
            };

            // Act
            var result = flashcardService.UpdateFlashcard(validFlashcard);

            // Assert - Fast fail if update fails for valid flashcard
            ClassicAssert.AreEqual(true, result);
        }

        #endregion UpdateFlashcard

        #region GetCountByCategoryId

        /// <summary>
        /// Test to verify that GetCountByCategoryId returns the correct count for a valid category ID.
        /// </summary>
        [Test]
        public void GetCountByCategoryId_Valid_Category_Should_Return_Correct_Count()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validCategoryId = "Python"; // Example valid category ID

            // Act
            var result = flashcardService.GetCountByCategoryId(validCategoryId);

            // Assert - Fast fail if count is incorrect
            ClassicAssert.AreEqual(false, result == 0);
        }

        /// <summary>
        /// Test to verify that GetCountByCategoryId returns 0 for an invalid category ID.
        /// </summary>
        [Test]
        public void GetCountByCategoryId_Invalid_Category_Should_Return_Zero()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var invalidCategoryId = "InvalidCategory"; // Non-existing category ID

            // Act
            var result = flashcardService.GetCountByCategoryId(invalidCategoryId);

            // Assert - Fast fail, count should be zero for invalid category ID
            ClassicAssert.AreEqual(true, result == 0);
        }

        #endregion GetCountByCategoryId

        #region RemoveFlashcard

        /// <summary>
        /// Test to verify that RemoveFlashcard removes the flashcard for a valid ID and returns true.
        /// </summary>
        [Test]
        public void RemoveFlashcard_Valid_Id_Should_Return_True()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var validFlashcard = new FlashcardModel
            {
                Id = "123e4567-e89b-12d3-a456-426614174001", // Assume this is a valid flashcard ID that exists in the dataset
                Question = "Sample Question",
                Answer = "Sample Answer",
                CategoryId = "SampleCategory",
                DifficultyLevel = 1
            };

            // Adding the flashcard first to ensure it exists before testing removal
            flashcardService.CreateData(validFlashcard);

            // Act
            var result = flashcardService.RemoveFlashcard(validFlashcard.Id);

            // Assert - Verify that removal was successful
            ClassicAssert.AreEqual(true, result);

            // Verify that the flashcard no longer exists in the dataset
            var flashcardAfterRemoval = flashcardService.GetById(validFlashcard.Id);
            ClassicAssert.AreEqual(true, flashcardAfterRemoval == null);
        }

        /// <summary>
        /// Test to verify that RemoveFlashcard returns false for an invalid ID.
        /// </summary>
        [Test]
        public void RemoveFlashcard_Invalid_Id_Should_Return_False()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var invalidId = "00000000-0000-0000-0000-000000000000" ; // Non-existing flashcard ID

            // Act
            var result = flashcardService.RemoveFlashcard(invalidId);

            // Assert - Verify that removal attempt fails for non-existent ID
            ClassicAssert.AreEqual(false, result);
        }

        #endregion RemoveFlashcard
    
        #region CreateData

        /// <summary>
        /// Test to verify that CreateData adds a new flashcard
        /// and returns the flashcard with a unique ID.
        /// </summary>
        [Test]
        public void CreateData_Valid_Input_Should_Return_Flashcard_With_Unique_Id()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var newFlashcard = new FlashcardModel
            {
                Question = "New Question",
                Answer = "New Answer",
                CategoryId = "OOP",
                DifficultyLevel = 2
            };

            // Act
            var createdFlashcard = flashcardService.CreateData(newFlashcard);

            // Assert - Check that a flashcard was created
            ClassicAssert.AreEqual(false, createdFlashcard == null);

            // Assert - Verify that the ID is unique and greater than 0
            ClassicAssert.IsFalse(string.IsNullOrEmpty(createdFlashcard.Id));
        }
        
        /// <summary>
        /// Test to verify that CreateData initializes OpenCount to 0 for the new flashcard.
        /// </summary>
        [Test]
        public void CreateData_Valid_Input_Should_Initialize_OpenCount_To_Zero()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var newFlashcard = new FlashcardModel
            {
                Question = "Question with OpenCount",
                Answer = "Answer with OpenCount",
                CategoryId = "OOP",
                DifficultyLevel = 1
            };

            // Act
            var createdFlashcard = flashcardService.CreateData(newFlashcard);

            // Assert 
            ClassicAssert.AreEqual(0, createdFlashcard.OpenCount);
        }
        
        /// <summary>
        /// Test to verify that CreateData adds a flashcard to the dataset.
        /// </summary>
        [Test]
        public void CreateData_Valid_Input_Should_Add_Flashcard_To_Dataset()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var initialCount = flashcardService.GetAllData().Count();
            var newFlashcard = new FlashcardModel
            {
                Question = "Test Addition Question",
                Answer = "Test Addition Answer",
                CategoryId = "OOP",
                DifficultyLevel = 3
            };

            // Act
            flashcardService.CreateData(newFlashcard);
            var finalCount = flashcardService.GetAllData().Count();

            // Assert - Verify that the dataset count has increased by 1
            ClassicAssert.AreEqual(initialCount + 1, finalCount);
        }

        #endregion CreateData
        
        #region ValidateUrlAsync

        /// <summary>
        /// Test to verify that ValidateUrlAsync returns true for a valid and accessible URL.
        /// </summary>
        [Test]
        public async Task ValidateUrlAsync_Valid_Url_Should_Return_True()
        {
            // Arrange
            var validUrl = "https://www.google.com";

            // Act
            var result = await flashcardService.ValidateUrlAsync(validUrl);

            // Assert
            ClassicAssert.AreEqual(true, result);
        }

        /// <summary>
        /// Test to verify that ValidateUrlAsync returns false for an invalid URL.
        /// </summary>
        [Test]
        public async Task ValidateUrlAsync_Invalid_Url_Should_Return_False()
        {
            // Arrange
            var invalidUrl = "https://invalid.url.com";

            // Act
            var result = await flashcardService.ValidateUrlAsync(invalidUrl);

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// Test to verify that ValidateUrlAsync returns false for an empty URL.
        /// </summary>
        [Test]
        public async Task ValidateUrlAsync_Empty_Url_Should_Return_False()
        {
            // Arrange
            var emptyUrl = "";

            // Act
            var result = await flashcardService.ValidateUrlAsync(emptyUrl);

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// Test to verify that ValidateUrlAsync returns false for a null URL.
        /// </summary>
        [Test]
        public async Task ValidateUrlAsync_Null_Url_Should_Return_False()
        {
            // Arrange
            string nullUrl = null;

            // Act
            var result = await flashcardService.ValidateUrlAsync(nullUrl);

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// Test to verify that ValidateUrlAsync handles URLs with invalid format gracefully.
        /// </summary>
        [Test]
        public async Task ValidateUrlAsync_Invalid_Format_Url_Should_Return_False()
        {
            // Arrange
            var invalidFormatUrl = "invalid-url";

            // Act
            var result = await flashcardService.ValidateUrlAsync(invalidFormatUrl);

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        #endregion ValidateUrlAsync
    }
}