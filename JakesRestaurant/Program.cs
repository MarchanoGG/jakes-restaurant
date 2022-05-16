using System;
using JakesRestaurant.views;
using JakesRestaurant.controllers;
namespace JakesRestaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            ctlInitialise initialise = new ctlInitialise();

            vLogin loginView = new vLogin();

            loginView.Navigation();
        }
    }
}
