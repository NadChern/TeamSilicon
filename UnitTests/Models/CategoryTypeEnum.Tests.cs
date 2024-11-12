using NUnit.Framework;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Models
{
    
    /// <summary>
    /// Unit tests for CategoryTypeEnumExtensions class
    /// </summary>
    public class CategoryTypeEnumExtensionsTests
    {
        #region DisplayName Tests

        /// <summary>
        /// Test that DisplayName returns "OOP" for CategoryTypeEnum.OOP
        /// </summary>
        [Test]
        public void DisplayName_Valid_Test_OOP_Should_Return_OOP()
        {
            // Arrange
            var category = CategoryTypeEnum.OOP;

            // Act
            var displayName = category.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("OOP"));
        }

        /// <summary>
        /// Test that DisplayName returns "Python" for CategoryTypeEnum.Python
        /// </summary>
        [Test]
        public void DisplayName_Valid_Test_Python_Should_Return_Python()
        {
            
            // Arrange
            var category = CategoryTypeEnum.Python;

            // Act
            var displayName = category.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Python"));
        }

        /// <summary>
        /// Test that DisplayName returns "CSharp" for CategoryTypeEnum.CSharp
        /// </summary>
        [Test]
        public void DisplayName_Valid_Test_CSharp_Should_Return_CSharp()
        {
            
            // Arrange
            var category = CategoryTypeEnum.CSharp;

            // Act
            var displayName = category.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("CSharp"));
        }

        /// <summary>
        /// Test that DisplayName returns "CPlusPlus" for CategoryTypeEnum.CPlusPlus
        /// </summary>
        [Test]
        public void DisplayName_Valid_Test_CPlusPlus_Should_Return_CPlusPlus()
        {
            
            // Arrange
            var category = CategoryTypeEnum.CPlusPlus;

            // Act
            var displayName = category.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("CPlusPlus"));
        }

        /// <summary>
        /// Test that DisplayName returns "Mobile" for CategoryTypeEnum.Mobile
        /// </summary>
        [Test]
        public void DisplayName_Valid_Test_Mobile_Should_Return_Mobile()
        {
            
            // Arrange
            var category = CategoryTypeEnum.Mobile;

            // Act
            var displayName = category.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Mobile"));
        }

        /// <summary>
        /// Test that DisplayName returns "DS" for CategoryTypeEnum.DS
        /// </summary>
        [Test]
        public void DisplayName_Valid_Test_DS_Should_Return_DS()
        {
            
            // Arrange
            var category = CategoryTypeEnum.DS;

            // Act
            var displayName = category.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("DS"));
        }

        /// <summary>
        /// Test that DisplayName returns the default ToString for an undefined category
        /// </summary>
        [Test]
        public void DisplayName_Invalid_Test_UndefinedCategory_Should_Return_ToString()
        {
            
            // Arrange
            var undefinedCategory = (CategoryTypeEnum)99;

            // Act
            var displayName = undefinedCategory.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(undefinedCategory.ToString()));
        }

        #endregion DisplayName Tests
    }
}