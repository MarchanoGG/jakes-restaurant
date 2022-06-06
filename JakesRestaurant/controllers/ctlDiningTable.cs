using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using reservation;

namespace controllers
{

    internal class ctlDiningTable
	{
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "diningtable.json");

        public List<DiningTable> diningTables;

        public DiningTable selecteditem { get; set; }
		public ctlDiningTable()
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
                diningTables = JsonSerializer.Deserialize<List<DiningTable>>(json);
            }
            else
            {
                diningTables = new List<DiningTable>();
            }
            
        }
        public List<DiningTable> GetList()
        {
            if (diningTables != null)
                return diningTables;
            else
                return null;
        }

        public void Write()
        {
            string json = JsonSerializer.Serialize(diningTables);
            File.WriteAllText(path, json);
        }

        public void UpdateList(DiningTable p)
        {
            int index = diningTables.FindIndex(s => s.ID == p.ID);

            if (index != -1)
            {
                diningTables[index] = p;
            }
            else
            {
                diningTables.Add(p);
            }

            Write();

        }
        public void DeleteById(DiningTable p)
        {
            if (p != null)
            {
                int index = diningTables.FindIndex(s => s.ID == p.ID);

                if (index != -1)
                {
                    diningTables.Remove(p);
                }

                Write();
            }
        }

        public DiningTable GetID(int id)
        {
            return diningTables.Find(i => i.ID == id);
        }

        public int IncrementID()
        {
            if (diningTables.Any())           
               return diningTables.Last().ID + 1;           
            else
                return 1;
        }
    }
}