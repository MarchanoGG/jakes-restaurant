using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JakesRestaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();
            if (ctlAuth.Login(InsertCredentials("username"), InsertCredentials("password")))
            {

                Console.WriteLine("Hello World!");

            }

            return;
        }

        static string InsertCredentials(string aCredential)
        {
            string res = "";

            Console.WriteLine("Insert " + aCredential + ":");
            res = Console.ReadLine();

            return res;
        }
    }

    public static class JsonFileReader
    {
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
    }
}
