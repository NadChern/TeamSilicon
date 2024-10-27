using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.FlashcardAdmin
{
    public class ReadModel : PageModel
    {
        public JsonFileFlashcardService FlashcardService { get; }

        public FlashcardModel Flashcard { get; private set; }

        public ReadModel(JsonFileFlashcardService flashcardService)
        {
            FlashcardService = flashcardService;
        }

        public void OnGet(string id)
        {
            Flashcard = FlashcardService.GetById(id);

        }
    }
}
