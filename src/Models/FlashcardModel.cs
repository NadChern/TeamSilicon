using System.Text.Json;


namespace ContosoCrafts.WebSite.Models
{
    public class FlashcardModel
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public override string ToString() => JsonSerializer.Serialize<FlashcardModel>(this);
    }
}
