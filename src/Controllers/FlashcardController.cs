using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlashcardController: ControllerBase
    {
        public FlashcardController(JsonFileFlashcardService flashcardService)
        {
            FlashcardService = flashcardService;
        }
        
        public JsonFileFlashcardService FlashcardService { get; }
        
        [HttpGet]
        public IEnumerable<FlashcardModel> Get()
        {
            return FlashcardService.GetAllData();
        }
    }
}
