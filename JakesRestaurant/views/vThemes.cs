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

        private static Theme d_currentTheme { get; set; }
        private TctlThemes d_themeCtrl;

        public vThemes()
        {
            d_themeCtrl = new TctlThemes();
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

        public void AddTheme()
        {
            d_currentTheme = new Theme();
            Console.WriteLine("Thema naam:");
            d_currentTheme.Name = Console.ReadLine();

            Console.WriteLine("Start datum:");
            d_currentTheme.StartDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Eind datum:");
            d_currentTheme.EndDate = DateTime.Parse(Console.ReadLine());

            d_currentTheme.ID = d_themeCtrl.IncrementID();
            d_themeCtrl.UpdateList(d_currentTheme);

            Navigation();
        }
    }
}
