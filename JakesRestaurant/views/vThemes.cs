using System;
using controllers;
using management;
using System.Collections.Generic;
using System.Linq;


namespace JakesRestaurant.views
{
    internal class vThemes
    {
        public static List<Option> options;
        public vMenu menu { get; set; }

        public vThemes()
        {
            DefaultMenu();
        }

        void DefaultMenu()
        {
            options = new List<Option>();
            options.Add(new Option("Voeg thema toe", AddTheme));
            options.Add(new Option("Exit", () => Environment.Exit(0)));

            Navigation();
        }

        public void Navigation()
        {
            this.menu = new vMenu(options);
        }

        static public void AddTheme()
        {

        }
    }
}
