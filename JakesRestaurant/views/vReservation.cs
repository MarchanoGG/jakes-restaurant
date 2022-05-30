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
                new Option("Terug", this.BackToMain),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }
        public void Navigation()
        {
            new vMenu(options, "Reserveringen");
        }
        public void Add()
        {
            SelectedItem = new Reservations();
            SelectedItem.ListProducts = new List<Product>();
            FieldPersons();
            FieldDueDate();
            FieldProductsSelect();
            FieldUserSelect();
            FieldComment();
            SelectedItem.ID = ctlMain.reservation.IncrementID();
            SelectedItem.CreateDateTime = DateTime.Now;
            ctlMain.reservation.UpdateList(SelectedItem);
            SelectedItem = null;
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
            List<Option> listoptions = new List<Option>()
            {
                new Option("Aanpassen", Edit),
                new Option("Verwijderen", Delete),
            };
            new vMenu(listoptions);
        }
        public void Edit()
        {
            Console.WriteLine("Reserveringen.");
            List<Option> listoptions = new List<Option>()
            {
                new Option("Terug", Navigation),
                new Option("Aantal personen: "+SelectedItem.NumberGuests, FieldPersons),
                new Option("Reseringsdatum: "+SelectedItem.DueDateTime.ToString("dd/MM/yyyy"), FieldDueDate),
                new Option("Producten", FieldProductsSelect),
                new Option("Contactpersoon: "+SelectedItem.User.FirstName, FieldUserSelect),
                new Option("Opmerking: "+SelectedItem.Comment, FieldComment),
            };
            new vMenu(listoptions);
            ctlMain.reservation.UpdateList(SelectedItem);
            SelectedItem = null;
            View();
        }
        public void Delete()
        {
            ctlMain.reservation.DeleteByItem(SelectedItem);
            SelectedItem = null;
            View();
        }
        public void FieldPersons()
        {
            Console.WriteLine("Aantal personen:");
            while (SelectedItem.DiningTable == null)
            {
                int persons = int.Parse(Console.ReadLine());
                SelectedItem.NumberGuests = persons;
                SelectedItem.DiningTable = ctlMain.reservation.FindByPerson(persons);
                if (SelectedItem.DiningTable == null)
                {
                    Console.WriteLine("Geen tafel/zitplek beschikbaar");
                    Navigation();
                }
            }
            ctlMain.reservation.UpdateList(SelectedItem);
        }
        public void FieldDueDate()
        {
            Console.WriteLine("Reseringsdatum:");
            string line = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Invalid date, please retry");
                line = Console.ReadLine();
            }
            SelectedItem.DueDateTime = dt;
            ctlMain.reservation.UpdateList(SelectedItem);
        }
        public void FieldComment()
        {
            Console.WriteLine("Opmerking (Optioneel):");
            SelectedItem.Comment = Console.ReadLine();
            ctlMain.reservation.UpdateList(SelectedItem);
        }
        public void FieldProductsSelect()
        {
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctlMain.products.GetProducts())
            {
                var label = $"ID:{l.ID}; Naam gerecht:{l.Name}; Prijs: {l.Price}";
                listoptions.Add(new Option(label, ProductSelect, l.ID));
            }
            new vMenu(listoptions);
        }
        public void FieldUserSelect()
        {
            List<Option> listoptions = new List<Option>();

            foreach (var l in ctlMain.users.users)
            {
                var label = $"{l.Username}";
                listoptions.Add(new Option(label, UserSelect, l.ID));
            }
            new vMenu(listoptions);
        }
        public void UserSelect(int aId)
        {
            SelectedItem.User = ctlMain.users.FindById(aId);
            Console.WriteLine($"Geselecteerd: {SelectedItem.User.Username}");
            ctlMain.reservation.UpdateList(SelectedItem);
        }

        public void ProductSelect(int aId)
        {
            Product Product = ctlMain.products.GetID(aId);
            Console.WriteLine($"Geselecteerd: {Product.Name}");
            SelectedItem.ListProducts.Add(Product);
            ctlMain.reservation.UpdateList(SelectedItem);
        }
        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
}
