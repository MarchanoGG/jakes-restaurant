using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controllers;
using reservation;
using Authentication;
using management;

namespace JakesRestaurant.views
{
    internal class vReservation
    {
        private ctlReservation ctl;
        private ctlDiningTable ctlDT;
        private ctlUsers ctlU;
        private TctlProducts ctlP;
        public static List<Option> options;
        public vMenu menu { get; set; }

        public vReservation()
        {
            ctl = new ctlReservation();
            ctlDT = ctl.ctlDT;
            ctlU = ctl.ctlU;
            ctlP = ctl.ctlP;
            options = new List<Option>
            {
                new Option("Voeg toe", this.Add),
                new Option("Lijst", this.View),
                new Option("Delete", this.ViewDelete),
                new Option("Back to menu", this.BackToMain),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }

        public void Navigation()
        {
            this.menu = new vMenu(options, "Reserveringen");
        }

        public void Add()
        {
            ctl.currentitem = new Reservations();
            ctl.currentitem.ListProducts = new List<Product>();
            Reservations p = ctl.currentitem;
            Form(ref p);
            p.ID = ctl.IncrementID();
            p.CreateDateTime = DateTime.Now;
            ctl.UpdateList(p);
            Navigation();
        }
        public void View()
        {
            Console.WriteLine("Reserveringen.");
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            string header = $" + - ID - Gast - Tafel - Datum - Aantal Artikelen";
            Console.WriteLine(header);
            foreach (var l in ctl.GetList())
            {
                string duedt = l.DueDateTime.ToString("dd/MM/yyyy");
                string label = $" - {l.ID} - {l.User.Username} - {l.DiningTable.Places} - {duedt} - {l.ListProducts.Count}";
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
                string duedt = l.DueDateTime.ToString("dd/MM/yyyy");
                string label = $" - {l.ID} - {l.User.Username} - {l.DiningTable.Places} - {duedt}";
                listoptions.Add(new Option(label, Delete, l.ID));
            }

            this.menu = new vMenu(listoptions);
            Navigation();
        }

        public void Edit(int aID)
        {
            // Get from parameter
            Reservations p = ctl.GetID(aID);

            Form(ref p);

            // If success than update product
            ctl.UpdateList(p);

            // Go back to View navigation
            View();
        }
        public void Delete(int aID)
        {
            // Get from parameter
            Reservations p = ctl.GetID(aID);

            // If success than update product
            ctl.DeleteById(p);

            // Go back to View navigation
        }

        public void Form(ref Reservations p)
        {
            Console.WriteLine("Aantal personen:");
            while (p.DiningTable == null)
            {
                int persons = int.Parse(Console.ReadLine());
                p.DiningTable = ctl.FindByPersons(persons);
                if (p.DiningTable == null)
                {
                    Console.WriteLine("Geen tafel/zitplek beschikbaar");
                    Navigation();
                }
            }

            Console.WriteLine("Boeking datum:");
            string line = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Invalid date, please retry");
                line = Console.ReadLine();
            }
            p.DueDateTime = dt;

            ViewProductsSelect();
            ViewUserSelect();
        }

        public void ViewProductsSelect()
        {
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctlP.GetProducts())
            {
                var label = $"ID:{l.ID}; Naam gerecht:{l.Name}; Prijs: {l.Price}";
                listoptions.Add(new Option(label, ProductSelect, l.ID));
            }

            new vMenu(listoptions);
        }
        public void ViewDiningTableSelect()
        {
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctlDT.GetList())
            {
                var label = $"Tafel voor {l.Places}";
                listoptions.Add(new Option(label, DiningTableSelect, l.ID));
            }

            new vMenu(listoptions);
        }
        public void ViewUserSelect()
        {
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctlU.users)
            {
                var label = $"{l.Username}";
                listoptions.Add(new Option(label, UserSelect, l.ID));
            }

            new vMenu(listoptions);
        }

        public void DiningTableSelect(int aId)
        {
            ctl.currentitem.DiningTable = ctlDT.GetID(aId);
        }

        public void UserSelect(int aId)
        {
            ctl.currentitem.User = ctlU.FindById(aId);
            Console.WriteLine($"Geselecteerd: {ctl.currentitem.User.Username}");
        }

        public void ProductSelect(int aId)
        {
            Product Product = ctlP.GetID(aId);
            Console.WriteLine($"Geselecteerd: {Product.Name}");
            ctl.currentitem.ListProducts.Add(Product);
            //Console.WriteLine($"Gebruiker: {ctl.currentitem.ListProducts.Count}");
        }
        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
}
