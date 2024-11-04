using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework.Legacy;
using ContosoCrafts.WebSite.Pages.FlashcardAdmin;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitTests.Pages.FlashcardAdmin
{
    /// <summary>
    /// Unit Tests for UpdateModel class.
    /// </summary>
    public class UpdateTests
    {
        private JsonFileFlashcardService flashcardService;
        private UpdateModel updateModel;

        #region OnGet
        /// <summary>
        /// OnGet should return false for invalid id
        /// </summary>
        [Test]
        public void OnGet_Invalid_Id_Should_Return_False()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            updateModel = new UpdateModel(flashcardService);
            var invalidId = 9999;

            // Act
            updateModel.OnGet(invalidId);
            var result = updateModel.IsFlashcardLoaded;

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// OnGet should return true for valid Id
        /// </summary>
        [Test]
        public void OnGet_Valid_Id_Should_Return_True()
        {
            // Arrange
            flashcardService = TestHelper.FlashcardService;
            updateModel = new UpdateModel(flashcardService);
            var validId = 1;

            // Act
            updateModel.OnGet(validId);
            var result = updateModel.IsFlashcardLoaded;

            // Assert
            ClassicAssert.AreEqual(true, result);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// OnPost should return false for an invalid flashcard model
        /// </summary>
        [Test]
        public void OnPost_Invalid_Model_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "", // Invalid model
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Easy"
                }
            };

            // Note: [Required(ErrorMessage = "*Required")] for Question in FlashcardModel.cs
            // It does not automatically execute validation in unit test,
            // so we manually add error to mimic the behavior.
            // Simulate validation error. Question is Required
            updateModel.ModelState.AddModelError("Flashcard.Question", "*Required");

            // Act:
            updateModel.OnPost();
            var result = updateModel.IsFlashcardUpdated;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// OnPost should return true for a valid model
        /// </summary>
        [Test]
        public void OnPost_Valid_Model_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question", // Valid model
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            updateModel.OnPost();
            var result = updateModel.IsFlashcardUpdated;

            // Assert:
            ClassicAssert.AreEqual(true, result);
        }
        #endregion OnPost

        #region DiffucultyLevel
        /// <summary>
        /// IsEasySelected should return false for null flashcard
        /// </summary>
        [Test]
        public void IsEasySelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsEasySelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsEasySelected should return false for a DifficultyLevel that is not Easy
        /// </summary>
        [Test]
        public void IsEasySelected_Invalid_DifficultyLevel_Not_Easy_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsEasySelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsEasySelected should return true for a DifficultyLevel that is Easy
        /// </summary>
        [Test]
        public void IsEasySelected_Valid_DifficultyLevel_Easy_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            var result = updateModel.IsEasySelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("Easy", updateModel.Flashcard.DifficultyLevel);
        }

        /// <summary>
        /// IsMediumSelected should return false for a null Flashcard
        /// </summary>
        [Test]
        public void IsMediumSelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsMediumSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsMediumSelected should return false for a DifficultyLevel that is not Medium
        /// </summary>
        [Test]
        public void IsMediumSelected_Invalid_DifficultyLevel_Not_Medium_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsMediumSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsMediumSelected should return true for a DifficultyLevel that is Medium
        /// </summary>
        [Test]
        public void IsMediumSelected_Valid_DifficultyLevel_Medium_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Medium"
                }
            };

            // Act:
            var result = updateModel.IsMediumSelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("Medium", updateModel.Flashcard.DifficultyLevel);
        }

        /// <summary>
        /// IsHardSelected should return false for a null Flashcard
        /// </summary>
        [Test]
        public void IsHardSelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsHardSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsHardSelected should return false for a DifficultyLevel that is not Hard
        /// </summary>
        [Test]
        public void IsHardSelected_Invalid_DifficultyLevel_Not_Hard_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            var result = updateModel.IsHardSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsHardSelected should return true for a DifficultyLevel that is Hard
        /// </summary>
        [Test]
        public void IsHardSelected_Valid_DifficultyLevel_Hard_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsHardSelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("Hard", updateModel.Flashcard.DifficultyLevel);
        }
        #endregion DifficultyLevel

        #region Category
        /// <summary>
        /// IsOOPSelected should return false for a null Flashcard
        /// </summary>
        [Test]
        public void IsOOPSelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsOOPSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsOOPSelected should return false for a Category that is not OOP
        /// </summary>
        [Test]
        public void IsOOPSelected_Invalid_Category_Not_OOP_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "Python",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsOOPSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsOOPSelected should return true for a Category that is OOP
        /// </summary>
        [Test]
        public void IsOOPSelected_Valid_Category_OOP_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            var result = updateModel.IsOOPSelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("OOP", updateModel.Flashcard.CategoryId);
        }

        /// <summary>
        /// IsPythonSelected should return false for null Flashcard
        /// </summary>
        [Test]
        public void IsPythonSelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsPythonSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsPythonSelected should return false for a Category that is not Python
        /// </summary>
        [Test]
        public void IsPythonSelected_Invalid_Category_Not_Python_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsPythonSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsPythonSelected should return true for a Category that is Python
        /// </summary>
        [Test]
        public void IsPythonSelected_Valid_Category_Python_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "Python",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            var result = updateModel.IsPythonSelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("Python", updateModel.Flashcard.CategoryId);
        }

        /// <summary>
        /// IsCSharpSelected should return false for a null Flashcard
        /// </summary>
        [Test]
        public void IsCSharpSelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsCSharpSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsCSharpSelected should return false for a Category that is not CSharp
        /// </summary>
        [Test]
        public void IsCSharpSelected_Invalid_Category_Not_CSharp_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsCSharpSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsCSharpSelected should return true for a Category that is CSharp
        /// </summary>
        [Test]
        public void IsCsharpSelected_Valid_Category_CSharp_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "C#",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            var result = updateModel.IsCSharpSelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("C#", updateModel.Flashcard.CategoryId);
        }

        /// <summary>
        /// IsCPlusPlusSelected should return false for a null Flashcard
        /// </summary>
        [Test]
        public void IsCPlusPlusSelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsCPlusPlusSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsCPlusPlusSelected should return false for a Category that is not CPlusPlus
        /// </summary>
        [Test]
        public void IsCPlusPlusSelected_Invalid_Category_Not_CPlusPlus_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsCPlusPlusSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsCPlusPlusSelected should return true for a Category that is CPlusPlus
        /// </summary>
        [Test]
        public void IsCPlusPlusSelected_Valid_Category_CPlusPlus_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "C++",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            var result = updateModel.IsCPlusPlusSelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("C++", updateModel.Flashcard.CategoryId);
        }

        /// <summary>
        /// IsMobileSelected should return false for a null Flashcard
        /// </summary>
        [Test]
        public void IsMobileSelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsMobileSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsMobileSelected should return false for a Category that is not Mobile
        /// </summary>
        [Test]
        public void IsMobileSelected_Invalid_Category_Not_Mobile_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsMobileSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsMobileSelected should return true for a Category that is Mobile
        /// </summary>
        [Test]
        public void IsMobileSelected_Valid_Category_Mobile_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "Mobile",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            var result = updateModel.IsMobileSelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("Mobile", updateModel.Flashcard.CategoryId);
        }

        /// <summary>
        /// IsDSSelected should return false for a null Flashcard
        /// </summary>
        [Test]
        public void IsDSSelected_Invalid_Flashcard_Null_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = null // Set Flashcard to null
            };

            // Act:
            var result = updateModel.IsDSSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsDSSelected should return false for a Category that is not DS (Distributed Systems)
        /// </summary>
        [Test]
        public void IsDSSelected_Invalid_Category_Not_DS_Should_Return_False()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "OOP",
                    DifficultyLevel = "Hard"
                }
            };

            // Act:
            var result = updateModel.IsDSSelected;

            // Assert:
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// IsDSSelected should return true for a Category that is DS (Distributed Systems)
        /// </summary>
        [Test]
        public void IsDSSelected_Valid_Category_DS_Should_Return_True()
        {
            // Arrange:
            flashcardService = TestHelper.FlashcardService;
            var updateModel = new UpdateModel(flashcardService)
            {
                Flashcard = new FlashcardModel
                {
                    Id = 1,
                    Question = "Sample Question",
                    Answer = "Sample Answer",
                    CategoryId = "DS",
                    DifficultyLevel = "Easy"
                }
            };

            // Act:
            var result = updateModel.IsDSSelected;

            // Assert:
            ClassicAssert.AreEqual(true, result);
            ClassicAssert.AreEqual("DS", updateModel.Flashcard.CategoryId);
        }
        #endregion Category
    }
}