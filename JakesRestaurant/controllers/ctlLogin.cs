using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Authentication
{
    public class TctlLogin
    {
        public bool Login(string aUsername, string aPassword) 
        {
            bool res = false;

            User userCheck = new User();
            User user = userCheck.CheckCredentials(aUsername, aPassword);

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
        public bool CreateUser(string aUsername, string aPassword)
        {
            User user = new User();

            Console.WriteLine("\n\rMaak een nieuwe gebruiker");

            return user.CreateCredentials(aUsername, aPassword);
        }
    }
}
