using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JakesRestaurant
{
    class Program
    {
        private static Authentication.User currentUser;
        static Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();
        static void Main(string[] args)
        {
            Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();
            if (ctlAuth.Login(InsertCredentials("username"), InsertCredentials("password")))
            {

                Console.WriteLine("Hello World!");

            if(Console.ReadLine() != null)
            {
                Console.Clear();
                if (ctlAuth.Login())
                {
                    Console.Clear();
                    Console.WriteLine("User: " + currentUser.Summary());

                    // Main application options

                    Console.ReadKey();
                }
            }

            return;
        }

        static public void SetUser(Authentication.User aUser)
        {
           Program.currentUser = aUser;
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
