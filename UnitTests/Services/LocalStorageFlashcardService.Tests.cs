using System;
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
    /// Unit Tests for LocalStorageFlashcardService
    /// </summary>
    public class LocalStorageFlashcardServiceTests
    {
        // Mock instance of ILocalStorageService to simulate local storage operations
        private Mock<ILocalStorageService> _mockLocalStorageService;

        // Instance of LocalStorageFlashcardService being tested
        private LocalStorageFlashcardService _service;

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
            _service = new LocalStorageFlashcardService(_mockLocalStorageService.Object);
        }

        #endregion Setup

        #region GetAllAsync

        /// <summary>
        /// Test to verify that GetAllAsync returns an empty list when no data is stored.
        /// </summary>
        [Test]
        public async Task GetAllAsync_Should_Return_Empty_List_When_No_Data()
        {
            // Arrange (simulate no data in the local storage)
            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<List<LocalStorageFlashcardService.FlashcardData>>(
                    It.Is<string>(key => key == "FlashcardsData"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<LocalStorageFlashcardService.FlashcardData>)null);

            // Act
            var result = await _service.GetAllAsync();

            // Assert (validate that the result is an empty list)
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// Test to verify that GetAllAsync returns stored flashcard data.
        /// </summary>
        [Test]
        public async Task GetAllAsync_Should_Return_Stored_FlashcardData()
        {
            // Arrange
            // Simulate stored flashcard data in local storage
            var storedData = new List<LocalStorageFlashcardService.FlashcardData>
            {
                new LocalStorageFlashcardService.FlashcardData { CardId = "card1", LastOpenedDate = DateTime.UtcNow }
            };

            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<List<LocalStorageFlashcardService.FlashcardData>>(
                    It.Is<string>(key => key == "FlashcardsData"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(storedData);

            // Act
            // Call GetAllAsync to retrieve data
            var result = await _service.GetAllAsync();

            // Assert (validate retrieved data)
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(1, result.Count);
            ClassicAssert.AreEqual("card1", result[0].CardId);
        }

        #endregion GetAllAsync

        #region UpdateAsync

        /// <summary>
        /// Test to verify that UpdateAsync adds a new flashcard if it does not already exist.
        /// </summary>
        [Test]
        public async Task UpdateAsync_Should_Add_New_Flashcard_If_Not_Exists()
        {
            // Arrange
            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<List<LocalStorageFlashcardService.FlashcardData>>(
                    It.Is<string>(key => key == "FlashcardsData"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<LocalStorageFlashcardService.FlashcardData>());

            // Capture the data passed to SetItemAsync for assertion
            List<LocalStorageFlashcardService.FlashcardData> capturedData = null;
            _mockLocalStorageService
                .Setup(storage => storage.SetItemAsync(
                    It.Is<string>(key => key == "FlashcardsData"),
                    It.IsAny<List<LocalStorageFlashcardService.FlashcardData>>(),
                    It.IsAny<CancellationToken>()))
                .Callback<string, List<LocalStorageFlashcardService.FlashcardData>, CancellationToken>((key, data, token) =>
                {
                    capturedData = data; // Capture the data for later assertions
                })
                .Returns(new ValueTask(Task.CompletedTask)); // Simulate async operation

            // Act
            await _service.UpdateAsync("new-card");

            // Assert
            // Ensure the mock was called
            ClassicAssert.IsNotNull(capturedData, "SetItemAsync was not called.");
            ClassicAssert.AreEqual(1, capturedData.Count, "The captured data should contain exactly one item.");
            ClassicAssert.AreEqual("new-card", capturedData[0].CardId, "The CardId should match the new card.");
        }

        /// <summary>
        /// Test to verify that UpdateAsync updates LastOpenedDate for an existing flashcard.
        /// </summary>
        [Test]
        public async Task UpdateAsync_Should_Update_LastOpenedDate_If_Flashcard_Exists()
        {
            // Arrange
            // Simulate old date
            var originalDate = DateTime.UtcNow.AddDays(-1);

            // Existing card
            var existingCard = new LocalStorageFlashcardService.FlashcardData
            {
                CardId = "existing-card",
                LastOpenedDate = originalDate
            };

            // List of stored data containing the existing card
            var storedData = new List<LocalStorageFlashcardService.FlashcardData> { existingCard };

            _mockLocalStorageService
                .Setup(storage => storage.GetItemAsync<List<LocalStorageFlashcardService.FlashcardData>>(
                    It.Is<string>(key => key == "FlashcardsData"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(storedData);

            // Capture the data passed to SetItemAsync for assertion
            List<LocalStorageFlashcardService.FlashcardData> capturedData = null;

            _mockLocalStorageService
                .Setup(storage => storage.SetItemAsync(
                    It.Is<string>(key => key == "FlashcardsData"),
                    It.IsAny<List<LocalStorageFlashcardService.FlashcardData>>(),
                    It.IsAny<CancellationToken>()))
                .Callback<string, List<LocalStorageFlashcardService.FlashcardData>, CancellationToken>((key, data, token) =>
                {
                    capturedData = data;
                })
                .Returns(new ValueTask(Task.CompletedTask));

            // Act
            await _service.UpdateAsync("existing-card");

            // Assert (validate that LastOpenedDate was updated)
            ClassicAssert.IsNotNull(capturedData);
            ClassicAssert.AreEqual(1, capturedData.Count);
            ClassicAssert.AreEqual("existing-card", capturedData[0].CardId);

            // Ensure LastOpenedDate was updated
            var updatedCard = capturedData[0];
            ClassicAssert.IsTrue(updatedCard.LastOpenedDate > originalDate);
        }

        #endregion UpdateAsync
    }
}