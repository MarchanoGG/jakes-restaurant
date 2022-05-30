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
            DiningTable p = new DiningTable();
            FillFromInput(ref p);
            p.ID = ctl.IncrementID();
            p.Status = "Vrij";
            ctl.UpdateList(p);

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
                var label = $"Tafel voor {l.Places}";
                listoptions.Add(new Option(label, Delete, l.ID));
            }

            this.menu = new vMenu(listoptions, "Tafels verwijderen");
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
