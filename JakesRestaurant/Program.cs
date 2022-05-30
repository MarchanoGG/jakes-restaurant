using System;
using JakesRestaurant.views;
using JakesRestaurant.controllers;
using controllers;

namespace JakesRestaurant
{
    class Program
    {
        private static Authentication.doUser currentUser;
        public static Authentication.doUser MyUser { get { return currentUser; } set { currentUser = value; } }
        static void Main(string[] args)
        {
            ctlInitialise initialise = new ctlInitialise();
            ctlMain ctlMain = new ctlMain();
            vLogin loginView = new vLogin();

            loginView.Navigation();
        }
    }
}
