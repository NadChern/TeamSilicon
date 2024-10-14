using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Flashcards
{
    public class FlashcardsModel : PageModel
    {
        public FlashcardsModel(JsonFileFlashcardService flashcardService)
        {
            FlashcardService = flashcardService;
        }

        public JsonFileFlashcardService FlashcardService { get; }
        public IEnumerable<FlashcardModel> Flashcards { get; private set; }
        
        // Manage selected card and answer visibility
        public string? SelectedCardId { get; private set; }
        public bool ShowAnswer { get; private set; }

        public void OnGet()
        {
            // Load all flashcards when the page is first loaded
            Flashcards = FlashcardService.GetAllData();
        }

        public void OnPost(string cardId)
        {
            // Manage the toggling of the card
            if (SelectedCardId == cardId)
            {
                ShowAnswer = !ShowAnswer;
            }
            else
            {
                SelectedCardId = cardId;
                ShowAnswer = true;
            }

            // Reload flashcards to maintain the state
            Flashcards = FlashcardService.GetAllData();
        }
    }
}