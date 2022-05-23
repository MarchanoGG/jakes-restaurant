using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using reservation;

namespace controllers
{
	internal class ctlReservation
	{
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "reservation.json");

        private List<Reservations> reservations;

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
        public List<Reservations> GetList()
        {
            if (reservations != null)
                return reservations;
            else
                return null;
        }

        public void Write()
        {
            string json = JsonSerializer.Serialize(reservations);
            File.WriteAllText(path, json);
            Console.WriteLine("write done");
        }

        public void UpdateList(Reservations p)
        {
            int index = reservations.FindIndex(s => s.ID == p.ID);

            if (index != -1)
            {
                reservations[index] = p;
            }
            else
            {
                reservations.Add(p);
            }

            Write();

        }
        public void DeleteById(Reservations p)
        {
            int index = reservations.FindIndex(s => s.ID == p.ID);

            if (index != -1)
            {
                reservations.Remove(p);
            }

            Write();
        }

        public Reservations GetID(int id)
        {
            return reservations.Find(i => i.ID == id);
        }

        public int IncrementID()
        {
            if (reservations.Any())           
               return reservations.Last().ID + 1;           
            else
                return 1;
        }
    }
}