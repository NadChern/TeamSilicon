using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileFlashcardService
    {
        public JsonFileFlashcardService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "flashcards.json");

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

        public FlashcardModel GetById(string id)
        /// <summary>
        /// GetById gets Flashcard based on id
        /// </summary>
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

        public bool UpdateFlashcard(FlashcardModel updatedFlashcard)
        /// <summary>
        /// Updates Flashcard
        /// </summary>
        {
            var flashcards = GetAllData();
            var existingFlashcard = flashcards.FirstOrDefault(f => f.Id == updatedFlashcard.Id);

            if (existingFlashcard == null)
            {
                return false; // Flashcard not found
            }

            UpdateFrom(existingFlashcard, updatedFlashcard);
            SaveData(flashcards);

            return true; // Update successful
        }

        private void SaveData(IEnumerable<FlashcardModel> flashcards)
        /// <summary>
        /// Saves flashcard to flashcards.json
        /// </summary>
        {
            var jsonData = JsonSerializer.Serialize(flashcards, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(JsonFileName, jsonData);
        }

        public void UpdateFrom(FlashcardModel existingFlashcard, FlashcardModel updatedFlashcard)
        /// <summary>
        /// Updates each attribute
        /// </summary>
        {
            existingFlashcard.CategoryId = updatedFlashcard.CategoryId;
            existingFlashcard.Question = updatedFlashcard.Question;
            existingFlashcard.Answer = updatedFlashcard.Answer;
            existingFlashcard.DifficultyLevel = updatedFlashcard.DifficultyLevel;
        }
    }
}
