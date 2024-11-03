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
        public int Id { get; set; }

        // Id of the category the card belong to
        public string CategoryId { get; set; }

        // Question on the flashcard
        [Required(ErrorMessage = "*Required")]
        public string Question { get; set; }

        // Answer for the question on the flashcard
        public string Answer { get; set; }

        // Difficulty level of the question (Easy, Medium, Hard)
        [JsonPropertyName("Difficulty")] public string DifficultyLevel { get; set; }

        // Link to additional resources related to the flashcard (optional)
        public string Url { get; set; }
        
        // Number of times flashcard has been opened (flipped)
        public int OpenCount { get; set; } = 0;

        /// <summary>
        /// Converts current FlashcardModel object into JSON string representation
        /// </summary>
        /// <returns>Json string representation of FlashcardModel object</returns>
        public override string ToString() => JsonSerializer.Serialize<FlashcardModel>(this);
        
    }
}
