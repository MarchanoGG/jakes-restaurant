using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

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
        public bool CreateUser()
        {
            User user = new User();

            Console.WriteLine("\n\rMaak een nieuwe gebruiker");

            return user.CreateCredentials(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord"));
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
