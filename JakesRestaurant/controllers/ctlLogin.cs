using System;

namespace Authentication
{
    class TctlLogin
    {
        public bool Login() 
        {
            bool res = false;

            User userCheck = new();
            User user = userCheck.CheckCredentials(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord"));

            if (user != null)
            {
                res = true;
                JakesRestaurant.Program.SetUser(user);
                Console.WriteLine("Succesvol aangemeld.");
            }
            else
            {
                Console.WriteLine("Onjuiste gegevens verstrekt!");
            }

            return res;
        }

        static string InsertCredentials(string aCredential)
        {
            Console.WriteLine("\n\r");
            Console.WriteLine("Voer " + aCredential + " in:");

            return Console.ReadLine();
        }
    }
}
