using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Restaurant
{
    class ctlOpeningTimes
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "times.json");

        public List<doOpeningTimes> GetOpeningTimes()
        {
            List<doOpeningTimes> existingUsers = ReadList<doOpeningTimes>(path);

            return existingUsers;
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
