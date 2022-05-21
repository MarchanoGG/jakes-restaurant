using System;
using System.Collections.Generic;

namespace JakesRestaurant.views
{
    internal class vLogin
    {
        private static Authentication.User currentUser;
        private Authentication.User ThisUser { get { return currentUser; } set { currentUser = value; } }

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
            this.ThisUser = null;
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
                new Option("Voornaam:           " + this.ThisUser.FirstName, this.InsertValue, 1),
                new Option("Achternaam:         " + this.ThisUser.Surname, this.InsertValue, 2),
                new Option("Email:              " + this.ThisUser.Email, this.InsertValue, 3),
                new Option("Telefoon nummer:    " + this.ThisUser.Phone, this.InsertValue, 4),
                new Option("Geboorte datum:     " + this.ThisUser.BirthDate, this.InsertValue, 5),
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
        public void UsersList()
        {
            vMain mainmenu = new vMain();

            options = new List<Option> {};

            Authentication.User usr = new Authentication.User();

            foreach (Authentication.User obj in usr.GetUsers())
            {
                options.Add(new Option(obj.ID + ". " + obj.FirstName + " " + obj.Surname, CheckRes));
            }

            options.Add(new Option("Terug", () => mainmenu.Navigation()));
            this.menu = new vMenu(options);
        }
        public void CheckRes()
        {
            vMain mainmenu = new vMain();

            options = new List<Option>
            {
                new Option("Wordt nog aan gewerkt, kies deze optie om terug te gaan naar het menu!", () => mainmenu.Navigation()),
            };
            this.menu = new vMenu(options);
        }

        static public void SetUser(Authentication.User aUser)
        {
            vLogin.currentUser = aUser;
        }

        public Authentication.User GetUser()
        {
            return this.ThisUser;
        }

        private void InsertValue(int idx)
        {

            switch (idx)
            {
                case 1:
                    Console.WriteLine("Voer nieuwe voornaam in:");
                    string FirstName = Console.ReadLine();
                    this.ThisUser.FirstName = FirstName;

                    this.ThisUser.UpdateUser(this.ThisUser);
                    break;
                case 2:
                    Console.WriteLine("Voer nieuwe achternaam in:");
                    string Surname = Console.ReadLine();
                    this.ThisUser.Surname = Surname;

                    this.ThisUser.UpdateUser(this.ThisUser);
                    break;
                case 3:
                    Console.WriteLine("Voer nieuwe email in:");
                    string Email = Console.ReadLine();
                    this.ThisUser.Email = Email;

                    this.ThisUser.UpdateUser(this.ThisUser);
                    break;
                case 4:
                    Console.WriteLine("Voer nieuwe telefoon nummer in:");
                    string Phone = Console.ReadLine();
                    this.ThisUser.Phone = Phone;

                    this.ThisUser.UpdateUser(this.ThisUser);
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

                    this.ThisUser.BirthDate = dt;

                    this.ThisUser.UpdateUser(this.ThisUser);
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
