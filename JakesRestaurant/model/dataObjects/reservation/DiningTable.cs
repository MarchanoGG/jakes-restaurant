using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace reservation
{
    internal class DiningTable
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("places")]
        public int Places { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        public string Summary() =>  $"{Description} is {Status}, hier is plek voor: {Places}.";
    }
}
