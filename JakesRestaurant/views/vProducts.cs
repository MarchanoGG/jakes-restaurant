using System;
using controllers;
using management;
using System.Collections.Generic;

namespace JakesRestaurant.views
{
    internal class vProducts
    {
        private TctlProducts d_prodCtrl;

        public static List<Option> options;
        public vMenu menu { get; set; }
        public List<vMenu> breadcrumbs { get; set; }
        public vProducts()
        {
            d_prodCtrl = new TctlProducts();
            DefaultMenu();
        }
        void DefaultMenu()
        {
            Console.WriteLine("Products");
            options = new List<Option>
            {
                new Option("Voeg toe", this.Add),
                new Option("Lijst", this.View),
                new Option("Exit", () => Environment.Exit(0)),
            };

            Navigation();
        }
        public void Navigation()
        {
            this.menu = new vMenu(options);
        }
        public void Add()
        {
            Product p  = new Product(); 
            FillFromInput(ref p);
            p.ID = d_prodCtrl.IncrementID();
            d_prodCtrl.UpdateList(p);

            Navigation();
        }
        public void View()
        {
            options = new List<Option>();

            options.Add(new Option("Terug", DefaultMenu));
         
            foreach (var l in d_prodCtrl.GetProducts())
            {
                options.Add(new Option(l.Name, Edit));
            }

            this.menu = new vMenu(options);
        }
        public void Edit()
        {
            int aID = 4; // Get from parameter
            Product p = d_prodCtrl.GetID(aID);

            FillFromInput(ref p);

            // If success than update product
            d_prodCtrl.UpdateList(p);

            // Go back to View navigation
            View();

        }

        public void FillFromInput(ref Product p)
        { 
            Console.WriteLine("Naam:");
            p.Name = Console.ReadLine();
        }
    }
}
