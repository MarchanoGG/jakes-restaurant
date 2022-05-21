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
        private ctlDiningTable d_DiningTableCtrl;
        public static List<Option> options;
        public vMenu menu { get; set; }
        public vDiningtable()
        {
            d_DiningTableCtrl = new ctlDiningTable();
            Console.WriteLine("Zit plaatsen");
            options = new List<Option>
            {
                new Option("Voeg toe", this.Add),
                new Option("Lijst", this.View),
                new Option("Back to menu", this.BackToMain),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }

        public void Navigation()
        {
            this.menu = new vMenu(options);
        }

        public void Add()
        {
            DiningTable p = new DiningTable();
            FillFromInput(ref p);
            p.ID = d_DiningTableCtrl.IncrementID();
            d_DiningTableCtrl.UpdateList(p);

            Navigation();
        }
        public void View()
        {
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            foreach (var l in d_DiningTableCtrl.GetList())
            {
                var label = $"Tafel voor {l.Places}";
                listoptions.Add(new Option(label, Edit, l.ID));
            }

            this.menu = new vMenu(listoptions);
        }
        public void Edit(int aID)
        {
            // Get from parameter
            DiningTable p = d_DiningTableCtrl.GetID(aID);

            FillFromInput(ref p);

            // If success than update product
            d_DiningTableCtrl.UpdateList(p);

            // Go back to View navigation
            View();
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
