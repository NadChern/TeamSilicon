using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ContosoCrafts.WebSite.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace UnitTests.Services
{
    /// <summary>
    /// Unit tests for LocalStorageCategoryService.
    /// </summary>
    public class LocalStorageCategoryServiceTests
    {
        // Mock instance of ILocalStorageService
        private Mock<ILocalStorageService> _mockLocalStorageService;

        // Instance of LocalStorageCategoryService being tested
        private LocalStorageCategoryService _service;

        #region Setup

        /// <summary>
        /// Initialize resources before each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Initialize the mocked ILocalStorageService
            _mockLocalStorageService = new Mock<ILocalStorageService>();

            // Initialize the service with the mocked local storage
            _service = new LocalStorageCategoryService(_mockLocalStorageService.Object);
        }

        #endregion Setup

        #region GetFavoritesAsync

        /// <summary>
        /// Test to verify that GetFavoritesAsync returns an empty set when no data is stored.
        /// </summary>
        [Test]
        public async Task GetFavoritesAsync_Should_Return_Empty_Set_When_No_Data()
        {
            // Arrange
            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<HashSet<string>>(
                    It.Is<string>(key => key == "FavoriteCategories"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((HashSet<string>)null);

            // Act
            var result = await _service.GetFavoritesAsync();

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Test to verify that GetFavoritesAsync returns stored favorites.
        /// </summary>
        [Test]
        public async Task GetFavoritesAsync_Should_Return_Stored_Favorites()
        {
            // Arrange
            var storedFavorites = new HashSet<string> { "category1", "category2" };

            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<HashSet<string>>(
                    It.Is<string>(key => key == "FavoriteCategories"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(storedFavorites);

            // Act
            var result = await _service.GetFavoritesAsync();

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(2, result.Count);
            ClassicAssert.IsTrue(result.Contains("category1"));
        }

        #endregion GetFavoritesAsync

        #region AddToFavoritesAsync

        /// <summary>
        /// Test to verify that AddToFavoritesAsync adds a category to the favorites.
        /// </summary>
        [Test]
        public async Task AddToFavoritesAsync_Should_Add_Category_To_Favorites()
        {
            // Arrange
            var storedFavorites = new HashSet<string> { "category1" };

            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<HashSet<string>>(
                    It.Is<string>(key => key == "FavoriteCategories"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(storedFavorites);

            // Capture the data passed to SetItemAsync
            HashSet<string> capturedFavorites = null;

            _mockLocalStorageService
                .Setup(storage => storage.SetItemAsync(
                    It.Is<string>(key => key == "FavoriteCategories"),
                    It.IsAny<HashSet<string>>(),
                    It.IsAny<CancellationToken>()))
                .Callback<string, HashSet<string>, CancellationToken>((key, data, token) =>
                {
                    capturedFavorites = data;
                })
                .Returns(new ValueTask(Task.CompletedTask));

            // Act
            await _service.AddToFavoritesAsync("category2");

            // Assert
            ClassicAssert.IsNotNull(capturedFavorites);
            ClassicAssert.AreEqual(2, capturedFavorites.Count);
            ClassicAssert.IsTrue(capturedFavorites.Contains("category2"));
        }

        #endregion AddToFavoritesAsync

        #region RemoveFromFavoritesAsync

        /// <summary>
        /// Test to verify that RemoveFromFavoritesAsync removes a category from the favorites.
        /// </summary>
        [Test]
        public async Task RemoveFromFavoritesAsync_Should_Remove_Category_From_Favorites()
        {
            // Arrange
            var storedFavorites = new HashSet<string> { "category1", "category2" };

            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<HashSet<string>>(
                    It.Is<string>(key => key == "FavoriteCategories"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(storedFavorites);

            // Capture the data passed to SetItemAsync
            HashSet<string> capturedFavorites = null;

            _mockLocalStorageService
                .Setup(storage => storage.SetItemAsync(
                    It.Is<string>(key => key == "FavoriteCategories"),
                    It.IsAny<HashSet<string>>(),
                    It.IsAny<CancellationToken>()))
                .Callback<string, HashSet<string>, CancellationToken>((key, data, token) =>
                {
                    capturedFavorites = data;
                })
                .Returns(new ValueTask(Task.CompletedTask));

            // Act
            await _service.RemoveFromFavoritesAsync("category2");

            // Assert
            ClassicAssert.IsNotNull(capturedFavorites);
            ClassicAssert.AreEqual(1, capturedFavorites.Count);
            ClassicAssert.IsFalse(capturedFavorites.Contains("category2"));
        }

        #endregion RemoveFromFavoritesAsync

        #region IsFavoriteAsync

        /// <summary>
        /// Test to verify that IsFavoriteAsync returns true for a category in the favorites.
        /// </summary>
        [Test]
        public async Task IsFavoriteAsync_Should_Return_True_For_Favorite_Category()
        {
            // Arrange
            var storedFavorites = new HashSet<string> { "category1", "category2" };

            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<HashSet<string>>(
                    It.Is<string>(key => key == "FavoriteCategories"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(storedFavorites);

            // Act
            var result = await _service.IsFavoriteAsync("category1");

            // Assert
            ClassicAssert.IsTrue(result);
        }

        /// <summary>
        /// Test to verify that IsFavoriteAsync returns false for a category not in the favorites.
        /// </summary>
        [Test]
        public async Task IsFavoriteAsync_Should_Return_False_For_Non_Favorite_Category()
        {
            // Arrange
            var storedFavorites = new HashSet<string> { "category1", "category2" };

            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<HashSet<string>>(
                    It.Is<string>(key => key == "FavoriteCategories"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(storedFavorites);

            // Act
            var result = await _service.IsFavoriteAsync("category3");

            // Assert
            ClassicAssert.IsFalse(result);
        }

        #endregion IsFavoriteAsync
    }
}