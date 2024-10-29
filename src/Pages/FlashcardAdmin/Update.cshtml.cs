using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContosoCrafts.WebSite.Pages.FlashcardAdmin
{
    public class UpdateModel : PageModel
    {
        public JsonFileFlashcardService FlashcardService { get; }

        [BindProperty]
        public FlashcardModel Flashcard { get; set; }

        public UpdateModel(JsonFileFlashcardService flashcardService)
        {
            FlashcardService = flashcardService;
        }

        // Category selection properties
        public bool IsOOPSelected => Flashcard?.CategoryId == "OOP";
        public bool IsPythonSelected => Flashcard?.CategoryId == "Python";
        public bool IsCSharpSelected => Flashcard?.CategoryId == "C#";
        public bool IsCPlusPlusSelected => Flashcard?.CategoryId == "C++";
        public bool IsMobileSelected => Flashcard?.CategoryId == "Mobile";
        public bool IsDSSelected => Flashcard?.CategoryId == "DS";

        // Difficulty selection properties
        public bool IsEasySelected => Flashcard?.DifficultyLevel == "Easy";
        public bool IsMediumSelected => Flashcard?.DifficultyLevel == "Medium";
        public bool IsHardSelected => Flashcard?.DifficultyLevel == "Hard";

        public bool IsFlashcardLoaded { get; set; } // For testing purposes
        public bool IsFlashcardUpdated { get; set; } // For testing purposes

        public void OnGet(string id)
        {
            Flashcard = FlashcardService.GetById(id);
            if (Flashcard == null)
            {
                IsFlashcardLoaded = false;
                return;
            }
            IsFlashcardLoaded = true;
        }

        public IActionResult OnPost()
        {       
            if (ModelState.IsValid)
            {
                FlashcardService.UpdateFlashcard(Flashcard);
                IsFlashcardUpdated = true;
                return RedirectToPage("/FlashcardAdmin/Read", new { id = Flashcard.Id }); // Redirect details page
            }
            IsFlashcardUpdated = false;
            return Page(); // Redisplay with errors
         
        }
    }
}
