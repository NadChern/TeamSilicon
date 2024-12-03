using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Service for managing favorite category states stored in local storage.
    /// </summary>
    public class LocalStorageCategoryService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "FavoriteCategories";

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
            var favorites = await GetFavoritesAsync();
            return favorites.Contains(categoryId);
        }
    }
}
