using System;
using System.Collections.Generic;

namespace JakesRestaurant.views
{
    internal class vLogin
    {
        static Authentication.ctlLogin ctlAuth = new Authentication.ctlLogin();
        static Authentication.ctlUsers ctlUsers = new Authentication.ctlUsers();

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
            this.menu = new vMenu(options);//, title);
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
            Program.MyUser = null;
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
                new Option("Voornaam:           " + Program.MyUser.FirstName, this.InsertValue, 1),
                new Option("Achternaam:         " + Program.MyUser.Surname, this.InsertValue, 2),
                new Option("Email:              " + Program.MyUser.Email, this.InsertValue, 3),
                new Option("Telefoon nummer:    " + Program.MyUser.Phone, this.InsertValue, 4),
                new Option("Geboorte datum:     " + Program.MyUser.BirthDate, this.InsertValue, 5),
                new Option("Nieuw wachtwoord invullen", this.InsertValue, 6),
                new Option("Terug", () => mainmenu.Navigation()),
            };
            this.menu = new vMenu(options);
        }
        public void Create()
        {
            if (ctlUsers.CreateCredentials(InsertCredentials("gebruikersnaam"), InsertCredentials("wachtwoord")))
            {
                Console.WriteLine("Gebruiker is aangemaakt");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Kan de gebruiker niet aanmaken!");
                Console.ReadLine();
            }
            this.menu = new vMenu(options);
        }
        public void UsersList()
        {
            vMain mainmenu = new vMain();

            options = new List<Option> {};

            Authentication.doUser usr = new Authentication.doUser();

            foreach (Authentication.doUser obj in ctlUsers.GetUsers())
            {
                options.Add(new Option(obj.ID + ". " + obj.FirstName + " " + obj.Surname, GetUserDetails, obj.ID));
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
        public void GetUserDetails(int ID)
        {
            vMain mainmenu = new vMain();

            Authentication.doUser usr = new Authentication.doUser();
            Authentication.doUser foundUser = null;

            foreach (Authentication.doUser obj in ctlUsers.GetUsers())
            {
                if (obj.ID == ID) 
                {
                    foundUser = obj;
                }
            }

            if (foundUser != null)
            {
                options = new List<Option>
                {
                    new Option("Voornaam:           " + foundUser.FirstName),
                    new Option("Achternaam:         " + foundUser.Surname),
                    new Option("Email:              " + foundUser.Email),
                    new Option("Telefoon nummer:    " + foundUser.Phone),
                    new Option("Geboorte datum:     " + foundUser.BirthDate)
                };
            }
            options.Add(new Option("Terug naar gebruikers lijst", UsersList));
            this.menu = new vMenu(options);
        }

        private void InsertValue(int idx)
        {

            switch (idx)
            {
                case 1:
                    Console.WriteLine("Voer nieuwe voornaam in:");
                    string FirstName = Console.ReadLine();
                    Program.MyUser.FirstName = FirstName;

                    ctlUsers.UpdateUser(Program.MyUser);
                    break;
                case 2:
                    Console.WriteLine("Voer nieuwe achternaam in:");
                    string Surname = Console.ReadLine();
                    Program.MyUser.Surname = Surname;

                    ctlUsers.UpdateUser(Program.MyUser);
                    break;
                case 3:
                    Console.WriteLine("Voer nieuwe email in:");
                    string Email = Console.ReadLine();
                    Program.MyUser.Email = Email;

                    ctlUsers.UpdateUser(Program.MyUser);
                    break;
                case 4:
                    Console.WriteLine("Voer nieuwe telefoon nummer in:");
                    string Phone = Console.ReadLine();
                    Program.MyUser.Phone = Phone;

                    ctlUsers.UpdateUser(Program.MyUser);
                    break;
                case 5:
                    Console.WriteLine("Voer uw geboorte datum in (dd/MM/yyyy):");

                    string line = Console.ReadLine();
                    DateTime dt;
                    while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        Console.WriteLine("Invalide datum, let op het formaat moet zijn: dd/MM/yyyy!");
                        line = Console.ReadLine();
                    }

                    Program.MyUser.BirthDate = dt;

                    ctlUsers.UpdateUser(Program.MyUser);
                    break;
                case 6:
                    string NewPassword = InsertCredentials("wachtwoord");
                    string ConfirmNewPassword = InsertCredentials("wachtwoord opnieuw");

                    while (NewPassword != ConfirmNewPassword)
                    {
                        NewPassword = InsertCredentials("wachtwoord");
                        ConfirmNewPassword = InsertCredentials("wachtwoord opnieuw");
                    }

                    Program.MyUser.Password = ctlUsers.HashString(NewPassword);

                    ctlUsers.UpdateUser(Program.MyUser);
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

                if (aCredential == "wachtwoord" || aCredential == "wachtwoord opnieuw")
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
