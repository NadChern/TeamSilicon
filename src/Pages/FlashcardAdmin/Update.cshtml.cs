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

        // Category selection properties

        /// <summary>
        /// Gets a value indicating whether the "OOP" category is selected.
        /// </summary>
        public bool IsOOPSelected => Flashcard?.CategoryId == "OOP";

        /// <summary>
        /// Gets a value indicating whether the "Python" category is selected.
        /// </summary>
        public bool IsPythonSelected => Flashcard?.CategoryId == "Python";

        /// <summary>
        /// Gets a value indicating whether the "C#" category is selected.
        /// </summary>
        public bool IsCSharpSelected => Flashcard?.CategoryId == "C#";

        /// <summary>
        /// Gets a value indicating whether the "C++" category is selected.
        /// </summary>
        public bool IsCPlusPlusSelected => Flashcard?.CategoryId == "C++";

        /// <summary>
        /// Gets a value indicating whether the "Mobile" category is selected.
        /// </summary>
        public bool IsMobileSelected => Flashcard?.CategoryId == "Mobile";

        /// <summary>
        /// Gets a value indicating whether the "DS" category is selected.
        /// </summary>
        public bool IsDSSelected => Flashcard?.CategoryId == "DS";

        // Difficulty selection properties

        // /// <summary>
        // /// Gets a value indicating whether the "Easy" difficulty level is selected.
        // /// </summary>
        // public bool IsEasySelected => Flashcard?.DifficultyLevel == "Easy";

        // /// <summary>
        // /// Gets a value indicating whether the "Medium" difficulty level is selected.
        // /// </summary>
        // public bool IsMediumSelected => Flashcard?.DifficultyLevel == "Medium";

        // /// <summary>
        // /// Gets a value indicating whether the "Hard" difficulty level is selected.
        // /// </summary>
        // public bool IsHardSelected => Flashcard?.DifficultyLevel == "Hard";

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
