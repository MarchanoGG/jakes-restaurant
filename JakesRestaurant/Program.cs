using System;

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
            Console.WriteLine("Thema: <To be loaded from config>");

            Console.WriteLine("\rDruk op een knop om door te gaan naar de login");

            if (Console.ReadKey().ToString().Length > 0)
            {
                Console.Clear();
                if (ctlAuth.Login())
                {
                    Console.Clear();
                    Console.WriteLine("Gebruiker: " + currentUser.Summary());

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
}
