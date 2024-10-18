using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// FlashcardModel represents a single flashcard with its properties
    /// </summary>
    public class FlashcardModel
    {
        // Primary key Id
        public string Id { get; set; }

        // Id of the category the card belong to
        public string CategoryId { get; set; }

        // Question on the flashcard
        public string Question { get; set; }

        // Answer on the flashcard
        public string Answer { get; set; }

        // Difficulty level of the question
        [JsonPropertyName("Difficulty")] public string DifficultyLevel { get; set; }

        // Link to additional resources related to the flashcard (optional)
        public string Url { get; set; }

        // Returns the string representation of the flashcard
        public override string ToString() => JsonSerializer.Serialize<FlashcardModel>(this);
    }
}
