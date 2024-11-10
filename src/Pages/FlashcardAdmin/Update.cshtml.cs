using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;


namespace ContosoCrafts.WebSite.Pages.FlashcardAdmin
{
    /// <summary>
    /// Represents Update page
    /// </summary>
    public class UpdateModel : PageModel
    {
        /// <summary>
        /// Gets service responsible for accessing and updating flashcard data
        /// </summary>
        public JsonFileFlashcardService FlashcardService { get; }

        /// <summary>
        /// Gets or sets the flashcard being updated
        /// </summary>
        [BindProperty] //Binds form data to this property on POST requests
        public FlashcardModel Flashcard { get; set; }

        /// <summary>
        /// Initializes a new instance of UpdateModel class
        /// </summary>
        /// <param name="flashcardService">Service used to manage flashcard data.</param>
        public UpdateModel(JsonFileFlashcardService flashcardService)
        {
            // Assign provided service to FlashcardService property
            FlashcardService = flashcardService;
        }

        /// <summary>
        /// Indicates whether the flashcard was successfully loaded
        /// </summary>
        public bool IsFlashcardLoaded { get; set; }

        /// <summary>
        /// Indicates whether the flashcard was successfully updated
        /// </summary>
        public bool IsFlashcardUpdated { get; set; }


        /// <summary>
        /// Handles HTTP GET requests to retrieve a specific flashcard by ID
        /// </summary>
        /// <param name="id">ID of the flashcard to be retrieved.</param>
        public void OnGet(string id)
        {
            // Retrieve the flashcard using the provided ID.
            Flashcard = FlashcardService.GetById(id);
            if (Flashcard == null)
            {
                IsFlashcardLoaded = false;
                return;
            }

            IsFlashcardLoaded = true;
        }

        /// <summary>
        /// Handles HTTP POST requests to update the flashcard
        /// </summary>
        /// <returns>
        /// Redirects to the Read page if the update is successful. 
        /// If the model state is invalid, redisplays the Update page with errors.
        /// </returns>
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                FlashcardService.UpdateFlashcard(Flashcard);
                IsFlashcardUpdated = true;
                return RedirectToPage("/FlashcardAdmin/Index",
                    new { id = Flashcard.Id }); // Redirect details page
            }

            IsFlashcardUpdated = false;
            return Page(); // Redisplay with errors
        }
    }
}
