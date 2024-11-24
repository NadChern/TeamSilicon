using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


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
        public FlashcardModel GetById(string id)
        {
            return GetAllData().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Creates a new flashcard entry with a unique ID and initializes OpenCount to 0.
        /// </summary>
        /// <param name="flashcard">The flashcard model containing the data to be added.</param>
        /// <returns>The newly created flashcard model with an assigned ID and OpenCount set to 0.</returns>
        public FlashcardModel CreateData(FlashcardModel flashcard)
        {
            // generate a unique ID for the new flashcard
            flashcard.OpenCount = 0;

            // retrieve the existing dataset and add the new flashcard
            var dataset = GetAllData();

            // create new dataset to append new card
            var newDataset = dataset.Append(flashcard);

            // save the updated dataset
            SaveData(newDataset);
            return flashcard;
        }

        /// <summary>
        /// Removes a flashcard with the specified ID from the dataset.
        /// </summary>
        /// <param name="id">The ID of the flashcard to remove.</param>
        /// <returns>True if the flashcard was successfully removed; otherwise, false.</returns>
        public virtual bool RemoveFlashcard(string id)
        {
            // Retrieve all flashcards from JSON file, convert to list for manipulation
            var flashcards = GetAllData().ToList();

            // Find the flashcard with the specified ID
            var flashcardToRemove = flashcards.FirstOrDefault(f => f.Id == id);

            // Flashcard not found
            if (flashcardToRemove == null)
            {
                return false;
            }

            // Remove the flashcard from the list
            flashcards.Remove(flashcardToRemove);

            // Save the updated list back to the JSON file
            SaveData(flashcards);
            return true;
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

            // Flashcard not found
            if (existingFlashcard == null)
            {
                return false;
            }

            UpdateFrom(existingFlashcard, updatedFlashcard);
            SaveData(flashcards);
            return true;
        }


        /// <summary>
        /// Filters flashcards by a specific category.
        /// </summary>
        /// <param name="category"> Category to filter flashcards by.
        /// If null or empty, returns all flashcards.</param>
        /// <returns>An enumerable collection of flashcards that belong to
        /// specified category.</returns>
        public IEnumerable<FlashcardModel> GetFilteredFlashcardsByCategory(string category)
        {
            // Retrieves all flashcards from FlashcardService
            var allFlashcards = GetAllData();

            // If no category is selected, return all flashcards
            if (string.IsNullOrEmpty(category))
            {
                return allFlashcards;
            }

            return allFlashcards.Where(f =>
                f.CategoryId.Equals(category, StringComparison.OrdinalIgnoreCase));
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

        /// <summary>
        /// Validates if the provided URL exists and can be reached by making an HTTP HEAD request
        /// </summary>
        /// <param name="url">The URL to validate</param>
        /// <returns>Returns true if the URL exists, otherwise, false for invalid or unreachable URLs.</returns>
        public virtual async Task<bool> ValidateUrlAsync(string url)
        {
            // Check if URL is null, empty, or contains only white spaces
            if (string.IsNullOrWhiteSpace(url)) return false;

            // Create new instance of HttpClient to make HTTP requests
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Make a HEAD request to avoid downloading the content
                    var request = new HttpRequestMessage(HttpMethod.Head, url);

                    // Send HTTP request asynchronously and await the response
                    var response = await httpClient.SendAsync(request);

                    // Check if the response status code is a success (200-299 range)
                    return response.IsSuccessStatusCode;
                }
                
                catch
                {
                    // Treat any error as a non-existent URL
                    return false;
                }
            }
        }
    }
}