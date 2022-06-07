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

        [JsonIgnore]
        public string StartDateStr
        {
            get { return StartDate.ToString("dd/MM/yyyy"); }
        }

        [JsonIgnore]
        public string EndDateStr
        {
            get { return EndDate.ToString("dd/MM/yyyy"); }
        }
        //   [JsonPropertyName("products")]
        //    public List<Product> Products { get; set; }
    }
}
