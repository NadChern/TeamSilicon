using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Service for managing flashcard data stored in local storage
    /// </summary>
    public class LocalStorageFlashcardService
    {
        // Dependency for interacting with local storage
        private readonly ILocalStorageService _localStorage;
        
        // Key used to store and retrieve flashcard data in local storage
        private const string StorageKey = "FlashcardsData";

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalStorageFlashcardService"/> class.
        /// </summary>
        /// <param name="localStorage">The local storage service instance.</param>
        public LocalStorageFlashcardService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        /// <summary>
        /// Represents the metadata for a flashcard stored in local storage.
        /// </summary>
        public class FlashcardData
        {
            /// <summary>
            /// Gets or sets the unique identifier for the flashcard.
            /// </summary>
            public string CardId { get; set; }
            
            /// <summary>
            /// Gets or sets the date and time when the flashcard was last opened.
            /// </summary>
            public DateTime LastOpenedDate { get; set; }
        }

        /// <summary>
        /// Retrieves all stored flashcard metadata from local storage.
        /// </summary>
        /// <returns>A list of "FlashcardData" objects, or an empty
        /// list if no data is found.</returns>
        public virtual async Task<List<FlashcardData>> GetAllAsync()
        {
            return await _localStorage.GetItemAsync<List<FlashcardData>>(StorageKey) ?? new List<FlashcardData>();
        }

        /// <summary>
        /// Updates the last opened date for a specific flashcard. 
        /// If the flashcard does not exist, it will be added to the storage.
        ///</summary>
        /// <param name="cardId">The unique identifier of the flashcard to update.</param>
        public async Task UpdateAsync(string cardId)
        {
            // Retrieve all flashcards from local storage.
            var flashcards = await GetAllAsync();
            
            // Attempt to find the flashcard by its ID.
            var existingCard = flashcards.FirstOrDefault(f => f.CardId == cardId);

            // If the flashcard does not exist, add it to the list.
            if (existingCard == null)
            {
                flashcards.Add(new FlashcardData { CardId = cardId, LastOpenedDate = DateTime.UtcNow });
            }

            // If the flashcard exists, update its last opened date.
            if (existingCard != null)
            {
                existingCard.LastOpenedDate = DateTime.UtcNow;
            }
            
            // Save the updated list of flashcards back to local storage.
            await _localStorage.SetItemAsync(StorageKey, flashcards);
        }
    }
}