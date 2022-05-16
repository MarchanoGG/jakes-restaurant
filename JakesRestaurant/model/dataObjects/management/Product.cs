using System.Text.Json.Serialization;

namespace management
{
    class Product
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}
