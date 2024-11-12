using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;


namespace UnitTests.Models
{
    /// <summary>
    /// Unit tests for FlashcardModel class.
    /// </summary>
    public class FlashcardModelTests
    {
        #region Id

        /// <summary>
        /// Test to ensure the Id getter works correctly.
        /// </summary>
        [Test]
        public void Get_Id_Valid_Should_Return_Correct_Id()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                Id = "123e4567-e89b-12d3-a456-426614174000"
            };

            // Act:
            var result = flashcard.Id;

            // Assert:
            ClassicAssert.AreEqual("123e4567-e89b-12d3-a456-426614174000", result);
        }

        /// <summary>
        /// Test to ensure the Id setter works correctly.
        /// </summary>
        [Test]
        public void Set_Id_Valid_Should_Return_Correct_Id()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                Id ="123e4567-e89b-12d3-a456-426614174000"
            };

            flashcard.Id = "123e4567-e89b-12d3-a456-426614174001";

            // Act:
            var result = flashcard.Id;

            // Assert:
            ClassicAssert.AreEqual("123e4567-e89b-12d3-a456-426614174001", result);
        }
        #endregion Id Property Tests

        #region CategoryId
        /// <summary>
        /// Test to ensure the CategoryId getter works correctly.
        /// </summary>
        [Test]
        public void Get_CategoryId_Valid_Should_Return_Correct_CategoryId()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                CategoryId = "OOP"
            };

            // Act:
            var result = flashcard.CategoryId;

            // Assert:
            ClassicAssert.AreEqual("OOP", result);
        }

        /// <summary>
        /// Test to ensure the CategoryId setter works correctly.
        /// </summary>
        [Test]
        public void Set_CategoryId_Valid_Should_Return_Correct_CategoryId()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                CategoryId = "OOP"
            };

            flashcard.CategoryId = "Python";

            // Act:
            var result = flashcard.CategoryId;

            // Assert:
            ClassicAssert.AreEqual("Python", result);
        }
        #endregion CategoryId

        #region Question
        /// <summary>
        /// Test to ensure the Question getter works correctly.
        /// </summary>
        [Test]
        public void Get_Question_Valid_Should_Return_Correct_Question()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                Question = "Sample Question?"
            };

            // Act:
            var result = flashcard.Question;

            // Assert:
            ClassicAssert.AreEqual("Sample Question?", result);
        }

        /// <summary>
        /// Test to ensure the Question setter works correctly.
        /// </summary>
        [Test]
        public void Set_Question_Valid_Should_Return_Correct_Question()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                Question = "Sample Question?"
            };

            flashcard.Question = "Sample Question 2?";

            // Act:
            var result = flashcard.Question;

            // Assert:
            ClassicAssert.AreEqual("Sample Question 2?", result);
        }
        #endregion Question

        #region Answer
        /// <summary>
        /// Test to ensure the Answer getter works correctly.
        /// </summary>
        [Test]
        public void Get_Answer_Valid_Should_Return_Correct_Answer()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                Answer = "Sample Answer"
            };

            // Act:
            var result = flashcard.Answer;

            // Assert:
            ClassicAssert.AreEqual("Sample Answer", result);
        }

        /// <summary>
        /// Test to ensure the Answer setter works correctly.
        /// </summary>
        [Test]
        public void Set_Answer_Valid_Should_Return_Correct_Answer()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                Answer = "Sample Answer 2"
            };

            flashcard.Question = "Sample Answer 2";

            // Act:
            var result = flashcard.Answer;

            // Assert:
            ClassicAssert.AreEqual("Sample Answer 2", result);
        }
        #endregion Answer

        #region DifficultyLevel
        /// <summary>
        /// Test to ensure the Difficulty Level getter works correctly.
        /// </summary>
        [Test]
        public void Get_DifficultyLevel_Valid_Should_Return_Correct_DifficultyLevel()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                DifficultyLevel = 3
            };

            // Act:
            var result = flashcard.DifficultyLevel;

            // Assert:
            ClassicAssert.AreEqual(3, result);
        }

        /// <summary>
        /// Test to ensure the DifficultyLevel setter works correctly.
        /// </summary>
        [Test]
        public void Set_DifficultyLevel_Valid_Should_Return_Correct_DifficultyLevel()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                DifficultyLevel = 1
            };

            flashcard.DifficultyLevel = 3;

            // Act:
            var result = flashcard.DifficultyLevel;

            // Assert:
            ClassicAssert.AreEqual(3, result);
        }
        #endregion DifficultyLevel

        #region Url
        /// <summary>
        /// Test to ensure the Url getter works correctly.
        /// </summary>
        [Test]
        public void Get_Url_Valid_Should_Return_Correct_Url()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                Url = "https://www.sample.com"
            };

            // Act:
            var result = flashcard.Url;

            // Assert:
            ClassicAssert.AreEqual("https://www.sample.com", result);
        }

        /// <summary>
        /// Test to ensure the Url setter works correctly.
        /// </summary>
        [Test]
        public void Set_Url_Valid_Should_Return_Correct_Url()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                Url = "https://www.sample.com"
            };

            flashcard.Url = "https://www.sample2.com";

            // Act:
            var result = flashcard.Url;

            // Assert:
            ClassicAssert.AreEqual("https://www.sample2.com", result);
        }
        #endregion Url

        #region OpenCount
        /// <summary>
        /// Test to ensure the OpenCount getter works correctly.
        /// </summary>
        [Test]
        public void Get_OpenCount_Valid_Should_Return_Correct_OpenCount()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                OpenCount = 123
            };

            // Act:
            var result = flashcard.OpenCount;

            // Assert:
            ClassicAssert.AreEqual(123, result);
        }

        /// <summary>
        /// Test to ensure the OpenCount setter works correctly.
        /// </summary>
        [Test]
        public void Set_OpenCount_Valid_Should_Return_Correct_OpenCount()
        {
            // Arrange:
            var flashcard = new FlashcardModel
            {
                OpenCount = 123
            };

            flashcard.OpenCount = 0;

            // Act:
            var result = flashcard.OpenCount;

            // Assert:
            ClassicAssert.AreEqual(0, result);
        }
        #endregion OpenCount
    }
}
