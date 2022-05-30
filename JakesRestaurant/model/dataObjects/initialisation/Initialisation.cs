using System;
using System.IO;
namespace JakesRestaurant.model.dataObjects.initialisation
{
    public class Initialisation
    {
        public Initialisation(string[] Files)
        {
            for (int i = 0; i < Files.Length; i++)
            {
                if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data/", Files[i])))
                {
                    // Create products from json 
                    var myFile = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data/", Files[i]));
                    myFile.Close();
                }
            }
        }
    }
}
