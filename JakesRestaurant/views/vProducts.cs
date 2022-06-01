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
        private TctlThemes d_themeCtrl;

        public static List<Option> options;
        public vMenu menu { get; set; }

        public static Product d_currentProduct { get; set; }
        public vProducts()
        {
            d_prodCtrl = new TctlProducts();
            d_themeCtrl = new TctlThemes();
            DefaultMenu();
        }
        void DefaultMenu()
        {
            Console.WriteLine("Products");
            options = new List<Option>();
            
                if (d_themeCtrl.GetThemes() != null)            
                options.Add(new Option("Voeg producten toe", this.AddProduct));
                else
                options.Add(new Option("Voeg eerst een thema toe!"));

            options.Add(new Option("Lijst", this.View));
            options.Add(new Option("Exit", () => Environment.Exit(0)));
   
            Navigation();
        }
        public void Navigation()
        {
            this.menu = new vMenu(options);
        }


        public void AddProduct()
        {
            // Some how add the id of the current Theme selected
            List<string> allergens = new List<string>();
            List<string> ingredients = new List<string>();

            d_currentProduct = new Product();

            Console.WriteLine("Naam:");
            d_currentProduct.Name = Console.ReadLine();

            Console.WriteLine("Prijs:");
            d_currentProduct.Price = double.Parse(Console.ReadLine());

            Console.WriteLine("Allergiën: (Gebruik ; na ieder item)");

            allergens = Console.ReadLine().Split(';').ToList();
            d_currentProduct.Allergens = allergens;

            Console.WriteLine("Ingrediënten: (Gebruik ; na ieder item)");

            ingredients = Console.ReadLine().Split(';').ToList();
            d_currentProduct.Ingredients = ingredients;

            Console.WriteLine("Alcoholisch gerecht?");
            Console.WriteLine("1) Ja");
            Console.WriteLine("2) Nee");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int res))
                {
                    if (res == 1)
                        break;
                    if (res == 2)
                        break;
                }
            }

            options = new List<Option>();

            foreach (var el in d_themeCtrl.GetThemes())
            {
                options.Add(new Option(el.Name, SetThemeID, el.ID));
            }

            Navigation();



            d_currentProduct.ID = d_prodCtrl.IncrementID();
            d_prodCtrl.UpdateList(d_currentProduct);

            DefaultMenu();
        }
        public void OptionYes()
        {
            d_currentProduct.Alcohol = true;
        }
        public void OptionNo()
        {
            d_currentProduct.Alcohol = false;
        }
        public void SetThemeID(int aThemeID)
        {
            d_currentProduct.ThemeID = aThemeID;
        }

        public void View()
        {
            options = new List<Option>();

            options.Add(new Option("Terug", DefaultMenu));
         
            foreach (var l in d_prodCtrl.GetProducts())
            {
                options.Add(new Option(l.Name, Edit, l.ID));
            }
            Navigation();
        }
        public void Edit(int aID)
        {
            d_currentProduct = d_prodCtrl.GetByID(aID);

            if (d_currentProduct != null)
            {
                options = new List<Option>();
                options.Add(new Option(d_currentProduct.Name, FieldName));
                options.Add(new Option("Terug", this.DefaultMenu));
            }

            Navigation();
        }

        public void FieldName()
        {
            Console.WriteLine("Enter name");
            d_currentProduct.Name = Console.ReadLine();
            Navigation();
        }

     
    }
}
