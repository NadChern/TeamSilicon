using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace ContosoCrafts.WebSite.Services
{
    public class LocalStorageFlashcardService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "FlashcardsData";

        public LocalStorageFlashcardService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public class FlashcardData
        {
            public string CardId { get; set; }
            public DateTime LastOpenedDate { get; set; }
        }

        // Retrieve all stored flashcard metadata
        public async Task<List<FlashcardData>> GetAllAsync()
        {
            return await _localStorage.GetItemAsync<List<FlashcardData>>(StorageKey) ?? new List<FlashcardData>();
        }

        // Update the lastOpenedDate for a specific card
        public async Task UpdateAsync(string cardId)
        {
            var flashcards = await GetAllAsync();
            var existingCard = flashcards.FirstOrDefault(f => f.CardId == cardId);

            if (existingCard != null)
            {
                existingCard.LastOpenedDate = DateTime.UtcNow;
            }
            else
            {
                flashcards.Add(new FlashcardData { CardId = cardId, LastOpenedDate = DateTime.UtcNow });
            }

            await _localStorage.SetItemAsync(StorageKey, flashcards);
        }

        // Get the lastOpenedDate for a specific card
        public async Task<DateTime?> GetLastOpenedDateAsync(string cardId)
        {
            var flashcards = await GetAllAsync();
            return flashcards.FirstOrDefault(f => f.CardId == cardId)?.LastOpenedDate;
        }
    }
}