using System;

namespace JakesRestaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            model.controllers.TctlLogin ctlAuth = new model.controllers.TctlLogin();
            if (ctlAuth.Login())
            {

                Console.WriteLine("Hello World!");

            }

            Console.WriteLine("Unable to login!");

            return;
        }
    }
}
