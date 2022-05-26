using System;
using JakesRestaurant.views;
using JakesRestaurant.controllers;
using controllers;

namespace JakesRestaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            ctlInitialise initialise = new ctlInitialise();
            ctlMain ctlMain = new ctlMain();
            vLogin loginView = new vLogin();

            loginView.Navigation();
        }
    }
}
