using management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace controllers
{
    public class TctlProducts
    {
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "products.json");

        private TctlThemes d_themeCtrl;
        private List<Product> d_products;

        public List<Product> GetProducts()
        {
            return d_products;
        }

        public TctlProducts()
        {
            d_themeCtrl = new TctlThemes();
            Load();
        }

        public void Load()
        {
            if (!File.Exists(path))
            {
                // Create products from json 
                var myFile = File.Create(path);
                myFile.Close();
            }

            string json = File.ReadAllText(path);
            if (json != "")
            {
                d_products = JsonSerializer.Deserialize<List<Product>>(json);
            }
            else
            {
                d_products = new List<Product>();
            }

        }

        public List<Product> GetProductsByThemeID(int aThemeID)
        {
            List<Product> tmpProducts = new List<Product>();

            foreach (var el in d_products)
            {

                if (d_products.Find(s => s.ThemeID == aThemeID) != null)
                {
                    tmpProducts.Add(el);
                }
            }
            return tmpProducts;
        }

        public void Write()
        {
            string json = JsonSerializer.Serialize(d_products);
            File.WriteAllText(path, json);
            Console.WriteLine("write done");
        }

        public void UpdateList(Product Product)
        {
            int index = d_products.FindIndex(s => s.ID == Product.ID);

            if (index != -1)
            {
                d_products[index] = Product;
            }
            else
            {
                d_products.Add(Product);
            }

            Write();
        }

        public bool Delete(int aID)
        {
            if (d_products.Find(s => s.ID == aID) != null)
            {
                d_products.Remove(d_products.Find(s => s.ID == aID));
                Write();
                return true;
            }

            return false;

        }

        public Product GetByID(int id)
        {
            return d_products.Find(i => i.ID == id);
        }

        public int IncrementID()
        {
            if (d_products.Any())
                return d_products.Last().ID + 1;
            else
                return 1;
        }

        public List<Product> GetFromDate(DateTime aBegin, DateTime aEnd)
        {
            List<Product> products = new List<Product>();


            foreach (var el in d_products)
            {
                Theme t = d_themeCtrl.GetByID(el.ID);

                if (t != null)
                {
                    DateTime startDate = t.StartDate;
                    DateTime endDate = t.EndDate;

                    if (startDate.CompareTo(aBegin) >= 0 && endDate.CompareTo(aEnd) <= 0)
                    {
                        products.Add(el);
                    }
                }
            }

            return products;

        }
    }
}