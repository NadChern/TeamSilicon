using Bunit;
using NUnit.Framework;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Bunit.TestDoubles;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit test suite for the CategoryList Blazor component.
    /// This suite validates rendering, navigation, interactivity, and styles.
    /// </summary>
    public class CategoryListTests : BunitTestContext
    {
        #region TestSetup

        /// <summary>
        /// Initialize test dependencies and mock services before each test.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Mock services with test data from TestHelper
            Services.AddSingleton<JsonFileCategoryService>(TestHelper.CategoryService);
            Services.AddSingleton<JsonFileFlashcardService>(TestHelper.FlashcardService);
        }

        #endregion TestSetup

        #region TestMethods

        /// <summary>
        /// Validates that all categories are rendered correctly as cards.
        /// </summary>
        [Test]
        public void CategoryList_Should_Render_All_Categories()
        {
            // Arrange: Set up the test service and expected count of categories
            var categoryService = TestHelper.CategoryService; // Mocked category service
            var expectedCategoryCount = categoryService.GetAllData().Count(); // Count of categories to render

            // Act: Render the CategoryList component and find all category cards
            var page = RenderComponent<CategoryList>();
            var renderedCards = page.FindAll(".CL-card");

            // Assert: Verify the rendered card count matches the data count
            Assert.That(renderedCards.Count, Is.EqualTo(expectedCategoryCount),
                "The number of rendered category cards should match the data count.");
        }

        /// <summary>
        /// Validates that clicking on a category card navigates to the correct URL.
        /// </summary>
        [Test]
        public void CategoryList_Should_Navigate_To_Category_On_Click()
        {
            // Arrange: Set up navigation manager and render the component
            var navigationManager = Services.GetRequiredService<FakeNavigationManager>();
            var page = RenderComponent<CategoryList>();
            var targetCategoryId = "OOP"; // Valid category ID for the test

            // Find the clickable category card element
            var targetCategoryStyle = "#003366"; // Background style for the target category
            var categoryCard = page.Find($"div[style*='{targetCategoryStyle}'] .CL-image");

            // Act: Simulate a click on the target category card
            categoryCard.Click();

            // Assert: Verify the URL navigates to the expected category page
            Assert.That(navigationManager.Uri, Does.EndWith($"/Flashcards/{targetCategoryId}"),
                "The URL should navigate to the correct category.");
        }

        /// <summary>
        /// Validates that toggling the heart icon adds or removes the 'active' class.
        /// </summary>
        [Test]
        public void CategoryList_ToggleHeart_Should_Add_Or_Remove_Active_Class()
        {
            // Arrange: Render the component and locate the heart icon for a specific category
            var page = RenderComponent<CategoryList>();
            var heartIconStyle = "#D269E7"; // Style for the heart icon
            var heartIcon = page.Find($"div[style*='{heartIconStyle}'] .CL-heart-icon");

            // Act: Toggle the heart icon's state by clicking twice
            heartIcon.Click(); // Toggle heart on
            var hasActiveClassAfterClick = heartIcon.ClassList.Contains("active");

            heartIcon.Click(); // Toggle heart off
            var hasActiveClassAfterSecondClick = heartIcon.ClassList.Contains("active");

            // Assert: Verify the 'active' class is added and removed correctly
            Assert.That(hasActiveClassAfterClick, Is.True,
                "The heart icon should have the 'active' class after being clicked once.");
            Assert.That(hasActiveClassAfterSecondClick, Is.False,
                "The heart icon should not have the 'active' class after being clicked twice.");
        }

        /// <summary>
        /// Validates that toggling the heart icon triggers the grow animation.
        /// </summary>
        [Test]
        public void CategoryList_ToggleHeart_Should_Trigger_Grow_Animation()
        {
            // Arrange: Render the component and locate the heart icon for a specific category
            var page = RenderComponent<CategoryList>();
            var heartIconStyle = "#FFA07A"; // Style for the heart icon
            var heartIcon = page.Find($"div[style*='{heartIconStyle}'] .CL-heart-icon");

            // Act: Click the heart icon to trigger the grow animation
            heartIcon.Click();
            var hasGrowClass = heartIcon.ClassList.Contains("grow");

            // Assert: Verify the 'grow' class is added after clicking
            Assert.That(hasGrowClass, Is.True,
                "The heart icon should have the 'grow' class after being clicked.");
        }

        /// <summary>
        /// Validates that each category card's background color matches the expected color.
        /// </summary>
        [Test]
        public void CategoryList_Should_Have_Correct_Background_Color()
        {
            // Arrange: Get all categories and render the component
            var categoryService = TestHelper.CategoryService; // Mocked category service
            var categories = categoryService.GetAllData(); // List of categories
            var page = RenderComponent<CategoryList>();

            // Act & Assert: Verify the background color of each rendered category card
            foreach (var category in categories)
            {
                // Locate the card element by its background color style
                var cardElement = page.Find($"div[style*='{category.CategoryColor}']");
                var actualBackgroundColor = cardElement.GetAttribute("style");

                // Verify the card's background color matches the expected value
                Assert.That(actualBackgroundColor, Contains.Substring(category.CategoryColor),
                    $"The background color for category '{category.Id}' should match.");
            }
        }

        #endregion TestMethods
    }
}