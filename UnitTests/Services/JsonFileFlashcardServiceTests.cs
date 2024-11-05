using System.Collections.Generic;
using System.Linq;
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
            var validId = 1; // Set valid flashcard ID

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
            var invalidId = 9999; // Non-existing flashcard ID

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
                Id = -1,
                Question = "Question",
                Answer = "Answer",
                CategoryId = "Python",
                DifficultyLevel = "Easy"
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
                Id = 1, // valid flashcard ID
                Question = "Updated Question",
                Answer = "Updated Answer",
                CategoryId = "Python",
                DifficultyLevel = "Medium"
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
                Id = 2, // Assume this is a valid flashcard ID that exists in the dataset
                Question = "Sample Question",
                Answer = "Sample Answer",
                CategoryId = "SampleCategory",
                DifficultyLevel = "Easy"
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
            var invalidId = 9999; // Non-existing flashcard ID

            // Act
            var result = flashcardService.RemoveFlashcard(invalidId);

            // Assert - Verify that removal attempt fails for non-existent ID
            ClassicAssert.AreEqual(false, result);
        }
        #endregion RemoveFlashcard

        #region GenerateCardId
        
        /// <summary>
        /// Test to verify that GenerateCardId returns a positive integer as the new ID.
        /// </summary>
        [Test]
        public void GenerateCardId_Should_Return_Positive_Integer()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;

            // Act
            var newId = flashcardService.GenerateCardId();

            // Assert 
            ClassicAssert.AreEqual(true, newId > 0);
        }
        
        /// <summary>
        /// Test to verify that GenerateCardId returns an ID that is not in the existing dataset.
        /// </summary>
        [Test]
        public void GenerateCardId_Should_Return_Unique_Id_Not_In_Dataset()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            var existingIds = flashcardService.GetAllData().Select(f => f.Id).ToList();

            // Act
            var newId = flashcardService.GenerateCardId();

            // Assert - Verify that the generated ID is unique and not in the existing dataset
            ClassicAssert.AreEqual(false, existingIds.Contains(newId));
        }
        
        /// <summary>
        /// Test to verify that GenerateCardId increments properly when there are no gaps in the dataset.
        /// </summary>
        [Test]
        public void GenerateCardId_Should_Increment_When_No_Gaps()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;

            // Create a dataset with sequential IDs to remove gaps
            var lastFlashcardId = flashcardService.GetAllData().Max(f => f.Id);
            var newFlashcard = new FlashcardModel
            {
                Id = lastFlashcardId + 1,
                Question = "Test Question",
                Answer = "Test Answer",
                CategoryId = "OOP",
                DifficultyLevel = "Medium"
            };

            flashcardService.CreateData(newFlashcard);

            // Act
            var newId = flashcardService.GenerateCardId();

            // Assert - Verify that the new ID is one more than the last ID
            ClassicAssert.AreEqual(lastFlashcardId + 2, newId);
        }
        
        /// <summary>
        /// Test to verify that GenerateCardId returns the first available gap in the sequence.
        /// </summary>
        [Test]
       public void GenerateCardId_Should_Return_First_Available_Gap()
        {
            // Arrange - create a list with a gap in IDs
            var testData = new List<FlashcardModel>
            {
                new FlashcardModel { Id = 1, Question = "Q1", Answer = "A1", CategoryId = "OOP" },
                new FlashcardModel { Id = 2, Question = "Q2", Answer = "A2", CategoryId = "OOP" },
                // Gap at ID 3
                new FlashcardModel { Id = 4, Question = "Q4", Answer = "A4", CategoryId = "OOP" }
            };

            var flashcardService = TestHelper.FlashcardService;

            // Act
            var newId = flashcardService.GenerateCardId(testData);

            // Assert
            Assert.That(newId, Is.EqualTo(3));
        }

        /// <summary>
        /// Test to verify that GenerateCardId returns the next ID after the highest ID when there are no gaps.
        /// </summary>
        [Test]
        public void GenerateCardId_Should_Return_Next_Id_If_No_Gaps()
        {
            // Arrange - create a list with no gaps
            var flashcardsSequential = new List<FlashcardModel>
            {
                new FlashcardModel { Id = 1, Question = "Q1", Answer = "A1", CategoryId = "OOP" },
                new FlashcardModel { Id = 2, Question = "Q2", Answer = "A2", CategoryId = "OOP" },
                new FlashcardModel { Id = 3, Question = "Q3", Answer = "A3", CategoryId = "OOP" }
            };

            var flashcardService = TestHelper.FlashcardService;

            // Act
            var newId = flashcardService.GenerateCardId(flashcardsSequential);

            // Assert 
            Assert.That(newId, Is.EqualTo(4));
        }
        
        #endregion GenerateCardId
        
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
                DifficultyLevel = "Medium"
            };

            // Act
            var createdFlashcard = flashcardService.CreateData(newFlashcard);

            // Assert - Check that a flashcard was created
            ClassicAssert.AreEqual(false, createdFlashcard == null);

            // Assert - Verify that the ID is unique and greater than 0
            ClassicAssert.AreEqual(true, createdFlashcard.Id > 0);
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
                DifficultyLevel = "Easy"
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
                DifficultyLevel = "Hard"
            };

            // Act
            flashcardService.CreateData(newFlashcard);
            var finalCount = flashcardService.GetAllData().Count();

            // Assert - Verify that the dataset count has increased by 1
            ClassicAssert.AreEqual(initialCount + 1, finalCount);
        }
        #endregion CreateData
        
    }
}
