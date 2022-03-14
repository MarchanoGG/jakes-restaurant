using System;

namespace JakesRestaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            Authentication.TctlLogin ctlAuth = new Authentication.TctlLogin();
            if (ctlAuth.Login())
            {

                Console.WriteLine("Hello World!");

            }

            Console.WriteLine("Unable to login!");

            return;
        }
    }
}
