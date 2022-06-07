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
                options.Add(new Option("Voeg producten toe", AddProduct));
            else
                options.Add(new Option("Voeg eerst een thema toe!"));
            options.Add(new Option("Lijst", View));
            options.Add(new Option("Terug", BackToMain));
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
            string header = "  ";
            if (ctlMain.themes.GetThemes() != null)
            {
                header += vMenu.EqualWidthCol("ID", 3);
                header += vMenu.EqualWidthCol("Naam", 10);
                header += vMenu.EqualWidthCol("Start", 10);
                header += vMenu.EqualWidthCol("Eind", 10);
                Console.WriteLine(header);
                foreach (var r in ctlMain.themes.GetThemes())
                {
                    string label = "";
                    label += vMenu.EqualWidthCol(r.ID.ToString(), 3);
                    label += vMenu.EqualWidthCol(r.Name, 10);
                    label += vMenu.EqualWidthCol(r.StartDateStr, 10);
                    label += vMenu.EqualWidthCol(r.EndDateStr, 10);
                    listoptions.Add(new Option(label, ThemeSelect, r.ID));
                }
            }
            else
            {
                Console.WriteLine("Geen");
            }
            listoptions.Add(new Option("Opslaan", Save));
            listoptions.Add(new Option("Terug", Edit));
            new vMenu(listoptions, "Selecteer een Thema.");
        }
        public void ThemeSelect(int aId)
        {
            d_currentProduct.Theme = ctlMain.themes.GetByID(aId);
            Console.WriteLine($"Geselecteerd: {d_currentProduct.Theme.Name}");
            Save();
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
                itemlist.Add(new Option($"Naam: " + d_currentProduct.Name, InsertValue, 1));
                itemlist.Add(new Option($"Prijs: " + d_currentProduct.Price.ToString(), InsertValue, 2));
                itemlist.Add(new Option($"Alocoholisch: " + d_currentProduct.Alcohol, InsertValue, 4));
                itemlist.Add(new Option($"Allergien lijst: " + aL, InsertValue, 5));
                itemlist.Add(new Option($"Ingredienten lijst: " + iL, InsertValue, 6));
                itemlist.Add(new Option($"Thema: " + tName, InsertValue, 3));
            }
            itemlist.Add(new Option("Verwijderen?", Delete));
            itemlist.Add(new Option("Opslaan?", Save));
            new vMenu(itemlist);
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
        }
        //public void FieldTheme()
        //{
        //    SetTheme();
        //    Save();
        //}
        public void FieldAlcohol()
        {
            SetAlcohol();
        }
        public void FieldAllergen()
        {
            SetAllergens(d_currentProduct.Allergens);
        }
        public void FieldIngredients()
        {
            SetIngredients(d_currentProduct.Ingredients);
        }
        public void Delete()
        {
            ctlMain.products.Delete(d_currentProduct.ID);
            View();
        }
        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
        public void InsertValue(int idx)
        {
            switch (idx)
            {
                case 1:
                    FieldName();
                    break;
                case 2:
                    FieldPrice();
                    break;
                case 3:
                    FieldThemeSelect();
                    break;
                case 4:
                    FieldAlcohol();
                    break;
                case 5:
                    FieldAllergen();
                    break;
                case 6:
                    FieldIngredients();
                    break;
                default:
                    break;
            }
            Edit();
        }
    }
}