using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FileManagement
{
    public static class JsonFileReader
    {
        public static T Read<T>(string aFilePath)
        {
            string text = File.ReadAllText(aFilePath);

            T res = JsonSerializer.Deserialize<T>(text);

            return res;
        }

        public static List<T> ReadList<T>(string aFilePath)
        {
            List<T> res = new List<T>();
            string text = File.ReadAllText(aFilePath);

            T[] arr = JsonSerializer.Deserialize<T[]>(text);

            foreach (T obj in arr)
            {
                res.Add(obj);
            }

            return res;
        }


        public static void WriteToFile<T>(T aObj, string aFilePath)
        {
            string contents = aObj.ToString();
            File.WriteAllText(aFilePath, contents);
        }
    }
}