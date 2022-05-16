using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using FileManagement;

namespace management
{
    class Product
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "products.json");

        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public string Summary()
        {
            return Name;
        }

        // Function will create default Products (Used for initial setup)
        public void InitializeProducts()
        {
            if (!FileManagement.JsonFileManager.FileExists(path))
            {
                // Create products from json 
            }
        }

        public void Create(Product aProduct)
        {
            if (JakesRestaurant.Program.GetCurrentUser().HasPrivilege())
            {
                // Auto increment ID based on Count of List
                List<Product> products = FileManagement.JsonFileManager.ReadList<Product>(path);

                // Only when products file could not be found this would give a problem
                aProduct.ID = products.Count + 1;
                FileManagement.JsonFileManager.UpdateList(path, aProduct);
            }
            else
            {
                Console.WriteLine("Deze gebruiker heeft niet de juiste rechten!");
            }
        }

        public void Update(Product aProduct)
        {
            if (JakesRestaurant.Program.GetCurrentUser().HasPrivilege())
            {
                FileManagement.JsonFileManager.UpdateList(path, aProduct);
            }
            else
            {
                Console.WriteLine("Deze gebruiker heeft niet de juiste rechten!");
            }
        }

        public void Delete(int id)
        {
            if (JakesRestaurant.Program.GetCurrentUser().HasPrivilege())
            {

            }
            else
            {
                Console.WriteLine("Deze gebruiker heeft niet de juiste rechten!");
            }
        }
    }
}
