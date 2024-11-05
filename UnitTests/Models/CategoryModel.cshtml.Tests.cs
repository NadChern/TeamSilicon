using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace UnitTests.Models
{
    /// <summary>
    /// Unit tests for CategoryModel class
    /// </summary>
    public class CategoryModelTests
    {
        /// <summary>
        /// Test that Id property can be set and retrieved correctly
        /// </summary>
        [Test]
        public void Id_Should_Set_And_Get_Value()
        {
            // Arrange 
            var category = new CategoryModel();

            // Act 
            category.Id = "1";

            // Assert 
            Assert.That(category.Id, Is.EqualTo("1"));
        }

        /// <summary>
        /// Test that Image property can be set and retrieved correctly
        /// </summary>
        [Test]
        public void Image_Should_Set_And_Get_Value()
        {
            // Arrange 
            var category = new CategoryModel();

            // Act 
            category.Image = "path/to/image.jpg";

            // Assert 
            Assert.That(category.Image, Is.EqualTo("path/to/image.jpg"));
        }

        /// <summary>
        /// Test that Title property can be set and retrieved correctly
        /// </summary>
        [Test]
        public void Title_Should_Set_And_Get_Value()
        {
            // Arrange 
            var category = new CategoryModel();

            // Act 
            category.Title = "Programming";

            // Assert 
            Assert.That(category.Title, Is.EqualTo("Programming"));
        }

        /// <summary>
        /// Test that CategoryColor property can be set and retrieved correctly
        /// </summary>
        [Test]
        public void CategoryColor_Should_Set_And_Get_Value()
        {
            // Arrange 
            var category = new CategoryModel();

            // Act 
            category.CategoryColor = "#FF5733";

            // Assert 
            Assert.That(category.CategoryColor, Is.EqualTo("#FF5733"));
        }

        /// <summary>
        /// Test that ToString method returns the JSON representation of the CategoryModel
        /// </summary>
        [Test]
        public void ToString_Should_Return_Json_Representation()
        {
            // Arrange 
            var category = new CategoryModel
            {
                Id = "1",
                Image = "path/to/image.jpg",
                Title = "Programming",
                CategoryColor = "#FF5733"
            };

            // Act 
            var json = category.ToString();

            // Assert 
            var expectedJson = JsonSerializer.Serialize(category);
            Assert.That(json, Is.EqualTo(expectedJson));
        }

        /// <summary>
        /// Test that Title property enforces the minimum length constraint
        /// </summary>
        [Test]
        public void Title_Should_Enforce_Minimum_Length()
        {
            // Arrange 
            var category = new CategoryModel { Title = "" };
            var validationContext = new ValidationContext(category) { MemberName = "Title" };
            var validationResults = new List<ValidationResult>();

            // Act 
            var isValid = Validator.TryValidateProperty(category.Title, validationContext, validationResults);

            // Assert 
            Assert.That(isValid, Is.False);
            Assert.That(validationResults.Count, Is.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Title should have a length of more than 1 and less than 33"));
        }

        /// <summary>
        /// Test that Title property enforces the maximum length constraint
        /// </summary>
        [Test]
        public void Title_Should_Enforce_Maximum_Length()
        {
            // Arrange 
            var category = new CategoryModel { Title = new string('A', 34) }; // 34 characters
            var validationContext = new ValidationContext(category) { MemberName = "Title" };
            var validationResults = new List<ValidationResult>();

            // Act 
            var isValid = Validator.TryValidateProperty(category.Title, validationContext, validationResults);

            // Assert 
            Assert.That(isValid, Is.False);
            Assert.That(validationResults.Count, Is.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The Title should have a length of more than 1 and less than 33"));
        }

        /// <summary>
        /// Test that Title property passes validation when within length constraints
        /// </summary>
        [Test]
        public void Title_Should_Pass_Validation_With_Valid_Length()
        {
            // Arrange 
            var category = new CategoryModel { Title = "Valid Title" };
            var validationContext = new ValidationContext(category) { MemberName = "Title" };
            var validationResults = new List<ValidationResult>();

            // Act 
            var isValid = Validator.TryValidateProperty(category.Title, validationContext, validationResults);

            // Assert 
            Assert.That(isValid, Is.True);
            Assert.That(validationResults.Count, Is.EqualTo(0));
        }
    }
}