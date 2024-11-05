using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

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
            // Ensure the service is not null, otherwise throw an exception.
            FlashcardService = flashcardService ?? 
                               throw new ArgumentNullException(nameof(flashcardService), 
                                   "Flashcard service cannot be null.");
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
        /// Handles HTTP GET requests to retrieve all flashcards and assigns them
        /// to Flashcards property
        /// </summary>
        public void OnGet()
        {
            // Fetch all flashcards from service, store them in Flashcards property
            Flashcards = FlashcardService.GetAllData();
        }
    }
}