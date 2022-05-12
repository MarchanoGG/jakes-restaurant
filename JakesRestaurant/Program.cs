using System;
using JakesRestaurant.controllers;

namespace JakesRestaurant
{
    class Program
    {
        private static Authentication.User currentUser;
        static Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();
        static void Main(string[] args)
        {
            Console.WriteLine("Jakes retaurant");
            Console.WriteLine("Wijnhaven 107, 3011 WN in Rotterdam");
            Console.WriteLine("Thema: <To be loaded from config> \r");

            Console.WriteLine("\rDruk op een '1' om door te gaan naar de login");
            Console.WriteLine("\rDruk op een '2' om een gebruiker aan te maken");

            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.D1)
            {
                Console.Clear();
                if (ctlAuth.Login(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord")))
                {
                    Console.Clear();
                    Console.WriteLine("Inglogd als: " + GetUser().Summary());

                    // Main application options
                    ctlMain mainmenu = new ctlMain();
                    mainmenu.Navigation();

                    Console.ReadKey();
                }
            }
            if(key == ConsoleKey.D2)
            {
                if (ctlAuth.CreateUser(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord")))
                {
                    Console.WriteLine("Gebruiker is aangemaakt");
                }
                else
                {
                    Console.WriteLine("Kan de gebruiker niet aanmaken!");
                }
                Console.ReadKey();
            }

            Console.ReadKey();
            return;
        }

        static public void SetUser(Authentication.User aUser)
        {
           Program.currentUser = aUser;
        }

        static public Authentication.User GetUser()
        {
            return Program.currentUser; 
        }
        private static string InsertCredentials(string aCredential)
        {
            Console.WriteLine("\n\r");
            Console.WriteLine("Voer " + aCredential + " in:");

            string credentials = "";
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (aCredential == "wachtwoord")
                {
                    if (key == ConsoleKey.Backspace && credentials.Length > 0)
                    {
                        Console.Write("\b \b");
                        credentials = credentials[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        credentials += keyInfo.KeyChar;
                    }
                }
                else
                {
                    if (key == ConsoleKey.Backspace && credentials.Length > 0)
                    {
                        Console.Write("\b \b");
                        credentials = credentials[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        for (int i = 0; i < credentials.Length; i++)
                        {
                            Console.Write("\b \b");
                        }
                        credentials += keyInfo.KeyChar;
                        Console.Write(credentials);
                    }
                }
            } while (key != ConsoleKey.Enter);

            return credentials;
        }
    }
}
