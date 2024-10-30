using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Pages.Flashcards
{
    /// <summary>
    /// FlashcardsModel handles fetching and displaying flashcards data,
    /// including filtering by category ID.
    /// </summary>
    public class FlashcardsModel : PageModel
    {
        /// <summary>
        /// Initializes a new instance of the FlashcardModel with service
        /// </summary>
        /// <param name="flashcardService"></param>
        public FlashcardsModel(JsonFileFlashcardService flashcardService)
        {
            FlashcardService = flashcardService;
        }

        /// <summary>
        /// Fetches flashcard data from JSON file
        /// </summary>
        public JsonFileFlashcardService FlashcardService { get; }

        /// <summary>
        /// A collection of flashcards to display, filtered by category if specified.
        /// Defaults to an empty collection if no flashcards are available.
        /// </summary>
        public IEnumerable<FlashcardModel> Flashcards { get; private set; } =
            Enumerable.Empty<FlashcardModel>();

        /// <summary>
        /// Captures CategoryId from query string
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string? CategoryId { get; set; }

        /// <summary>
        /// Fetches and displays flashcards
        /// </summary>
        /// <param name="categoryId"></param>
        public void OnGet(string? categoryId)
        {
            // Assign categoryId from query string to the property
            CategoryId = categoryId;

            // Retrieve all flashcards from the service
            var allFlashcards = FlashcardService.GetAllData();

            // Filter flashcards by category 
            if (!string.IsNullOrEmpty(CategoryId))
            {
                Flashcards = allFlashcards
                    .Where(f => f.CategoryId.Equals(CategoryId,
                        System.StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                // if no category ID is provided, display all flashcards
                Flashcards = allFlashcards;
            }
        }
    }
}
