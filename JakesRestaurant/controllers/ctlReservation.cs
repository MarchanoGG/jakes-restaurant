using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using reservation;
using JakesRestaurant.views;

namespace controllers
{
	internal class ctlReservation
	{
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "reservation.json");

        public List<Reservations> reservations;
        public Reservations currentitem { get; set; }
        public ctlReservation()
		{
            Load();
        }

        public void Load()
        {
            if (!File.Exists(path))
            {
                var myFile = File.Create(path);
                myFile.Close();
            }

            string json = File.ReadAllText(path);
            if (json != "")
            {
                reservations = JsonSerializer.Deserialize<List<Reservations>>(json);
            }
            else
            {
                reservations = new List<Reservations>();
            }
            
        }

        public void Write()
        {
            string json = JsonSerializer.Serialize(reservations);
            File.WriteAllText(path, json);
            Console.WriteLine("Gegevens opgeslagen");
        }

        public void UpdateList(Reservations p)
        {
            int index = reservations.FindIndex(s => s.ID == p.ID);

            if (index != -1)
            {
                reservations[index] = p;
                DiningTable dt = ctlMain.diningtable.GetID(p.DiningTable.ID);
                dt.Status = "Bezet";
                ctlMain.diningtable.UpdateList(dt);
            }
            else
            {
                reservations.Add(p);
            }
            Write();
        }
        public void DeleteAll()
        {
            reservations = new List<Reservations>();
            Write();
            Load();
        }
        public void DeleteByItem(Reservations p)
        {
            int index = reservations.FindIndex(s => s.ID == p.ID);

            if (index != -1)
            {
                reservations.Remove(p);
            }

            Write();
        }

        public Reservations FindById(int id)
        {
            return reservations.Find(i => i.ID == id);
        }
        public string GenerateResCode()
        {
            string code;
            int RanNum;
            do
            {
                Random rand = new Random();
                RanNum = rand.Next(0, 9999);
                code = String.Format("{0,4}", RanNum.ToString("D4"));
            }
            while (FindByCode(code) != null);
            return code;
        }

        public Reservations FindByCode(string code)
        {
            return reservations.Find(i => i.ReserveCode == code);
        } 

        public int IncrementID()
        {
            if (reservations.Any())           
               return reservations.Last().ID + 1;           
            else
                return 1;
        }

        public DiningTable FindByPerson(int persons)
        {
            DiningTable result = null;

            foreach (var item in ctlMain.diningtable.diningTables)
            {
                if (persons <= item.Places && item.Status == "Vrij")
                {
                    result = item;
                    break;
                }
            }
            return result;
        }
    }
}