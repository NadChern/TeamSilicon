using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Service class to manage flashcard data stored in a JSON file
    /// </summary>
    public class JsonFileFlashcardService
    {
        /// <summary>
        /// Initializes a new instance of JsonFileFlashcardService class.
        /// </summary>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment,
        /// including the web root path.</param>
        public JsonFileFlashcardService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Gets the web hosting environment, used to access the web root path.
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }


        /// <summary>
        /// Gets the full path to the JSON file containing the flashcard data.
        /// </summary>
        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "flashcards.json");

        /// <summary>
        /// Retrieves all flashcard data from the JSON file.
        /// </summary>
        /// <returns>A collection of FlashcardModel objects.</returns>
        public IEnumerable<FlashcardModel> GetAllData()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<FlashcardModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Retrieves a specific flashcard by its ID.
        /// </summary>
        /// <param name="id">The ID of the flashcard to retrieve.</param>
        /// <returns>The FlashcardModel object with the specified ID, or null if not found.</returns>
        public FlashcardModel GetById(int id)
        {
            return GetAllData().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Get the number of flashcards for a specific category.
        /// </summary>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>The number of flashcards in the given category.</returns>
        public int GetCountByCategoryId(string categoryId)
        {
            return GetAllData().Count(f => f.CategoryId == categoryId);
        }

        /// <summary>
        /// Updates the details of an existing flashcard.
        /// </summary>
        /// <param name="updatedFlashcard">The updated flashcard data.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public bool UpdateFlashcard(FlashcardModel updatedFlashcard)
        {
            // Retrieve all flashcards from JSON file, convert to list 
            var flashcards = GetAllData();

            // Find the flashcard with the specified ID
            var existingFlashcard = flashcards.FirstOrDefault(f => f.Id == updatedFlashcard.Id);
            if (existingFlashcard == null)
            {
                return false; // Flashcard not found
            }

            UpdateFrom(existingFlashcard, updatedFlashcard);
            SaveData(flashcards);
            return true; // Update successful
        }

        /// <summary>
        /// Saves the flashcard data to the JSON file.
        /// </summary>
        /// <param name="flashcards">The collection of flashcards to save.</param>
        private void SaveData(IEnumerable<FlashcardModel> flashcards)
        {
            // Serialize the flashcards to JSON format with indentation.
            var jsonData = JsonSerializer.Serialize(flashcards, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(JsonFileName, jsonData);
        }

        /// <summary>
        /// Updates the attributes of an existing flashcard with new values.
        /// </summary>
        /// <param name="existingFlashcard">The existing flashcard to update.</param>
        /// <param name="updatedFlashcard">The flashcard containing the new values.</param>
        public void UpdateFrom(FlashcardModel existingFlashcard, FlashcardModel updatedFlashcard)
        {
            existingFlashcard.CategoryId = updatedFlashcard.CategoryId;
            existingFlashcard.Question = updatedFlashcard.Question;
            existingFlashcard.Answer = updatedFlashcard.Answer;
            existingFlashcard.DifficultyLevel = updatedFlashcard.DifficultyLevel;
            existingFlashcard.OpenCount = updatedFlashcard.OpenCount;
            existingFlashcard.Url = updatedFlashcard.Url;
        }
    }
}
