using System;
using System.Collections.Generic;
using System.IO;

namespace Authentication
{
    class TctlLogin
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "auth.conf.txt");

        public bool Login(string aUserName, string aPassword) 
        {
            bool res = false;

            List<TdoUser> users = JakesRestaurant.JsonFileReader.ReadList<TdoUser>(path);

            TdoUser myUser = users.Find(i => i.Username == aUserName);

            if (myUser != null)
            {
                if (myUser.Password == aPassword)
                {
                    res = true;
                    Console.WriteLine("Successfully logged in.");
                }
                else
                {
                    Console.WriteLine("Invalid credentials provided!");
                }
            }
            else
            {
                Console.WriteLine("Invalid credentials provided!");
            }

            return res;
        }
    }
}
