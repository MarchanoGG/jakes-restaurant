using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using management;

namespace controllers
{
	internal class TctlProducts
	{
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "products.json");

        private List<Thema> d_thema;

        public List<Product> GetProducts(int aID)
        {
            return d_thema.Find(i => i.ID == aID).Products;
        }

		public TctlProducts()
		{
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
                d_thema = JsonSerializer.Deserialize<List<Thema>>(json);
            }
            else
            {
                d_thema = new List<Thema>();
            }
            
        }

        public List<Thema> GetTheme()
        {
            if (d_thema != null)
                return d_thema;
            else
                return null;
        }

        public void Write()
        {
            string json = JsonSerializer.Serialize(d_thema);
            File.WriteAllText(path, json);
            Console.WriteLine("write done");
        }

        public void UpdateList(Thema thema)
        {
            int index = d_thema.FindIndex(s => s.ID == thema.ID);
               

            if (index != -1)
            {
                d_thema[index] = thema;
            }
            else
            {
                d_thema.Add(thema);
            }

            Write();

        }

        public Thema GetID(int id)
        {
            return d_thema.Find(i => i.ID == id);
        }

        public int IncrementID() // Leave for now but we don't want to increment based of theme but from product and theme
        {
            if (d_thema.Any())           
               return d_thema.Last().ID + 1;           
            else
                return 1;
        }

        public int IncrementProductID(Thema aTheme) // Leave for now but we don't want to increment based of theme but from product and theme
        {
            if (aTheme.Products.Any())
                return aTheme.Products.Last().ID + 1;
            else
                return 1;
        }
    }
}