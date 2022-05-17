using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JakesRestaurant.model.dataObjects.reservation
{
    internal class DiningTable
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "diningtable.json");
        
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("places")]
        public int Places { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        public bool Add(int Places, string Description = "")
        {
            bool result = false;
            int id = 0;
            List<DiningTable> list = ReadList<DiningTable>(path);
            DiningTable obj =  new DiningTable
            {
                ID = id,
                Places = Places,
                Description = Description
            };
            list.Add(obj);
            this.WriteList(path, list);

            return result;
        }
        public static T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);

            T res = JsonSerializer.Deserialize<T>(text);

            return res;
        }

        public static List<T> ReadList<T>(string filePath)
        {
            List<T> res = new List<T>();
            string text = File.ReadAllText(filePath);

            T[] arr = JsonSerializer.Deserialize<T[]>(text);

            foreach (T obj in arr)
            {
                res.Add(obj);
            }

            return res;
        }
        public void WriteList<T>(string filePath, List<T> aList)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(aList));
        }
    }
}
