using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Service for managing favorite category states stored in local storage.
    /// </summary>
    public class LocalStorageCategoryService
    {
        // Service to interact with local storage.
        private readonly ILocalStorageService _localStorage;
        
        // Key used to store and retrieve favorite categories in local storage
        private const string StorageKey = "FavoriteCategories";

        /// <summary>
        /// Initializes a new instance of the LocalStorageCategoryService class
        /// </summary>
        /// <param name="localStorage"></param>
        public LocalStorageCategoryService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        /// <summary>
        /// Retrieve all favorite category IDs from local storage.
        /// </summary>
        /// <returns>A HashSet of favorite category IDs.</returns>
        public async Task<HashSet<string>> GetFavoritesAsync()
        {
            return await _localStorage.GetItemAsync<HashSet<string>>(StorageKey) ?? new HashSet<string>();
        }

        /// <summary>
        /// Add a category to the list of favorites.
        /// </summary>
        /// <param name="categoryId">The ID of the category to add.</param>
        public async Task AddToFavoritesAsync(string categoryId)
        {
            // Retrieve the current list of favorite categories
            var favorites = await GetFavoritesAsync();
            
            favorites.Add(categoryId);
            await _localStorage.SetItemAsync(StorageKey, favorites);
        }

        /// <summary>
        /// Remove a category from the list of favorites.
        /// </summary>
        /// <param name="categoryId">The ID of the category to remove.</param>
        public async Task RemoveFromFavoritesAsync(string categoryId)
        {
            // Retrieve the current list of favorite categories
            var favorites = await GetFavoritesAsync();
            
            favorites.Remove(categoryId);
            await _localStorage.SetItemAsync(StorageKey, favorites);
        }

        /// <summary>
        /// Check if a category is in the list of favorites.
        /// </summary>
        /// <param name="categoryId">The ID of the category to check.</param>
        /// <returns>True if the category is a favorite; otherwise, false.</returns>
        public async Task<bool> IsFavoriteAsync(string categoryId)
        {
            // Retrieve the current list of favorite categories
            var favorites = await GetFavoritesAsync();
            
            return favorites.Contains(categoryId);
        }
    }
}