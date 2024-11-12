using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    
    /// <summary>
    /// Service class to manage category data stored in a JSON file
    /// </summary>
    public class JsonFileCategoryService
    {
        
        /// <summary>
        /// Initializes a new instance of JsonFileCategoryService class
        /// </summary>
        /// <param name="webHostEnvironment">
        /// Provides information about the web hosting environment, including the web root path</param>
        public JsonFileCategoryService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Gets the web hosting environment, used to access the web root path.
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Gets the full path to the JSON file containing the category data.
        /// </summary>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// Retrieves all category data from the JSON file.
        /// </summary>
        /// <returns>A collection of CategoryModel objects.</returns>
        public IEnumerable<CategoryModel> GetAllData()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<CategoryModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Retrieves the color associated with a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The color of the specified category.</returns>
        public string GetCategoryColorById(string id)
        {
            
            // Retrieves all categories
            var products = GetAllData();
            
            // Finds the category with the specified ID
            var product = products.FirstOrDefault(x => x.Id.Equals(id));
            
            // Returns the category color
            return product.CategoryColor;
        }

        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data"></param>
        public CategoryModel UpdateData(CategoryModel data)
        {
            
            // Retrieves all categories
            var products = GetAllData();
            
            // Finds the category to update
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            
            if (productData == null)
            {
                return null;
            }

            // Update the data to the new passed in values
            productData.Title = data.Title;
            productData.Image = data.Image;
            SaveData(products);
            return productData;
        }

        /// <summary>
        /// Save All products data to storage
        /// </summary>
        private void SaveData(IEnumerable<CategoryModel> products)
        {
            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<CategoryModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Create a new product using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns></returns>
        public CategoryModel CreateData()
        {
            
            // Create a new CategoryModel instance with default values
            var data = new CategoryModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Enter Title",
                Image = "",
            };

            // Get the current set, and append the new record to it
            // because IEnumerable does not have Add
            var dataSet = GetAllData();
            dataSet = dataSet.Append(data);
            SaveData(dataSet);
            return data;
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns></returns>
        public CategoryModel DeleteData(string id)
        {
            
            // Retrieves all categories
            var dataSet = GetAllData();
            
            // Finds the category to delete.
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));
            
            // Creates a new dataset excluding the deleted category.
            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);
            SaveData(newDataSet);
            return data;
        }
    }
}