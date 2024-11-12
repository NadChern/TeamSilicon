using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// CategoryModel represents a single category of the flashcards 
    /// with its properties
    /// </summary>
    public class CategoryModel
    {
        // Primary key Id for the category
        public string Id { get; set; }
        
        // Path to category's image
        [JsonPropertyName("img")]
        public string Image { get; set; }

        // Title of the category
        [StringLength (maximumLength: 33, MinimumLength = 1, ErrorMessage = 
            "The Title should have a length of more than {2} and less than {1}")]
        public string Title { get; set; }

        // Color assigned to the category of the flashcards
        public string CategoryColor { get; set; }
        
        /// <summary>
        /// Converts current CategoryModel object into JSON string representation
        /// </summary>
        /// <returns>Json string representation of CategoryModel object</returns>
        public override string ToString() => JsonSerializer.Serialize<CategoryModel>(this);
    }
}