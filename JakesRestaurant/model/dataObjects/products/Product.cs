using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json.Serialization;

namespace JakesRestaurant.model.dataObjects.products
{
    internal class Product
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "products.json");

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("is_alcohol")]
        public string IsAlcohol { get; set; }

        [JsonPropertyName("allergens")]
        public string Allergens { get; set; }

        [JsonPropertyName("ingredients")]
        public string Ingredients { get; set; }
    }
}
