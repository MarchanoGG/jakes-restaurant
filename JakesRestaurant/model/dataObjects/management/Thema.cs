using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace management
{
    public class Theme
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }
    }
}
