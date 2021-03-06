using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace management
{
   public class Product
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("themeId")]
        public int ThemeID { get; set; }

        [JsonPropertyName("theme")]
        public Theme Theme { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("allergens")]
        public List<string> Allergens { get; set; }

        [JsonPropertyName("ingredients")]
        public List<string> Ingredients { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("is_alcohol")]
        public bool Alcohol { get; set; }

    }
}
