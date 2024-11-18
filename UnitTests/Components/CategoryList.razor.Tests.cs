using Bunit;
using NUnit.Framework;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Bunit.TestDoubles;

namespace UnitTests.Components
{
    public class CategoryListTests : BunitTestContext
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
            // Mock services with valid data
            Services.AddSingleton<JsonFileCategoryService>(TestHelper.CategoryService);
            Services.AddSingleton<JsonFileFlashcardService>(TestHelper.FlashcardService);
        }

        #endregion TestSetup

        /// <summary>
        /// Test if the CategoryList component renders all categories correctly.
        /// </summary>
        [Test]
        public void CategoryList_Should_Render_All_Categories()
        {
            // Arrange
            var categoryService = TestHelper.CategoryService;
            var expectedCategoryCount = categoryService.GetAllData().Count();

            // Act
            var page = RenderComponent<CategoryList>();
            var renderedCards = page.FindAll(".CL-card");

            // Assert
            Assert.That(renderedCards.Count, Is.EqualTo(expectedCategoryCount), "The number of rendered category cards should match the data count.");
        }

        /// <summary>
        /// Test if clicking on a category navigates to the correct URL.
        /// </summary>
        [Test]
        public void CategoryList_Should_Navigate_To_Category_On_Click()
        {
            // Arrange
            var navigationManager = Services.GetRequiredService<FakeNavigationManager>();
            var page = RenderComponent<CategoryList>();
            var categoryId = "OOP"; // Valid category ID

            // Act
            var categoryCard = page.Find($"div[style*='#003366'] .CL-image"); // Target the correct clickable div
            categoryCard.Click(); // Simulate the click event

            // Assert
            Assert.That(navigationManager.Uri, Does.EndWith($"/Flashcards/{categoryId}"), "The URL should navigate to the correct category.");
        }


        /// <summary>
        /// Test if toggling the heart icon adds or removes the active class.
        /// </summary>
        [Test]
        public void CategoryList_ToggleHeart_Should_Add_Or_Remove_Active_Class()
        {
            // Arrange
            var page = RenderComponent<CategoryList>();
            var categoryId = "Python"; // Valid category ID
            var heartIcon = page.Find($"div[style*='#D269E7'] .CL-heart-icon");

            // Act
            heartIcon.Click(); // Toggle heart on
            var hasActiveClassAfterClick = heartIcon.ClassList.Contains("active");

            heartIcon.Click(); // Toggle heart off
            var hasActiveClassAfterSecondClick = heartIcon.ClassList.Contains("active");

            // Assert
            Assert.That(hasActiveClassAfterClick, Is.True, "The heart icon should have the 'active' class after being clicked once.");
            Assert.That(hasActiveClassAfterSecondClick, Is.False, "The heart icon should not have the 'active' class after being clicked twice.");
        }

        /// <summary>
        /// Test if toggling the heart icon triggers the grow animation.
        /// </summary>
        [Test]
        public void CategoryList_ToggleHeart_Should_Trigger_Grow_Animation()
        {
            // Arrange
            var page = RenderComponent<CategoryList>();
            var categoryId = "CPlusPlus"; // Valid category ID
            var heartIcon = page.Find($"div[style*='#FFA07A'] .CL-heart-icon");

            // Act
            heartIcon.Click(); // Trigger grow animation
            var hasGrowClass = heartIcon.ClassList.Contains("grow");

            // Assert
            Assert.That(hasGrowClass, Is.True, "The heart icon should have the 'grow' class after being clicked.");
        }

        /// <summary>
        /// Test if the category card's background color matches the expected category color.
        /// </summary>
        [Test]
        public void CategoryList_Should_Have_Correct_Background_Color()
        {
            // Arrange
            var categoryService = TestHelper.CategoryService;
            var categories = categoryService.GetAllData();

            var page = RenderComponent<CategoryList>();

            // Act & Assert
            foreach (var category in categories)
            {
                var cardElement = page.Find($"div[style*='{category.CategoryColor}']");
                var actualBackgroundColor = cardElement.GetAttribute("style");
                Assert.That(actualBackgroundColor, Contains.Substring(category.CategoryColor), $"The background color for category '{category.Id}' should match.");
            }
        }
    }
}
