using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.FlashcardAdmin
{
    
    /// <summary>
    /// Represents Create page 
    /// </summary>
    public class CreateModel : PageModel
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
        /// Initializes a new instance of CreateModel class
        /// </summary>
        /// <param name="flashcardService">Service used to manage flashcard data.</param>
        public CreateModel(JsonFileFlashcardService flashcardService)
        {
            
            // Assign provided service to FlashcardService property
            FlashcardService = flashcardService;
        }

        /// <summary>
        /// Handles HTTP GET requests to initialize Create page.
        /// </summary>
        /// <returns>Create page.</returns>
        public IActionResult OnGet()
        {
            
            // Initialize Flashcard to avoid null reference issues in the form
            Flashcard = new FlashcardModel();
            return Page();
        }

        /// <summary>
        /// Handles HTTP POST requests to create a new flashcard.
        /// </summary>
        /// <returns>
        /// Redirects to the Index page if the creation is successful. 
        /// If the model state is invalid, redisplays the Create page with errors.
        /// </returns>
        public IActionResult OnPost()
        {
            
            // Check if the model state is valid
            if (ModelState.IsValid == false)
            {
                return Page(); 
            }
            
            // Proceed with valid ModelState
            FlashcardService.CreateData(Flashcard);
            return RedirectToPage("/FlashcardAdmin/Index");
        }
    }
}