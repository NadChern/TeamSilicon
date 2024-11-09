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
        private readonly JsonFileFlashcardService _flashcardService;

        public FlashcardModel Flashcard { get; private set; }

        public DeleteModel(JsonFileFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        public IActionResult OnGet(string id)
        {
            Flashcard = _flashcardService.GetById(id);
            if (Flashcard == null)
            {
                return RedirectToPage("/FlashcardAdmin/Index");
            }

            return Page();
        }

        public IActionResult OnPost(string id)
        {
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
