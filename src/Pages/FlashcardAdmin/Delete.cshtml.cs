using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.FlashcardAdmin
{
    
    /// <summary>
    /// Page model for deleting a flashcard
    /// </summary>
    public class DeleteModel : PageModel
    {
        
        /// <summary>
        /// Service for handling flashcard data operations.
        /// </summary>
        private readonly JsonFileFlashcardService _flashcardService;

        /// <summary>
        /// Service for handling flashcard data operations.
        /// </summary>
        public FlashcardModel Flashcard { get; private set; }

        /// <summary>
        /// Constructor to initialize the DeleteModel with a flashcard service.
        /// </summary>
        /// <param name="flashcardService">Service for accessing flashcard data.</param>
        public DeleteModel(JsonFileFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        /// <summary>
        /// Handles the GET request to retrieve a flashcard by ID for deletion.
        /// </summary>
        /// <param name="id">ID of the flashcard to retrieve.</param>
        /// <returns>Redirects to Index if flashcard is not found, otherwise returns the current page.</returns>
        public IActionResult OnGet(string id)
        {
            
            // Retrieve the flashcard by ID using the service.
            Flashcard = _flashcardService.GetById(id);
            
            if (Flashcard == null)
            {
                return RedirectToPage("/FlashcardAdmin/Index");
            }

            return Page();
        }

        /// <summary>
        /// Handles the POST request to delete a flashcard by ID.
        /// </summary>
        /// <param name="id">ID of the flashcard to delete.</param>
        /// <returns>Redirects to Index on success, otherwise reloads the current page with an error message.</returns>
        public IActionResult OnPost(string id)
        {
            
            // Attempt to delete the flashcard by ID using the service.
            var success = _flashcardService.RemoveFlashcard(id);
            
            if (success)
            {
                return RedirectToPage("/FlashcardAdmin/Index");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while trying to delete the flashcard.");
            return Page();
        }
    }
}