using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FileManagement
{
    public static class JsonFileManager
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

        public static void Write<T>(string aPath, List<T> ls)
        {
            string json = JsonSerializer.Serialize(ls);
            //Console.WriteLine(json);
            File.WriteAllText(aPath, json);
            Console.WriteLine("write done");
        }

        public static void UpdateList<T>(string aPath, T obj)
        {
            List<T> ls = FileManagement.JsonFileManager.ReadList<T>(aPath);

            int index = ls.IndexOf(obj);

            if (index != -1)
            {
                ls[index] = obj;
            }
            else
            {
                ls.Add(obj);
            }
            Write<T>(aPath, ls);

        }

    }
}