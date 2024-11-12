using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.FlashcardAdmin
{
    
    /// <summary>
    /// Represents Read page
    /// </summary>
    public class ReadModel : PageModel
    {
        
        /// <summary>
        /// Gets service responsible for accessing flashcard data
        /// </summary>
        public JsonFileFlashcardService FlashcardService { get; }

        /// <summary>
        /// Gets the flashcard data that is retrieved by OnGet method
        /// </summary>
        public FlashcardModel Flashcard { get; private set; }

        /// <summary>
        /// Initializes a new instance of ReadModel class
        /// </summary>
        /// <param name="flashcardService"></param>
        public ReadModel(JsonFileFlashcardService flashcardService)
        {
            FlashcardService = flashcardService;
        }

        /// <summary>
        /// A flag to indicate if the flashcard has been successfully loaded.
        /// </summary>
        public bool IsFlashcardLoaded { get; set; }

        /// <summary>
        /// Handles the HTTP GET request to retrieve a specific flashcard by its ID
        /// </summary>
        /// <param name="id">ID of the flashcard to be retrieved</param>
        public void OnGet(string id)
        {
            
            // Assign provided service to FlashcardService property
            Flashcard = FlashcardService.GetById(id);
            
            if (Flashcard == null)
            {
                IsFlashcardLoaded = false;
                return;
            }

            IsFlashcardLoaded = true;
        }
    }
}