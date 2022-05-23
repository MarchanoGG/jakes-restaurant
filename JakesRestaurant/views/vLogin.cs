using System;
using System.Collections.Generic;

namespace JakesRestaurant.views
{
    internal class vLogin
    {
        private static Authentication.User currentUser;
        static Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();
        public string title = "=== Log in ===";
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
            this.menu = new vMenu(options, title);
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
                new Option("Voornaam:           " + vLogin.currentUser.FirstName, this.InsertValue, 1),
                new Option("Achternaam:         " + vLogin.currentUser.Surname, this.InsertValue, 2),
                new Option("Email:              " + vLogin.currentUser.Email, this.InsertValue, 3),
                new Option("Telefoon nummer:    " + vLogin.currentUser.Phone, this.InsertValue, 4),
                new Option("Geboorte datum:     " + vLogin.currentUser.BirthDate, this.InsertValue, 5),
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

        private void InsertValue(int idx)
        {

            switch (idx)
            {
                case 1:
                    Console.WriteLine("Voer nieuwe voornaam in:");
                    string FirstName = Console.ReadLine();
                    vLogin.currentUser.FirstName = FirstName;

                    vLogin.currentUser.UpdateUser(vLogin.currentUser);
                    break;
                case 2:
                    Console.WriteLine("Voer nieuwe achternaam in:");
                    string Surname = Console.ReadLine();
                    vLogin.currentUser.Surname = Surname;

                    vLogin.currentUser.UpdateUser(vLogin.currentUser);
                    break;
                case 3:
                    Console.WriteLine("Voer nieuwe email in:");
                    string Email = Console.ReadLine();
                    vLogin.currentUser.Email = Email;

                    vLogin.currentUser.UpdateUser(vLogin.currentUser);
                    break;
                case 4:
                    Console.WriteLine("Voer nieuwe telefoon nummer in:");
                    string Phone = Console.ReadLine();
                    vLogin.currentUser.Phone = Phone;

                    vLogin.currentUser.UpdateUser(vLogin.currentUser);
                    break;
                case 5:
                    Console.WriteLine("Voer uw geboorte datum in:");

                    string line = Console.ReadLine();
                    DateTime dt;
                    while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        Console.WriteLine("Invalid date, please retry");
                        line = Console.ReadLine();
                    }

                    vLogin.currentUser.BirthDate = dt;

                    vLogin.currentUser.UpdateUser(vLogin.currentUser);
                    break;
                default:
                    break;
            }

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
