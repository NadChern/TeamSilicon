using System.Text.Json;
using System.Text.Json.Serialization;


namespace ContosoCrafts.WebSite.Models
{
    public class FlashcardModel
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        
        [JsonPropertyName("Difficulty")]
        public string DifficultyLevel { get; set; }
        
        public string Url { get; set; }
        public override string ToString() => JsonSerializer.Serialize<FlashcardModel>(this);
    }
}
