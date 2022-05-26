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
        public static List<Option> options;
        public static Reservations SelectedItem { get; set; }
        public vReservation()
        {
            options = new List<Option>
            {
                new Option("Voeg toe", this.Add),
                new Option("Alle reserveringen", this.View),
                new Option("Delete", this.ViewDelete),
                new Option("Back to menu", this.BackToMain),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }
        public void Navigation()
        {
            new vMenu(options, "Reserveringen");
        }
        public void Add()
        {
            ctlMain.reservation.currentitem = new Reservations();
            ctlMain.reservation.currentitem.ListProducts = new List<Product>();
            Reservations p = ctlMain.reservation.currentitem;
            Form();
            p.ID = ctlMain.reservation.IncrementID();
            p.CreateDateTime = DateTime.Now;
            ctlMain.reservation.UpdateList(p);
            Navigation();
        }
        public void View()
        {
            Console.WriteLine("Reserveringen.");
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            if (ctlMain.reservation.reservations != null)
            {
                string header = $" + - ID - Gast - Tafel - Datum - Aantal Artikelen";
                Console.WriteLine(header);
                foreach (var l in ctlMain.reservation.reservations)
                {
                    string duedt = l.DueDateTime.ToString("dd/MM/yyyy");
                    string label = $" - {l.ID} - {l.User.Username} - {l.DiningTable.Places} - {duedt}";
                    listoptions.Add(new Option(label, EditItem, l.ID));
                }
                new vMenu(listoptions);
            } else
            {
                Console.WriteLine("Geen");
            }
        }
        public void EditItem(int aID)
        {
            SelectedItem = ctlMain.reservation.GetID(aID);
            List<Option> listoptions = new List<Option>();
            listoptions = new List<Option>
            {
                new Option("Aanpassen", Edit, aID),
                new Option("Verwijderen", Delete, aID),
            };
        }
        public void ViewDelete()
        {
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            foreach (var l in ctlMain.reservation.reservations)
            {
                string duedt = l.DueDateTime.ToString("dd/MM/yyyy");
                string label = $" - {l.ID} - {l.User.Username} - {l.DiningTable.Places} - {duedt}";
                listoptions.Add(new Option(label, Delete, l.ID));
            }
            new vMenu(listoptions);
            Navigation();
        }
        public void Edit(int aID)
        {
            // Get from parameter
            Form();
            // If success than update product
            ctlMain.reservation.UpdateList(SelectedItem);

            // Go back to View navigation
            View();
        }
        public void Delete(int aID)
        {
            // Get from parameter
            Reservations p = ctlMain.reservation.GetID(aID);

            // If success than update product
            ctlMain.reservation.DeleteById(p);

            // Go back to View navigation
        }
        public void Form()
        {
            Reservations p = SelectedItem;
            Console.WriteLine("Aantal personen:");
            while (p.DiningTable == null)
            {
                int persons = int.Parse(Console.ReadLine());
                p.DiningTable = ctlMain.reservation.FindByPersons(persons);
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

            foreach (var l in ctlMain.products.GetProducts())
            {
                var label = $"ID:{l.ID}; Naam gerecht:{l.Name}; Prijs: {l.Price}";
                listoptions.Add(new Option(label, ProductSelect, l.ID));
            }

            new vMenu(listoptions);
        }
        public void ViewDiningTableSelect()
        {
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctlMain.diningtable.GetList())
            {
                var label = $"Tafel voor {l.Places}";
                listoptions.Add(new Option(label, DiningTableSelect, l.ID));
            }

            new vMenu(listoptions);
        }
        public void ViewUserSelect()
        {
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctlMain.users.users)
            {
                var label = $"{l.Username}";
                listoptions.Add(new Option(label, UserSelect, l.ID));
            }

            new vMenu(listoptions);
        }

        public void DiningTableSelect(int aId)
        {
            ctlMain.reservation.currentitem.DiningTable = ctlMain.diningtable.GetID(aId);
        }

        public void UserSelect(int aId)
        {
            ctlMain.reservation.currentitem.User = ctlMain.users.FindById(aId);
            Console.WriteLine($"Geselecteerd: {ctlMain.reservation.currentitem.User.Username}");
        }

        public void ProductSelect(int aId)
        {
            Product Product = ctlMain.products.GetID(aId);
            Console.WriteLine($"Geselecteerd: {Product.Name}");
            ctlMain.reservation.currentitem.ListProducts.Add(Product);
            //Console.WriteLine($"Gebruiker: {ctl.currentitem.ListProducts.Count}");
        }
        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
}
