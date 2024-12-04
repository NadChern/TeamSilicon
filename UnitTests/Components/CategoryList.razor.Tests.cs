using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using NUnit.Framework;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Components;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit tests for the CategoryList component.
    /// </summary>
    public class CategoryListTests : BunitTestContext
    {
        #region TestSetup

        /// <summary>
        /// Sets up the required services and mocks for testing the CategoryList component.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Register the services and mocks provided by TestHelper
            Services.AddSingleton(TestHelper.CategoryService);
            Services.AddSingleton(TestHelper.FlashcardService);
            Services.AddSingleton<LocalStorageCategoryService>(TestHelper.LocalStorageCategoryService);
        }

        #endregion TestSetup

        /// <summary>
        /// Validates that the CategoryList component renders the default content correctly.
        /// </summary>
        [Test]
        public void CategoryList_Valid_Default_Should_Render_Content()
        {
            // Act: Render the component
            var page = RenderComponent<CategoryList>();

            // Assert: Check if the component contains specific expected content
            Assert.That(page.Markup.Contains("Filter by:"), Is.True);
        }

        /// <summary>
        /// Verifies that toggling a category's heart icon updates the favorites in local storage correctly.
        /// </summary>
        [Test]
        public async Task CategoryList_ToggleHeartAsync_Should_Update_Favorites()
        {
            // Arrange: Render the component and select a test category ID
            var page = RenderComponent<CategoryList>();
            var categoryId = "category-id-3";

            // Act: Add the category to favorites and then toggle it
            await TestHelper.LocalStorageCategoryService.AddToFavoritesAsync(categoryId);
            await page.Instance.ToggleHeartAsync(categoryId);

            // Assert: Verify the category was removed from favorites
            var isFavorite = await TestHelper.LocalStorageCategoryService.IsFavoriteAsync(categoryId);
            Assert.That(isFavorite, Is.False);
        }

        /// <summary>
        /// Ensures that GetFilteredCategories returns all categories when the filter is set to "All Categories."
        /// </summary>
        [Test]
        public void GetFilteredCategories_All_Should_Return_All_Categories()
        {
            // Act: Render the component and fetch all filtered categories
            var page = RenderComponent<CategoryList>();
            var result = page.Instance.GetFilteredCategories().ToList();

            // Assert: Verify the result is not empty
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        /// <summary>
        /// Ensures that clicking on a heart icon and filtering by "Favorite" returns only favorited categories.
        /// </summary>
        [Test]
        public async Task Clicking_Heart_And_Filtering_Should_Show_Only_Favorited_Categories()
        {
            // Arrange:
            var page = RenderComponent<CategoryList>();
            var categoryId = "category-id-1";

            // Simulate clicking the heart icon to favorite the category
            var heartButton = page.Find($".CL-heart-icon");
            await heartButton.ClickAsync(new MouseEventArgs());

            // Act:
            var dropdown = page.Find("#categoryFilter");
            dropdown.Change("Favorite");

            // Fetch the filtered categories
            var result = page.Instance.GetFilteredCategories().ToList();

            // Assert: 
            Assert.That(result.Any(c => c.Id == categoryId), Is.False);
        }

        /// <summary>
        /// Tests that the NavigateToCategory method navigates to the correct URL.
        /// </summary>
        [Test]
        public void NavigateToCategory_Valid_Should_Navigate_To_Correct_Url()
        {
            // Arrange: Render the component and specify a test category ID
            var page = RenderComponent<CategoryList>();
            var categoryId = "test-category-id";

            // Act: Navigate to the category
            page.Instance.NavigateToCategory(categoryId);

            // Assert: Verify the navigation URL is correct
            var navigationManager = Services.GetRequiredService<FakeNavigationManager>();
            var relativeUri = navigationManager.ToBaseRelativePath(navigationManager.Uri);
            Assert.That(relativeUri, Is.EqualTo($"Flashcards/{categoryId}"));
        }

        /// <summary>
        /// Verifies that the filter dropdown contains both "All Categories" and "My Favorite" options.
        /// </summary>
        [Test]
        public void CategoryList_Valid_FilterDropdown_Should_Contain_Both_Options()
        {
            // Act: Render the component and find the filter dropdown
            var page = RenderComponent<CategoryList>();
            var dropdown = page.Find("#categoryFilter");

            // Find all option elements within the dropdown
            var options = dropdown.QuerySelectorAll("option").Select(option => option.TextContent).ToList();

            // Assert: Verify that both "All Categories" and "My Favorite" are present
            Assert.That(options, Does.Contain("All Categories"));
            Assert.That(options, Does.Contain("My Favorite"));
        }

        /// <summary>
        /// Ensures the GetHeartClass method returns the correct CSS class for active hearts.
        /// </summary>
        [Test]
        public void GetHeartClass_Should_Return_Correct_Class()
        {
            // Arrange: Render the component and add a category to active hearts
            var page = RenderComponent<CategoryList>();
            var categoryId = "test-category-id";
            page.Instance.activeHearts.Add(categoryId);

            // Act: Get the CSS class for the category
            var result = page.Instance.GetHeartClass(categoryId);

            // Assert: Verify the result is "active"
            Assert.That(result, Is.EqualTo("active"));
        }

        /// <summary>
        /// Ensures the GetGrowClass method returns the correct CSS class for growing hearts.
        /// </summary>
        [Test]
        public void GetGrowClass_Should_Return_Correct_Class()
        {
            // Arrange: Render the component and add a category to growing hearts
            var page = RenderComponent<CategoryList>();
            var categoryId = "test-category-id";
            page.Instance.growingHearts.Add(categoryId);

            // Act: Get the CSS class for the category
            var result = page.Instance.GetGrowClass(categoryId);

            // Assert: Verify the result is "grow"
            Assert.That(result, Is.EqualTo("grow"));
        }

        /// <summary>
        /// Verifies that changing the filter updates the state correctly.
        /// </summary>
        [Test]
        public void HandleFilterChange_Should_Update_Filter_State()
        {
            // Arrange: Render the component
            var page = RenderComponent<CategoryList>();

            // Act: Simulate changing the filter to "Favorite"
            var dropdown = page.Find("#categoryFilter");
            dropdown.Change("Favorite");

            // Assert: Verify the state was updated
            Assert.That(page.Instance.isAllCategories, Is.False);
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
        /// Tests the ShowLabel function to ensure it correctly updates the label.
        /// </summary>
        [Test]
        public void ShowLabel_Should_Update_When_Flashcards_Are_Deleted()
        {
            // Arrange
            // Render the CategoryList component to initialize the test context
            var page = RenderComponent<CategoryList>();

            // Define a test category ID and create two mock flashcards belonging to this category
            var testCategoryId = "test-category";
            var flashcard1 = new FlashcardModel { Id = "flashcard-1", CategoryId = testCategoryId, Question = "Question 1", Answer = "Answer 1" };
            var flashcard2 = new FlashcardModel { Id = "flashcard-2", CategoryId = testCategoryId, Question = "Question 2", Answer = "Answer 2" };

            // Add the mock flashcards to the FlashcardService
            TestHelper.FlashcardService.CreateData(flashcard1);
            TestHelper.FlashcardService.CreateData(flashcard2);

            // Act
            // Retrieve the label for the test category when both flashcards are present
            var labelWithTwoCards = page.Instance.ShowLabel(testCategoryId);

            // Remove the first flashcard from the service
            TestHelper.FlashcardService.RemoveFlashcard(flashcard1.Id);

            // Retrieve the label for the test category after one flashcard is removed
            var labelWithOneCard = page.Instance.ShowLabel(testCategoryId);

            // Remove the second flashcard from the service
            TestHelper.FlashcardService.RemoveFlashcard(flashcard2.Id);

            // Retrieve the label for the test category after all flashcards are removed
            var labelWithNoCards = page.Instance.ShowLabel(testCategoryId);

            // Assert
            // Verify that the label correctly indicates 2 cards when both flashcards are present
            Assert.That(labelWithTwoCards, Is.EqualTo("Contains 2 cards"), "Label should indicate 2 cards initially.");

            // Verify that the label updates to indicate 1 card after one flashcard is removed
            Assert.That(labelWithOneCard, Is.EqualTo("Contains 1 card"), "Label should update to 1 card after one is removed.");

            // Verify that the label updates to indicate no cards after all flashcards are removed
            Assert.That(labelWithNoCards, Is.EqualTo("Currently no card"), "Label should update to no cards after all are removed.");
        }
    }
}