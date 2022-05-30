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
                new Option("Maak een reservering", Add),
                new Option("Mijn reserveringen", View),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }
        public virtual void Navigation()
        {
            //new vMenu(options, "Reserveringen");
            BackToMain();
        }
        public virtual void Add()
        {
            SelectedItem = new Reservations();
            SelectedItem.ID = ctlMain.reservation.IncrementID();
            SelectedItem.CreateDateTime = DateTime.Now;
            SelectedItem.Status = "Actief";
            SelectedItem.ListProducts = new List<Product>();
            SelectedItem.User = Program.MyUser;
            FieldReserveCode();
            FieldPersons();
            FieldDueDate();
            FieldComment();
            FieldListProducts();
        }
        public void FieldReserveCode()
        {
            SelectedItem.ReserveCode = ctlMain.reservation.GenerateResCode();
        }
        public virtual void View()
        {
            Console.WriteLine("Reserveringen.");
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            if (ctlMain.reservation.reservations != null)
            {
                string header = $" + - ID - Gast - Tafel - Datum - Aantal Artikelen - #Code";
                Console.WriteLine(header);
                foreach (var l in ctlMain.reservation.reservations)
                {
                    string duedt = l.DueDateTime.ToString("dd/MM/yyyy");
                    string label = $" - {l.ID} - {l.User.Username} - {l.DiningTable.Places} - {duedt} - {l.ListProducts.Count} - {l.ReserveCode}";
                    listoptions.Add(new Option(label, EditItem, l.ID));
                }
                new vMenu(listoptions);
            }
            else
            {
                Console.WriteLine("Geen");
            }
        }
        public virtual void EditItem(int aID)
        {
            SelectedItem = ctlMain.reservation.FindById(aID);
            Edit();
        }
        public virtual void Edit()
        {
            Console.WriteLine("Reserveringen.");
            List<Option> listoptions = new List<Option>()
            {
                new Option("Terug", Navigation),
                new Option("Aantal personen: "+SelectedItem.NumberGuests, FieldPersons),
                new Option("Reseringsdatum: "+SelectedItem.DueDateTime.ToString("dd/MM/yyyy"), FieldDueDate),
                new Option("Producten", FieldListProducts),
                new Option("Opmerking: "+SelectedItem.Comment, FieldComment),
                new Option("Opzeggen? ", FieldComment),
                new Option("Opslaan? ", SaveConfirm),
            };
            new vMenu(listoptions);
        }
        public void CheckSelectedItem()
        {
            bool IsValid = true;
            if (SelectedItem.NumberGuests <= 0) {
                IsValid = false;
                Console.WriteLine("Aantal personen is niet ingevuld");
            }
            if(SelectedItem.DueDateTime < DateTime.Now) {
                IsValid = false;
                Console.WriteLine("Datum is niet ingevuld");
            }
            if (!IsValid)
            {
                Console.ReadKey();
                Navigation();
            }
        }
        public void SaveConfirm()
        {
            CheckSelectedItem();
            List<Option> listoptions = new List<Option>()
            {
                new Option("Ja", SaveItem),
                new Option("Nee", Navigation),
            };
            new vMenu(listoptions);
        }
        public void SaveItem()
        {
            ctlMain.reservation.UpdateList(SelectedItem);
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
        }
        public void FieldStatus()
        {
            Console.WriteLine("Reservering annuleren?");
            List<Option> listoptions = new List<Option>()
            {
                new Option("Ja", ItemCancel),
                new Option("Nee", Edit),
            };
            new vMenu(listoptions);
            SelectedItem.Comment = Console.ReadLine();
        }
        public void ItemCancel()
        {
            SelectedItem.Status = "Geannuleerd";
            SaveConfirm();
        }
        public void FieldComment()
        {
            Console.WriteLine("Opmerking (Optioneel):");
            SelectedItem.Comment = Console.ReadLine();
        }
        public void FieldListProducts()
        {
            List<Option> listoptions = new List<Option>();
            foreach (var l in SelectedItem.ListProducts)
            {
                var label = $"ID:{l.ID}; Naam gerecht:{l.Name}; Prijs: {l.Price}";
                listoptions.Add(new Option(label, FieldListProductsRemove, l.ID));
            }
            listoptions.Add(new Option("Voeg een artikel toe", FieldListProductsAdd));
            listoptions.Add(new Option("Opslaan?", SaveConfirm));
            new vMenu(listoptions);
        }
        public void FieldListProductsAdd()
        {
            List<Option> listoptions = new List<Option>();
            listoptions.Add(new Option("Terug", FieldListProducts));
            foreach (var l in ctlMain.products.GetProducts())
            {
                var label = $"Naam gerecht:{l.Name}; Prijs: {l.Price}";
                listoptions.Add(new Option(label, ProductSelect, l.ID));
            }
            new vMenu(listoptions);
        }
        public void FieldListProductsRemove(int aId)
        {
            Product Product = SelectedItem.ListProducts.Find(s => s.ID == aId);
            if (Product != null)
            {
                SelectedItem.ListProducts.Remove(Product);
            }
            Console.WriteLine($"Artikel verwijderd");
            FieldListProducts();
        }
        public void ProductSelect(int aId)
        {
            Product Product = ctlMain.products.GetID(aId);
            Console.WriteLine($"Geselecteerd: {Product.Name}");
            SelectedItem.ListProducts.Add(Product);
            FieldListProducts();
        }

        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
    internal class vAdminReservation: vReservation
    {
        public vAdminReservation()
        {
            options = new List<Option>
            {
                new Option("Nieuwe reservering", this.Add),
                new Option("Alle reserveringen", this.View),
                new Option("Verwijder alle reserveringen", DeleteAll),
                new Option("Terug", this.BackToMain),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }
        public override void Navigation()
        {
            new vMenu(options, "Reserveringen");
        }
        public override void Add()
        {
            SelectedItem = new Reservations();
            SelectedItem.ID = ctlMain.reservation.IncrementID();
            SelectedItem.CreateDateTime = DateTime.Now;
            SelectedItem.Status = "Actief";
            SelectedItem.ListProducts = new List<Product>();
            FieldReserveCode();
            FieldPersons();
            FieldDueDate();
            FieldComment();
            FieldUserSelect();
        }
        public override void View()
        {
            Console.WriteLine("Reserveringen.");
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            if (ctlMain.reservation.reservations != null)
            {
                string header = $" + - ID - Gast - Tafel - Datum - Aantal Artikelen - #Code";
                Console.WriteLine(header);
                foreach (var l in ctlMain.reservation.reservations)
                {
                    string duedt = l.DueDateTime.ToString("dd/MM/yyyy");
                    string label = $" - {l.ID} - {l.User.Username} - {l.DiningTable.Places} - {duedt} - {l.ReserveCode}";
                    listoptions.Add(new Option(label, EditItem, l.ID));
                }
                new vMenu(listoptions);
            } else
            {
                Console.WriteLine("Geen");
            }
        }
        public override void Edit()
        {
            Console.WriteLine("Reservering aanpassen.");
            List<Option> listoptions = new List<Option>()
            {
                new Option("Terug", Navigation),
                new Option("Aantal personen: "+SelectedItem.NumberGuests, FieldPersons),
                new Option("Reseringsdatum: "+SelectedItem.DueDateTime.ToString("dd/MM/yyyy"), FieldDueDate),
                new Option("Producten", FieldListProducts),
                new Option("Contactpersoon: "+(SelectedItem.User != null ? SelectedItem.User.FirstName: ""), FieldUserSelect),
                new Option("Opmerking: "+SelectedItem.Comment, FieldComment),
                new Option("Opslaan? ", SaveConfirm),
                new Option("Opzeggen? ", FieldComment),
                new Option("Verwijderen? ", Delete),
            };
            new vMenu(listoptions);
        }
        public void DeleteAll()
        {
            ctlMain.reservation.DeleteAll();
            Navigation();
        }
        public void Delete()
        {
            ctlMain.reservation.DeleteByItem(SelectedItem);
            SelectedItem = null;
            View();
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
            FieldListProducts();
        }
    }
}
