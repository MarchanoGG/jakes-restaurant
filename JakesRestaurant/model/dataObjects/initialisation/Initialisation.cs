using System;
using System.IO;
namespace JakesRestaurant.model.dataObjects.initialisation
{
    public class Initialisation
    {
        public Initialisation(string[][] Files)
        {
            for (int i = 0; i < Files.Length; i++)
            {
                if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data/", Files[i][0])))
                {
                    // Create products from json 
                    var myFile = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data/", Files[i][0]));
                    if (Files[i][0] != "")
                    {
                        File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data/", Files[i][0]), Files[i][0]);
                    }
                    myFile.Close();
                }
            }
        }
    }
}
