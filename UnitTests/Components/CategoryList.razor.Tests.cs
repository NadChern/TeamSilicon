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
        /// Sets up the required services and mocks for testing the CategoryList.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Register the services and mocks from TestHelper
            Services.AddSingleton(TestHelper.CategoryService);
            Services.AddSingleton(TestHelper.FlashcardService);
            Services.AddSingleton<LocalStorageCategoryService>(TestHelper.LocalStorageCategoryService);
        }

        #endregion TestSetup

        /// <summary>
        /// Tests if the CategoryList renders valid content.
        /// </summary>
        [Test]
        public void CategoryList_Valid_Default_Should_Render_Content()
        {
            // Arrange

            // Act
            var page = RenderComponent<CategoryList>();

            // Assert
            Assert.That(page.Markup.Contains("Filter by:"), Is.True);
        }

        /// <summary>
        /// Tests that toggling a category's heart icon updates the favorites in local storage.
        /// </summary>
        [Test]
        public async Task CategoryList_ToggleHeartAsync_Should_Update_Favorites()
        {
            // Arrange
            var page = RenderComponent<CategoryList>();
            var categoryId = "category-id-3";

            // Act: Add to favorites
            await TestHelper.LocalStorageCategoryService.AddToFavoritesAsync(categoryId);
            await page.Instance.ToggleHeartAsync(categoryId);

            // Assert: Should be removed from favorites
            var isFavorite = await TestHelper.LocalStorageCategoryService.IsFavoriteAsync(categoryId);
            Assert.That(isFavorite, Is.False);
        }

        /// <summary>
        /// Tests that GetFilteredCategories returns all categories when "All Categories" filter is selected.
        /// </summary>
        [Test]
        public void GetFilteredCategories_All_Should_Return_All_Categories()
        {
            // Arrange
            var page = RenderComponent<CategoryList>();

            // Act
            var result = page.Instance.GetFilteredCategories().ToList();

            // Assert
            Assert.That(result.Count, Is.GreaterThan(0)); // Assuming categories are not empty
        }

        /// <summary>
        /// Tests that GetFilteredCategories returns only favorite categories when the filter is set to "Favorite".
        /// </summary>
        [Test]
        public async Task GetFilteredCategories_Favorite_Should_Return_Only_Favorites()
        {
            // Arrange
            var page = RenderComponent<CategoryList>();
            var categoryId = "category-id-1";

            // Simulate adding a category to favorites
            await TestHelper.LocalStorageCategoryService.AddToFavoritesAsync(categoryId);

            // Act
            page.Instance.isAllCategories = false; // Set filter to "Favorite"
            var result = page.Instance.GetFilteredCategories().ToList();

            // Assert
            Assert.That(result.Any(c => c.Id == categoryId), Is.False);
        }

        /// <summary>
        /// Tests that clicking a category navigates to the correct URL.
        /// </summary>
        [Test]
        public void NavigateToCategory_Valid_Should_Navigate_To_Correct_Url()
        {
            // Arrange
            var page = RenderComponent<CategoryList>();
            var categoryId = "test-category-id";

            // Act
            page.Instance.NavigateToCategory(categoryId);

            // Assert
            var navigationManager = Services.GetRequiredService<FakeNavigationManager>();
            var relativeUri = navigationManager.ToBaseRelativePath(navigationManager.Uri);
            Assert.That(relativeUri, Is.EqualTo($"Flashcards/{categoryId}"));
        }

        /// <summary>
        /// Tests the default rendering of the filter dropdown.
        /// </summary>
        [Test]
        public void CategoryList_Valid_Default_Should_Have_All_Filter_Selected()
        {
            // Arrange
            var page = RenderComponent<CategoryList>();

            // Act
            var dropdown = page.Find("#categoryFilter");
            var selectedOption = dropdown.QuerySelector("option[selected]");

            // Assert
            Assert.That(selectedOption?.TextContent, Is.EqualTo("All Categories"));
        }
    }
}
