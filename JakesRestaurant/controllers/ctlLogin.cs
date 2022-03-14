﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Authentication
{
    class TctlLogin
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "auth.conf.txt");

        public bool Login(string aUserName, string aPassword) 
        {
            bool res = false;

            List<TdoUser> users = JakesRestaurant.JsonFileReader.Read<TdoUser>(path);

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