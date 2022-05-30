using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using controllers;

namespace Authentication
{
    public class ctlLogin
    {
        private ctlUsers controller = new ctlUsers();
        public bool Login(string aUsername, string aPassword) 
        {
            bool res = false;

            doUser user = controller.CheckCredentials(aUsername, aPassword);

            if (user != null)
            {
                res = true;
                JakesRestaurant.Program.MyUser = user;
                Console.WriteLine("Succesvol aangemeld.");
            }
            else
            {
                Console.WriteLine("Onjuiste gegevens verstrekt!");
            }

            return res;
        }
    }
}
