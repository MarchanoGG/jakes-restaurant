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
            Console.WriteLine("Jakes Restaurant");
            Console.WriteLine("Wijnhaven 107, 3011 WN in Rotterdam");
            Console.WriteLine("Thema: <Insert theme here from config or something>\n\r");

            Console.WriteLine("Druk op een knop om door te gaan naar de login..");

            if(Console.ReadLine() != null)
            {
                Console.Clear();
                Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();
                if (ctlAuth.Login(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord")))
                {
                    Console.Clear();
                    Console.WriteLine("Hallo wereld!");
                }
            }

            return;
        }

        static string InsertCredentials(string aCredential)
        {
            string res = "";

            Console.WriteLine("Voer " + aCredential + " in:");
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
