using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controllers;
using reservation;

namespace JakesRestaurant.views
{
    internal class vDiningtable
    {
        public ctlDiningTable ctl { get; set; }
        public static List<Option> options;
        public vMenu menu { get; set; }
        public vDiningtable()
        {
            ctl = new ctlDiningTable();
            options = new List<Option>
            {
                new Option("Voeg toe", this.Add),
                new Option("Lijst", this.View),
                new Option("Verwijderen", this.ViewDelete),
                new Option("Terug", this.BackToMain),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }

        public void Navigation()
        {
            this.menu = new vMenu(options, "Zit plaatsen instellingen.");
        }

        public void Add()
        {
            DiningTable p = new DiningTable();
            FillFromInput(ref p);
            p.ID = ctl.IncrementID();
            p.Status = "Vrij";
            ctl.UpdateList(p);

            Navigation();
        }
        public void View()
        {
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            foreach (var l in ctl.GetList())
            {
                var label = $"Tafel met {l.Places} zit plaatsen. Omschrijving: {l.Description}";
                listoptions.Add(new Option(label, Edit, l.ID));
            }

            this.menu = new vMenu(listoptions);
        }

        public void ViewDelete()
        {
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            foreach (var l in ctl.GetList())
            {
                var label = $"Tafel voor {l.Places}";
                listoptions.Add(new Option(label, Delete, l.ID));
            }

            this.menu = new vMenu(listoptions);
            Navigation();
        }

        public void Edit(int aID)
        {
            // Get from parameter
            DiningTable p = ctl.GetID(aID);

            FillFromInput(ref p);

            // If success than update product
            ctl.UpdateList(p);

            // Go back to View navigation
            View();
        }
        public void Delete(int aID)
        {
            // Get from parameter
            DiningTable p = ctl.GetID(aID);

            // If success than update product
            ctl.DeleteById(p);

            // Go back to View navigation
        }

        public void FillFromInput(ref DiningTable p)
        {
            Console.WriteLine("Plaatsen:");
            p.Places = int.Parse(Console.ReadLine());

            Console.WriteLine("Omschrijving:");
            p.Description = Console.ReadLine();
        }

        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
}
