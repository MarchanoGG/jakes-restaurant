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
        
        [JsonPropertyName("comment")]
        public string Comment { get; set; }
        
        [JsonPropertyName("totalcosts")]
        public double TotalCosts { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("reservecode")]
        public string ReserveCode { get; set; }

        [JsonPropertyName("theme")]
        public Theme Theme { get; set; }
        [JsonIgnore]
        public string DueDateTimeStr
        {
            get { return DueDateTime.ToString("dd/MM/yyyy HH:mm"); }
        }
        [JsonIgnore]
        public string CreateDateTimeStr
        {
            get { return CreateDateTime.ToString("dd/MM/yyyy HH:mm"); }
        }
        [JsonIgnore]
        public string ProductSummary
        {
            get {
                return String.Join("; ", ListProducts.Select(m => m.Name).ToArray());
            }
        }
    }
}
