using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Authentication;
using management;

namespace reservation
{
    internal class Reservations
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        //[JsonPropertyName("userid")]
        //public int UserId { get; set; }

        [JsonPropertyName("user")]
        public doUser User { get; set; }

        [JsonPropertyName("products")]
        public List<Product> ListProducts { get; set; }

        [JsonPropertyName("numberguests")]
        public int NumberGuests { get; set; }

        [JsonPropertyName("diningtable")]
        public DiningTable DiningTable { get; set; }

        [JsonPropertyName("createdatetime")]
        public DateTime CreateDateTime { get; set; }

        [JsonPropertyName("duedatetime")]
        public DateTime DueDateTime { get; set; }
    }
}
