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
        public DiningTable table { get; set; }
        public ctlDiningTable ctl { get; set; }
        public static List<Option> options;
        public vMenu menu { get; set; }
        public vDiningtable()
        {
        }

        public void Navigation()
        {
            ctl = new ctlDiningTable();

            if (Program.MyUser.HasPrivilege() == true)
            {
                vMain mainmenu = new vMain();

                options = new List<Option>
                {
                    new Option("Lijst weergeven", this.View),
                    new Option("Nieuwe tafel toevoegen", this.Add),
                    new Option("Bestaande tafel verwijderen", this.ViewDelete),
                    new Option("Terug", () => mainmenu.Navigation())
                };
            }
            else
            {
                this.View();
            }

            this.menu = new vMenu(options, "Zitplaatsen");
        }

        public void Add()
        {
            this.table = new DiningTable();
            this.table.ID = ctl.IncrementID();
            this.table.Status = "Vrij";

            Console.WriteLine("Aantal plaatsen:");
            string Places = Console.ReadLine();

            while (!int.TryParse(Places, out int i))
            {
                Places = Console.ReadLine();
            }

            this.table.Places = int.Parse(Places);

            Console.WriteLine("\r\nOmschrijving:");

            this.table.Description = Console.ReadLine();

            ctl.UpdateList(this.table);

            Navigation();
        }
        public void View()
        {
            vMain mainmenu = new vMain();
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctl.GetList())
            {
                listoptions.Add(new Option(l.Summary(), Edit, l.ID));
            }

            if (Program.MyUser.HasPrivilege() == true)
            {
                listoptions.Add(new Option("Terug", Navigation));
            }
            else
            {
                listoptions.Add(new Option("Terug", () => mainmenu.Navigation()));
            }

            this.menu = new vMenu(listoptions, "Lijst van de tafels");
        }

        public void ViewDelete()
        {
            vMain mainmenu = new vMain();
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctl.GetList())
            {
                listoptions.Add(new Option("Verwijder: " + l.Description, Delete, l.ID));
            }

            listoptions.Add(new Option("Terug", Navigation));

            this.menu = new vMenu(listoptions, "Tafels verwijderen");
            Navigation();
        }

        public void Edit(int aID)
        {
            vMain mainmenu = new vMain();

            this.table = ctl.GetID(aID);

            options = new List<Option>
            {
                new Option("Aantal plaatsen:    " + this.table.Places, this.FillFromInput, 1),
                new Option("Omschrijving:       " + this.table.Description, this.FillFromInput, 2),
                new Option("Terug", View),
            };

            this.menu = new vMenu(options, "Tafel aanpassen");
        }
        public void Delete(int aID)
        {
            // Get from parameter
            DiningTable p = ctl.GetID(aID);

            // If success than update product
            ctl.DeleteById(p);

            // Go back to View navigation
            View();
        }

        public void FillFromInput(int aOption)
        {
            switch (aOption)
            {
                case 1:
                    Console.WriteLine("Aantal plaatsen aanpassen:");
                    string Places = Console.ReadLine();

                    while (!int.TryParse(Places, out int i))
                    {
                        Places = Console.ReadLine();
                    }

                    this.table.Places = int.Parse(Places);
                    break;
                case 2:
                    Console.WriteLine("Omschrijving aanpassen:");
                    this.table.Description = Console.ReadLine();
                    break;
                default:
                    break;
            }
            ctl.UpdateList(this.table);
            this.Edit(this.table.ID);
        }

        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
}
