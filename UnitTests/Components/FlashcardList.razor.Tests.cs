using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Bunit;
using NUnit.Framework;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Components;
using NUnit.Framework.Legacy;
using Bunit.TestDoubles;

namespace UnitTests.Pages
{
    public class FlashcardListTests : BunitTestContext
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
            // Register the required services for FlashcardList
            Services.AddSingleton<JsonFileFlashcardService>(TestHelper.FlashcardService);
            Services.AddSingleton<JsonFileCategoryService>(TestHelper.CategoryService);
        }

        #endregion TestSetup

        /// <summary>
        /// Tests if the FlashcardList renders valid content
        /// </summary>
        [Test]
        public void FlashcardList_Valid_Default_Should_Return_Content()
        {
            // Arrange

            // Act
            var page = RenderComponent<FlashcardList>();

            // Get the rendered markup
            var result = page.Markup;

            // Assert
            Assert.That(result.Contains("What is a Python dictionary and how do you access its elements?"), Is.EqualTo(true));
        }

        /// <summary>
        /// Tests if toggling a card increments the OpenCount
        /// </summary>
        [Test]
        public void FlashcardList_ToggleCard_Should_Increment_OpenCount()
        {
            // Arrange
            Services.AddSingleton<JsonFileFlashcardService>(TestHelper.FlashcardService);

            var page = RenderComponent<FlashcardList>();

            // Act
            page.Instance.ToggleCard("e0264da2-8c97-426a-8af2-0fb1bb64c243");

            var updatedFlashcard = TestHelper.FlashcardService.GetById("e0264da2-8c97-426a-8af2-0fb1bb64c243");

            // Assert
            ClassicAssert.AreEqual(4, updatedFlashcard.OpenCount); // OpenCount incremented from 3 to 4
        }

        /// <summary>
        /// Tests that toggling a non-existent card ID does not change the OpenCount of other flashcards
        /// </summary>
        [Test]
        public void FlashcardList_ToggleCard_Invalid_Id_Should_Not_Change_Other_Flashcard_OpenCount()
        {
            // Arrange
            Services.AddSingleton<JsonFileFlashcardService>(TestHelper.FlashcardService);

            var page = RenderComponent<FlashcardList>();

            // Act
            // Get the original OpenCount of a flashcard
            var flashcardId = "e0264da2-8c97-426a-8af2-0fb1bb64c243";
            var originalOpenCount = TestHelper.FlashcardService.GetById(flashcardId).OpenCount;

            // Toggle a non-existent card
            page.Instance.ToggleCard("non-existent-id");

            // Fetch the OpenCount again for the same flashcard
            var updatedOpenCount = TestHelper.FlashcardService.GetById(flashcardId).OpenCount;

            // Assert
            // Verify that the OpenCount of the specific flashcard has not changed
            Assert.That(updatedOpenCount, Is.EqualTo(originalOpenCount));
        }


        [Test]
        public void FlashcardList_ToggleCard_Three_Times_Should_Increment_OpenCount_Three_Times()
        {
            // Arrange
            Services.AddSingleton<JsonFileFlashcardService>(TestHelper.FlashcardService);

            var page = RenderComponent<FlashcardList>();
            var flashcardID = "e01073ce-be37-464a-aa8d-cc7e80579815";

            // Act
            // Toggle Card 3 times
            page.Instance.ToggleCard(flashcardID);
            page.Instance.ToggleCard(flashcardID);
            page.Instance.ToggleCard(flashcardID);

            var updatedFlashcard = TestHelper.FlashcardService.GetById(flashcardID);

            // Assert
            ClassicAssert.AreEqual(4, updatedFlashcard.OpenCount); // OpenCount incremented from 1 to 4
        }

        /// <summary>
        /// Tests that the IsFlipped method returns true with valid card ID
        /// </summary>
        [Test]
        public void IsFlipped_Valid_CardId_Should_Return_True()
        {
            // Arrange
            var page = RenderComponent<FlashcardList>();
            var flashcardID = "e0264da2-8c97-426a-8af2-0fb1bb64c243";

            // Simulate state
            page.Instance.selectedCardId = flashcardID;
            page.Instance.showAnswer = true;

            // Act
            var result = page.Instance.IsFlipped(flashcardID);

            // Assert
            ClassicAssert.AreEqual(true, result);
        }

        /// <summary>
        /// Tests if GetFilteredFlashcards returns only flashcards matching the valid category ID.
        /// </summary>
        [Test]
        public void GetFilteredFlashcards_Valid_CategoryId_Should_Return_Correct_Flashcards()
        {
            // Arrange
            var page = RenderComponent<FlashcardList>();
            page.Instance.categoryId = "OOP";

            // Act
            var result = page.Instance.GetFilteredFlashcards();

            // Assert
            Assert.That(result.All(f => f.CategoryId == "OOP"), Is.EqualTo(true)); // Verify all have CategoryId "OOP"
        }

        /// <summary>
        /// Tests that ShouldDisplayFlippedLabel returns false when the card is flipped.
        /// </summary>
        [Test]
        public void ShouldDisplayFlippedLabel_When_IsFlipped_Is_True_Should_Return_False()
        {
            // Arrange
            var page = RenderComponent<FlashcardList>();

            // Act
            var result = page.Instance.ShouldDisplayFlippedLabel(true, 1);

            // Assert
            ClassicAssert.AreEqual(false, result);
        }

        /// <summary>
        /// Tests that CanShowMoreInfoLink returns true when the card is flipped
        /// </summary>
        [Test]
        public void CanShowMoreInfoLink_When_IsFlipped_Is_True_Should_Return_True()
        {
            // Arrange
            var page = RenderComponent<FlashcardList>();

            // Act
            var result = page.Instance.CanShowMoreInfoLink(true, "https://example.com");

            // Assert
            ClassicAssert.AreEqual(true, result);
        }

        /// <summary>
        /// Tests that the categoryId is updated correctly when navigating to a specific category URL
        /// </summary>
        [Test]
        public void OnParametersSet_Should_Update_CategoryId()
        {
            // Arrange
            Services.AddSingleton<JsonFileCategoryService>(TestHelper.CategoryService);
            Services.AddSingleton<JsonFileFlashcardService>(TestHelper.FlashcardService);

            var navigationManager = Services.GetRequiredService<FakeNavigationManager>();
            navigationManager.NavigateTo("http://localhost/Flashcards/OOP");

            var page = RenderComponent<FlashcardList>();

            // Act
            // Bunit automatically invokes OnParametersSet when rendering
            var result = page.Instance;

            // Assert
            ClassicAssert.AreEqual("OOP", result.categoryId); // Check if categoryId was set correctly
        }

        /// <summary>
        /// Verifies that clicking a card toggles the "flipped" class correctly
        /// </summary>
        [Test]
        public void FlipClass_After_Two_Clicks_Should_Return_False()
        {
            // Arrange
            var page = RenderComponent<FlashcardList>(); // Render the FlashcardList component

            // Find the card for a different flashcard
            var cardId = "4cce8136-84c3-4e69-abfb-51fedae8432b";
            var cardElement = page.Find($"div[data-id='{cardId}']"); // Find the card by ID

            // Act
            cardElement.Click(); // Simulate the first click to toggle to "flipped"
            var firstFlipClass = cardElement.GetAttribute("class");

            cardElement.Click(); // Simulate the second click to toggle back
            var secondFlipClass = cardElement.GetAttribute("class");

            // Assert
            Assert.That(firstFlipClass.Contains("flipped"), Is.EqualTo(true));
            Assert.That(secondFlipClass.Contains("flipped"), Is.EqualTo(false));
        }

        /// <summary>
        /// Tests that clicking a card shows the "More Info" link
        /// </summary>
        [Test]
        public void FlashcardList_Click_Card_Should_Have_MoreInfo_As_A_Link()
        {
            // Arrange
            var page = RenderComponent<FlashcardList>(); // Render the FlashcardList component

            // Use the specified flashcard ID and URL
            var cardId = "e01073ce-be37-464a-aa8d-cc7e80579815";
            var expectedUrl = "https://www.w3schools.com/python/python_dictionaries.asp";

            // Act
            var cardElement = page.Find($"div[data-id='{cardId}']"); // Find the card by ID
            cardElement.Click(); // Simulate clicking the card (flipping to the answer side)

            // Locate the "More Info" link
            var moreInfoLink = page.Find($"a[href='{expectedUrl}']");

            // Assert
            Assert.That(moreInfoLink, Is.Not.Null);
            Assert.That(moreInfoLink.GetAttribute("href"), Is.EqualTo(expectedUrl));
        }

        /// <summary>
        /// Tests that deleting all cards from a category will display no flashcards message
        /// </summary>
        [Test]
        public void FlashcardList_Empty_Category_Should_Display_No_Flashcards_Message()
        {
            // Arrange
            var flashcardService = TestHelper.FlashcardService;

            var cardIdToDelete = "18dabdbd-c1af-4e7c-88e2-3e860720cbc3"; // Mobile card
            flashcardService.RemoveFlashcard(cardIdToDelete);

            // Render the FlashcardList component
            var page = RenderComponent<FlashcardList>();

            // Act
            // Simulate navigating to the "Mobile" category
            var navigationManager = Services.GetRequiredService<FakeNavigationManager>();
            navigationManager.NavigateTo("http://localhost/Flashcards/Mobile");
            page.Render();

            // Assert
            var result = page.Markup;
            Assert.That(result.Contains("No flashcards found with selected Category"), Is.EqualTo(true));
        }
    }
}