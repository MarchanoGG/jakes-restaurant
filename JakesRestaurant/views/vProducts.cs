using System;
using controllers;
using management;
using System.Collections.Generic;
using System.Linq;

namespace JakesRestaurant.views
{
    internal class vProducts
    {

        public static List<Option> options;
        public vMenu menu { get; set; }

        public static Product d_currentProduct { get; set; }
        public vProducts()
        {

        }
        public void DefaultMenu()
        {
            options = new List<Option>();
            if (ctlMain.themes.GetThemes() != null)
                options.Add(new Option("Voeg producten toe", this.AddProduct));
            else
                options.Add(new Option("Voeg eerst een thema toe!"));
            options.Add(new Option("Lijst", this.View));
            options.Add(new Option("Exit", () => Environment.Exit(0)));
        }
        public void Navigation()
        {
            DefaultMenu();
            new vMenu(options, "Products.");
        }
        public void AddProduct()
        {
            d_currentProduct = null;
            d_currentProduct = new Product();
            d_currentProduct.ID = ctlMain.products.IncrementID();
            SetName();
            SetPrice();
            SetAllergens();
            SetIngredients();
            SetAlcohol();
            FieldThemeSelect();
            Save();
            Navigation();
        }
        private void SetName(String name = "")
        {
            Console.Clear();
            Console.WriteLine("Naam:");
            String output = Console.ReadLine();
            if (output.Trim() == "")
                d_currentProduct.Name = name;
            else
            d_currentProduct.Name = output;
        }
        private void SetAllergens(List<string> aVal = null)
        {
            Console.Clear();
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
            Console.Clear();
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
        //private void SetTheme()
        //{
        //    Console.WriteLine("Selecteer thema");

        //    foreach (var el in ctlMain.themes.GetThemes())
        //    {
        //        Console.WriteLine($"ID: " + el.ID + " - " + el.Name);
        //    }

        //    while (true)
        //    {
        //        if (int.TryParse(Console.ReadLine(), out int res))
        //        {
        //            if (ctlMain.themes.GetByID(res) != null)
        //            {
        //                d_currentProduct.ThemeID = res;
        //                break;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Selecteer een geldige thema!");
        //            }
        //        }
        //    }
        //}
        public void FieldThemeSelect()
        {
            Console.Clear();
            List<Option> listoptions = new List<Option>();
            foreach (var l in ctlMain.themes.GetThemes())
            {
                string label = $"ID:  {l.ID}  -  {l.Name}";
                listoptions.Add(new Option(label, ThemeSelect, l.ID));
            }
            new vMenu(listoptions);
        }
        public void ThemeSelect(int aId)
        {
            d_currentProduct.Theme = ctlMain.themes.GetByID(aId);
            Console.WriteLine($"Geselecteerd: {d_currentProduct.Name}");
        }
        public void View()
        {
            List<Option> itemlist = new List<Option>();

            itemlist.Add(new Option("Terug", Navigation));

            foreach (var l in ctlMain.products.GetProducts())
            {
                itemlist.Add(new Option(l.Name, EditItem, l.ID));
            }
            new vMenu(itemlist);
        }
        public void EditItem(int aID)
        {
            d_currentProduct = ctlMain.products.GetByID(aID);
            Edit();
        }
        public void Edit()
        {
            List<Option> itemlist = new List<Option>();
            itemlist.Add(new Option("Terug", Navigation));
            if (d_currentProduct != null)
            {
                Theme obj = ctlMain.themes.GetByID(d_currentProduct.ThemeID);
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
                itemlist.Add(new Option($"Naam: " + d_currentProduct.Name, FieldName));
                itemlist.Add(new Option($"Prijs: " + d_currentProduct.Price.ToString(), FieldPrice));
                itemlist.Add(new Option($"Thema : " + tName, FieldThemeSelect));
                itemlist.Add(new Option($"Alocoholisch: " + d_currentProduct.Alcohol, FieldAlcohol));
                itemlist.Add(new Option($"Allergien lijst: " + aL, FieldAllergen));
                itemlist.Add(new Option($"Ingredienten lijst: " + iL, FieldIngredients));
            }
            itemlist.Add(new Option("Verwijder", this.Delete));
            Navigation();
        }
        private void Save()
        {
            ctlMain.products.UpdateList(d_currentProduct);
            d_currentProduct = null;
            View();
        }
        public void FieldName()
        {
            SetName(d_currentProduct.Name);
        }
        public void FieldPrice()
        {
            SetPrice();
            Save();
        }
        //public void FieldTheme()
        //{
        //    SetTheme();
        //    Save();
        //}
        public void FieldAlcohol()
        {
            SetAlcohol();
            Save();
        }
        public void FieldAllergen()
        {
            SetAllergens(d_currentProduct.Allergens);
            Save();
        }
        public void FieldIngredients()
        {
            SetIngredients(d_currentProduct.Ingredients);
            Save();
        }
        public void Delete()
        {
            ctlMain.products.Delete(d_currentProduct.ID);
            View();
        }
    }

}