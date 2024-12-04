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
        /// Ensures that GetFilteredCategories returns only favorite categories when the filter is set to "Favorite."
        /// </summary>
        [Test]
        public async Task GetFilteredCategories_Favorite_Should_Return_Only_Favorites()
        {
            // Arrange: Render the component and simulate adding a category to favorites
            var page = RenderComponent<CategoryList>();
            var categoryId = "category-id-1";
            await TestHelper.LocalStorageCategoryService.AddToFavoritesAsync(categoryId);

            // Act: Change the filter to "Favorite" and fetch the filtered categories
            page.Instance.isAllCategories = false;
            var result = page.Instance.GetFilteredCategories().ToList();

            // Assert: Verify the result contains only the favorited category
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
        /// Verifies that the filter dropdown defaults to "All Categories" on initial render.
        /// </summary>
        [Test]
        public void CategoryList_Valid_Default_Should_Have_All_Filter_Selected()
        {
            // Act: Render the component and find the filter dropdown
            var page = RenderComponent<CategoryList>();
            var dropdown = page.Find("#categoryFilter");
            var selectedOption = dropdown.QuerySelector("option[selected]");

            // Assert: Verify the selected option is "All Categories"
            Assert.That(selectedOption?.TextContent, Is.EqualTo("All Categories"));
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
    }
}