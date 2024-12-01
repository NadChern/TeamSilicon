using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.FlashcardAdmin
{
    
    /// <summary>
    /// Index Page will return all the data to show the user
    /// </summary>
    public class IndexModel : PageModel
    {
       
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="flashcardService"></param>
        public IndexModel(JsonFileFlashcardService flashcardService)
        {
            FlashcardService = flashcardService;
        }
        
       /// <summary>
       /// Gets data service
       /// </summary>
        public JsonFileFlashcardService FlashcardService { get; }

        /// <summary>
        /// Gets collection of data
        /// </summary>
        public IEnumerable<FlashcardModel> Flashcards { get; private set; }

        /// <summary>
        /// Search term from the search bar
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Retrieves all flashcards and filters them based on the search term, if provided.
        /// </summary>
        public void OnGet()
        {
            // Fetch all flashcards from the service
            var allFlashcards = FlashcardService.GetAllData();

            // Filter flashcards only if a search term is provided
            if (SearchTerm?.Length > 0)
            {
                Flashcards = allFlashcards.Where(card =>
                    card.Question.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase));
                return;
            }

            // Assign all flashcards to the Flashcards property when no search term is present
            Flashcards = allFlashcards;
        }
    }
}