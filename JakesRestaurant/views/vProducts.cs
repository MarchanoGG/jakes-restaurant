using System;
using controllers;
using management;
using System.Collections.Generic;
using System.Linq;

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
            int aID = 2; // Get from parameter
            Product p = d_prodCtrl.GetID(aID);

            FillFromInput(ref p);

            // If success than update product
            d_prodCtrl.UpdateList(p);

            // Go back to View navigation
            View();

        }

        public void FillFromInput(ref Product p)
        {
            List<string> allergens = new List<string>();
            List<string> ingredients = new List<string>();

            Console.WriteLine("Naam:");
            p.Name = Console.ReadLine();

            Console.WriteLine("Prijs:");
            p.Price = double.Parse(Console.ReadLine());

            Console.WriteLine("Allergiën: (Gebruik ; na ieder item)");
          
            allergens = Console.ReadLine().Split(';').ToList();
            p.Allergens = allergens;

            Console.WriteLine("Ingrediënten: (Gebruik ; na ieder item)");
       
            ingredients = Console.ReadLine().Split(';').ToList();
            p.Ingredients = ingredients;

            Console.WriteLine("Zit er alcohol in?");

            Console.WriteLine("1) Ja");
            Console.WriteLine("2) Nee");


            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
                p.Alcohol = true;
            else
                p.Alcohol = false;


        }
    }
}
