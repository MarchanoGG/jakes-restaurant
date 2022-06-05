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

            d_currentProduct = new Product();
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
            d_currentProduct = null;
            d_currentProduct = new Product();

            SetName();
            SetPrice();
            SetAllergens();
            SetIngredients();
            SetAlcohol();    
            SetTheme();         


            d_currentProduct.ID = d_prodCtrl.IncrementID();

            Save();
            DefaultMenu();
        }

        private void SetName(String name = "")
        {
            Console.WriteLine("Naam:");
            String output = Console.ReadLine();
            if (output.Trim() == "")
                d_currentProduct.Name = name;
            else
            d_currentProduct.Name = output;
        }

        private void SetAllergens(List<string> aVal = null)
        {
         
            Console.WriteLine("Allergiën: (Gebruik ; na ieder item)");
            String output = Console.ReadLine();
            if (output.Trim() == "")
                d_currentProduct.Allergens = aVal;
            else
                d_currentProduct.Allergens = output.Split(';').ToList();
        }

        private void SetIngredients(List<string> aVal = null)
        {
            Console.WriteLine("Ingrediënten: (Gebruik ; na ieder item)");

            String output = Console.ReadLine();
            if (output.Trim() == "")
                d_currentProduct.Ingredients = aVal;
            else
                d_currentProduct.Ingredients = output.Split(';').ToList();
        }


            private void SetPrice()
        {
            Console.WriteLine("Prijs:");
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double price))
                {
                    d_currentProduct.Price = price;
                    break;
                }
                else
                {
                    Console.WriteLine("Invoer niet correct!");
                }
            }
        }

        private void SetAlcohol()
        {
            Console.WriteLine("Alcoholisch gerecht?");
            Console.WriteLine("1) Ja");
            Console.WriteLine("2) Nee");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int res))
                {
                    if (res == 1)
                    {
                        d_currentProduct.Alcohol = true;
                        break;
                    }
                    if (res == 2)
                    {
                        d_currentProduct.Alcohol = false;
                        break;
                    }

                    Console.WriteLine("Graag een optie selecteren");
                }
            }
        }

        private void SetTheme()
        {
            Console.WriteLine("Selecteer thema");

            foreach (var el in d_themeCtrl.GetThemes())
            {
                Console.WriteLine($"ID: " + el.ID + " - " + el.Name);
            }

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int res))
                {
                    if (d_themeCtrl.GetByID(res) != null)
                    {
                        d_currentProduct.ThemeID = res;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Selecteer een geldige thema!");
                    }
                }
            }
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

            options = new List<Option>();
            options.Add(new Option("Terug", this.DefaultMenu));

            if (d_currentProduct != null)
            {
                Theme obj = d_themeCtrl.GetByID(d_currentProduct.ThemeID);
                String tName = "Niet geselecteerd";
                if (obj != null)
                    tName = obj.Name;


                String aL = "";
                String iL = "";


                if (d_currentProduct.Allergens != null)
                {
                    foreach (var el in d_currentProduct.Allergens)
                    {
                        aL += el;
                        aL += " ";
                    }
                }
                if (d_currentProduct.Ingredients != null)
                    {
                    foreach (var el2 in d_currentProduct.Ingredients)
                    {
                        iL += el2;
                        iL += " ";
                    }
                }
                

                options.Add(new Option($"Naam: " + d_currentProduct.Name, FieldName));
                options.Add(new Option($"Prijs: " + d_currentProduct.Price.ToString(), FieldPrice));
                options.Add(new Option($"Thema : " + tName, FieldTheme)); ;
                options.Add(new Option($"Alocoholisch: " + d_currentProduct.Alcohol, FieldAlcohol));
                options.Add(new Option($"Allergien lijst: " + aL, FieldAllergen));
                options.Add(new Option($"Ingredienten lijst: " + iL, FieldIngredients));
            }


            options.Add(new Option("Verwijder", this.Delete));

            Navigation();
        }

        private void Save()
        {
            d_prodCtrl.UpdateList(d_currentProduct);
        }

        public void FieldName()
        {
            SetName(d_currentProduct.Name);
            Save();
            Edit(d_currentProduct.ID);
        }
        public void FieldPrice()
        {
            SetPrice();
            Save();
            Edit(d_currentProduct.ID);
        }
        public void FieldTheme()
        {
            SetTheme();
            Save();
            Edit(d_currentProduct.ID);
        }
        public void FieldAlcohol()
        {
            SetAlcohol();
            Save();
            Edit(d_currentProduct.ID);
        }

        public void FieldAllergen()
        {
            SetAllergens(d_currentProduct.Allergens);
            Save();
            Edit(d_currentProduct.ID);
        }

        public void FieldIngredients()
        {
            SetIngredients(d_currentProduct.Ingredients);
            Save();
            Edit(d_currentProduct.ID);
        }

        public void Delete()
        {
            d_prodCtrl.Delete(d_currentProduct.ID);
            View();
        }
    }

}