using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JakesRestaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();
            if (ctlAuth.Login(InsertCredentials("username"), InsertCredentials("password")))
            {

                Console.WriteLine("Hello World!");

            }

            return;
        }

        static string InsertCredentials(string aCredential)
        {
            string res = "";

            Console.WriteLine("Insert " + aCredential + ":");
            res = Console.ReadLine();

            return res;
        }
    }
}
