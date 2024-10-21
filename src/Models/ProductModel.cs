using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
      
        [JsonPropertyName("img")]
        public string Image { get; set; }

        [StringLength (maximumLength: 33, MinimumLength = 1, ErrorMessage = 
            "The Title should have a length of more than {2} and less than {1}")]
        public string Title { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);

    }
}