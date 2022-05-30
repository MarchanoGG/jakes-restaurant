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
                new Option("Voeg producten toe", this.SelectTheme),
                new Option("Lijst", this.View),
                new Option("Exit", () => Environment.Exit(0)),
            };

            Navigation();
        }
        public void Navigation()
        {
            this.menu = new vMenu(options);
        }
        public void SelectTheme()
        {
            options = new List<Option>();

            if (d_prodCtrl.GetTheme().Count > 0)
            {
                Console.WriteLine("Selecteer een thema om producten toe te voegen:");
            }

            foreach (Thema el in d_prodCtrl.GetTheme())
             {
                options.Add(new Option(el.Name, AddProduct)); 
             }

            options.Add(new Option("Voeg thema toe!", AddTheme));
            options.Add(new Option("Terug", DefaultMenu));


            this.menu = new vMenu(options);
        }

        public void ThemeAction()
        {
            options = new List<Option>
            {
                new Option("Wijzig", this.Edit),
                new Option("Voeg product toe", this.AddProduct),
                new Option("Terug", this.SelectTheme)
              };

            this.menu = new vMenu(options);
        }

        public void AddProduct()
        {
            // Get thema from id
            Thema theme = d_prodCtrl.GetID(1);

            // Some how add the id of the current Theme selected
            List<string> allergens = new List<string>();
            List<string> ingredients = new List<string>();
            List<Product> products = new List<Product>();

            products = d_prodCtrl.GetProducts(1);

            Product p = new Product();

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

            products.Add(p);

            theme.Products = products;

            p.ID = d_prodCtrl.IncrementProductID(theme);
            d_prodCtrl.UpdateList(theme);

            ThemeAction();
        }


        public void AddTheme()
        {
            Thema thema = new Thema();

            Console.WriteLine("Thema naam:");
            thema.Name = Console.ReadLine();

            Console.WriteLine("Start datum:");
            thema.StartDate = DateTime.Parse(Console.ReadLine()); 

            Console.WriteLine("Eind datum:");
            thema.EndDate = DateTime.Parse(Console.ReadLine());

            thema.ID = d_prodCtrl.IncrementID();
            d_prodCtrl.UpdateList(thema);


            SelectTheme();
        }

        public void View()
        {
            options = new List<Option>();

            options.Add(new Option("Terug", DefaultMenu));
         
            foreach (var l in d_prodCtrl.GetTheme())
            {
                options.Add(new Option(l.Name, Edit));
            }

            this.menu = new vMenu(options);
        }
        public void Edit()
        {
            int aID = 1; // Get from parameter
            Thema theme = d_prodCtrl.GetID(aID);

            options = new List<Option>();

            options.Add(new Option(theme.Name, this.Edit));
            options.Add(new Option(theme.StartDate.ToString(), this.Edit));
            options.Add(new Option(theme.EndDate.ToString(), this.Edit));

            Console.WriteLine("Producten:");
            foreach (var el in d_prodCtrl.GetProducts(theme.ID))
            {
                options.Add(new Option(el.Name, this.Edit));
            }

            options.Add(new Option("Terug", this.DefaultMenu));

            this.menu = new vMenu(options);
        }

     
    }
}
