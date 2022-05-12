using System;
using System.Collections.Generic;
using JakesRestaurant.controllers;

namespace JakesRestaurant.views
{
    internal class vLogin
    {
        private static Authentication.User currentUser;
        static Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();

        public static List<Option> options;
        public ctlMenu menu { get; set; }
        public List<ctlMenu> breadcrumbs { get; set; }
        public vLogin()
        {
            Console.WriteLine("Login");
            options = new List<Option>
            {
                new Option("Inloggen", this.Login),
                new Option("Aanmelden", this.Create),
                new Option("Afsluiten", () => Environment.Exit(0)),
            };
        }
        public void Navigation()
        {
            this.menu = new ctlMenu(options);
        }
        public void Login()
        {
            if (ctlAuth.Login(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord")))
            {
                ctlMain mainmenu = new ctlMain();
                mainmenu.Navigation();
            }
        }
        public void Create()
        {
            if (ctlAuth.CreateUser(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord")))
            {
                Console.WriteLine("Gebruiker is aangemaakt");
            }
            else
            {
                Console.WriteLine("Kan de gebruiker niet aanmaken!");
            }
            this.menu = new ctlMenu(options);
        }

        static public void SetUser(Authentication.User aUser)
        {
            vLogin.currentUser = aUser;
        }

        static public Authentication.User GetUser()
        {
            return vLogin.currentUser;
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
