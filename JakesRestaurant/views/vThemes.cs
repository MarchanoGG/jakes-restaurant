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
            d_currentTheme = new Theme();
            DefaultMenu();
        }

        void DefaultMenu()
        {
            options = new List<Option>();
            options.Add(new Option("Voeg thema toe", AddTheme));
            options.Add(new Option("Lijst van themas", ViewThemes));
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
            DateTime startDate;

            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    d_currentTheme.StartDate = startDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Probeer opnieuw!");
                }
            }


            Console.WriteLine("Eind datum:");
            DateTime endDate;

            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out endDate))
                {
                    d_currentTheme.EndDate = endDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Probeer opnieuw!");
                }
            }

            d_currentTheme.ID = d_themeCtrl.IncrementID();
            d_themeCtrl.UpdateList(d_currentTheme);

            Navigation();
        }

        public void ViewThemes()
        {
            List<Theme> themes = new List<Theme>();
            themes = d_themeCtrl.GetThemes();

            options = new List<Option>();
            options.Add(new Option("Terug", DefaultMenu));

            foreach (var el in themes)
            {
                options.Add(new Option($"ID: " + el.ID + " - " + el.Name, Edit, el.ID));
            }

            Navigation();
        }

        public void Edit(int aID)
        {
            d_currentTheme = d_themeCtrl.GetByID(aID);

            if (d_currentTheme != null)
            {
                options = new List<Option>();
                options.Add(new Option("Terug", ViewThemes));
                options.Add(new Option(d_currentTheme.Name, FieldName));
                options.Add(new Option(d_currentTheme.StartDate.ToString(), FieldStartDate));
                options.Add(new Option(d_currentTheme.EndDate.ToString(), FieldEndDate));

                options.Add(new Option("Verwijder", this.Delete));


                Navigation();
            }
            else
            {

                Console.WriteLine("Kan thema niet vinden!");
                ViewThemes();
            }
        }


        private void FieldName()
        {
            if (d_currentTheme != null)
            {
                Console.WriteLine("Naam:");
                d_currentTheme.Name = Console.ReadLine();

                d_themeCtrl.UpdateList(d_currentTheme);

                Edit(d_currentTheme.ID);
            }
        }

        private void FieldStartDate()
        {
            Console.WriteLine("Start datum:");
            DateTime startDate;

            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    d_currentTheme.StartDate = startDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Probeer opnieuw!");
                }
            }
            d_themeCtrl.UpdateList(d_currentTheme);

            Edit(d_currentTheme.ID);
        }
        private void FieldEndDate()
        {
            Console.WriteLine("Eind datum:");
            DateTime endDate;

            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out endDate))
                {
                    d_currentTheme.EndDate = endDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Probeer opnieuw!");
                }
            }
            d_themeCtrl.UpdateList(d_currentTheme);

            Edit(d_currentTheme.ID);
        }

        public void Delete()
        {
            d_themeCtrl.Delete(d_currentTheme.ID);
            ViewThemes();
        }

    }
}