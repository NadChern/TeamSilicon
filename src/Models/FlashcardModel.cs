using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// FlashcardModel represents a single flashcard with its properties
    /// </summary>
    public class FlashcardModel
    {
        // Primary key Id for flashcard
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Id of the category the card belong to
        public string CategoryId { get; set; }

        // Question on the flashcard
        [Required(ErrorMessage = "*Required")] 
        public string Question { get; set; }

        // Answer for the question on the flashcard
        [Required(ErrorMessage = "*Required")] 
        public string Answer { get; set; }

       // Difficulty level of the question (1- Easy, 2 - Medium, 3 - Hard)
        [JsonPropertyName("Difficulty")]
        [Required(ErrorMessage = "*Required")]
        [Range(1, 3, ErrorMessage = "Please enter a value between 1 and 3.")]
        public int DifficultyLevel { get; set; }
       
        // Link to additional resources related to the flashcard (optional)
        [RegularExpression(@"(https?:\/\/.*)", 
            ErrorMessage = "Please enter secured urls starts with \"https://\". ")]
        public string Url { get; set; }

        // Number of times flashcard has been opened (flipped)
        [JsonPropertyName("OpenCount")]
        [Required(ErrorMessage = "*Required")]
        public int OpenCount { get; set; }

        /// <summary>
        /// Converts current FlashcardModel object into JSON string representation
        /// </summary>
        /// <returns>Json string representation of FlashcardModel object</returns>
        public override string ToString() => JsonSerializer.Serialize<FlashcardModel>(this);

    }
}
