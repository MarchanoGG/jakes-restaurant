using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Restaurant
{
    class doOpeningTimes
    {
        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("opened")]
        public bool Opened { get; set; }

        [JsonPropertyName("startingHours")]
        public int StartingHours { get; set; }

        [JsonPropertyName("startingMinutes")]
        public int StartingMinutes { get; set; }

        [JsonPropertyName("closingHours")]
        public int ClosingHours { get; set; }

        [JsonPropertyName("closingMinutes")]
        public int ClosingMinutes { get; set; }

        public string Summary()
        {
            if (Opened)
                return "Opened on " + Day + " from: " + new TimeSpan(StartingHours, StartingMinutes, 0) + " to: " + new TimeSpan(ClosingHours, ClosingMinutes, 0);
            else
                return "Closed on " + Day;
        }
    }
}
