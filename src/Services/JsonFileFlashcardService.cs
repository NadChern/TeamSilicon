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
        {
            return GetAllData().First(x => x.Id == id);
        }

        public void UpdateFlashcard(FlashcardModel updatedFlashcard)
        {
            Console.WriteLine("UpdateFlashcard called with ID: " + updatedFlashcard?.Id);

            var flashcards = GetAllData();
            var existingFlashcard = flashcards.FirstOrDefault(f => f.Id == updatedFlashcard.Id);

            if (existingFlashcard == null)
            {
                Console.WriteLine("Flashcard with ID " + updatedFlashcard.Id + " not found.");
                return;
            }

            Console.WriteLine("Updating flashcard properties.");
            UpdateFrom(existingFlashcard, updatedFlashcard);
            SaveData(flashcards);
        }

        public void UpdateFrom(FlashcardModel existingFlashcard, FlashcardModel updatedFlashcard)
        {
            Console.WriteLine("Updating properties for flashcard ID: " + existingFlashcard.Id);
            existingFlashcard.CategoryId = updatedFlashcard.CategoryId;
            existingFlashcard.Question = updatedFlashcard.Question;
            existingFlashcard.Answer = updatedFlashcard.Answer;
            existingFlashcard.DifficultyLevel = updatedFlashcard.DifficultyLevel;
        }

        private void SaveData(IEnumerable<FlashcardModel> flashcards)
        {
            var jsonData = JsonSerializer.Serialize(flashcards, new JsonSerializerOptions
            {
                WriteIndented = true // This ensures the JSON is pretty-printed
            });

            File.WriteAllText(JsonFileName, jsonData);
        }




    }
}
