using System;
using System.Collections.Generic;

namespace JakesRestaurant.views
{
    internal class vLogin
    {
        private static Authentication.User currentUser;
        static Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();

        public static List<Option> options;
        public vMenu menu { get; set; }
        public List<vMenu> breadcrumbs { get; set; }
        public vLogin()
        {
            options = new List<Option>
            {
                new Option("Inloggen", this.Login),
                new Option("Aanmelden", this.Create),
                new Option("Afsluiten", () => Environment.Exit(0)),
            };
        }
        public void Navigation()
        {
            this.menu = new vMenu(options);
        }
        public void Login()
        {
            if (ctlAuth.Login(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord")))
            {
                vMain mainmenu = new vMain();
                mainmenu.Navigation();
            }
        }
        public void BackToLogin()
        {
            vLogin.currentUser = null;
            options = new List<Option>
            {
                new Option("Inloggen", this.Login),
                new Option("Aanmelden", this.Create),
                new Option("Afsluiten", () => Environment.Exit(0)),
            };
            this.menu = new vMenu(options);
        }
        public void UpdateProfile()
        {
            vMain mainmenu = new vMain();
            
            options = new List<Option>
            {
                new Option("Voornaam:       " + vLogin.currentUser.FirstName, this.InsertFirstName),
                new Option("Achternaam:     " + vLogin.currentUser.Surname, this.InsertSurname),
                new Option("Terug", () => mainmenu.Navigation()),
            };
            this.menu = new vMenu(options);
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
            this.menu = new vMenu(options);
        }

        static public void SetUser(Authentication.User aUser)
        {
            vLogin.currentUser = aUser;
        }

        static public Authentication.User GetUser()
        {
            return vLogin.currentUser;
        }

        private void InsertFirstName()
        {
            Console.WriteLine("Voer nieuwe voornaam in:");
            string newVal = Console.ReadLine();
            vLogin.currentUser.FirstName = newVal;

            vLogin.currentUser.UpdateUser(vLogin.currentUser);

            UpdateProfile();
        }

        private void InsertSurname()
        {
            Console.WriteLine("Voer nieuwe achternaam in:");
            string newVal = Console.ReadLine();
            vLogin.currentUser.Surname = newVal;

            vLogin.currentUser.UpdateUser(vLogin.currentUser);

            UpdateProfile();
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
