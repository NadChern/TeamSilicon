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

        // Fetches flashcard data from JSON file
        public JsonFileFlashcardService FlashcardService { get; }

        // A collection of flashcards to display, filtered by category if specified.
        // Defaults to an empty collection if no flashcards are available.
        public IEnumerable<FlashcardModel> Flashcards { get; private set; } =
            Enumerable.Empty<FlashcardModel>();

        // Captures CategoryId from query string
        [BindProperty(SupportsGet = true)] 
        public string? CategoryId { get; set; }


        // Fetches and displays flashcards
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
