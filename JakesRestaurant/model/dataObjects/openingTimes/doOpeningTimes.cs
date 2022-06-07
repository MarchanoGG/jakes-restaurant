using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace controllers
{
    class doOpeningTimes
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("dayofweek")]
        public DayOfWeek Dayofweek { get; set; }

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
                return "Open op " + Day + " vanaf: " + new TimeSpan(StartingHours, StartingMinutes, 0) + " tot: " + new TimeSpan(ClosingHours, ClosingMinutes, 0);
            else
                return "Gesloten op " + Day;
        }
    }
}
