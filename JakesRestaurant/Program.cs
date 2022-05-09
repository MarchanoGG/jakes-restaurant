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
                if (ctlAuth.Login())
                {
                    Console.Clear();
                    Console.WriteLine("Gebruiker: " + currentUser.Summary());

                    // Main application options
                    ctlMain mainmenu = new ctlMain();
                    mainmenu.Navigation();

                    Console.ReadKey();
                }
            }
            if(key == ConsoleKey.D2)
            {
                if (ctlAuth.CreateUser())
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
    }
}
