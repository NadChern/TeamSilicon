using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    
    /// <summary>
    /// Controller for managing flashcards
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FlashcardController: ControllerBase
    {
        
        /// <summary>
        /// Initializes a new instance of FlashcardController class.
        /// </summary>
        /// <param name="flashcardService"></param>
        public FlashcardController(JsonFileFlashcardService flashcardService)
        {
            FlashcardService = flashcardService;
        }
        
        /// <summary>
        /// Gets service responsible for accessing flashcard data
        /// </summary>
        public JsonFileFlashcardService FlashcardService { get; }
        
        /// <summary>
        /// Handles HTTP GET requests to retrieve all flashcards
        /// </summary>
        /// <returns>Collection of FlashcardModel objects representing flashcards</returns>
        [HttpGet]
        public IEnumerable<FlashcardModel> Get()
        {
            return FlashcardService.GetAllData();
        }
    }
}