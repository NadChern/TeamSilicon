using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
            FlashcardService = flashcardService ?? 
                               throw new ArgumentNullException(nameof(flashcardService), 
                                   "Flashcard service cannot be null.");
        }
        
        // Data Service
        public JsonFileFlashcardService FlashcardService { get; }

        // Collection of the Data
        public IEnumerable<FlashcardModel> Flashcards { get; private set; }

        /// <summary>
        /// REST OnGet
        /// Return all the data
        /// </summary>
        public void OnGet()
        {
            Flashcards = FlashcardService.GetAllData();
        }
    }
}