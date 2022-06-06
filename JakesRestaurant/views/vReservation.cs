using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controllers;
using reservation;
using Authentication;
using management;
using Restaurant;

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
            FieldDueTime();
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
            string header = "  ";
            if (ctlMain.reservation.reservations != null)
            {

                header += vMenu.EqualWidthCol("ID", 3);
                header += vMenu.EqualWidthCol("Tafel", 10);
                header += vMenu.EqualWidthCol("Datum", 10);
                header += vMenu.EqualWidthCol("#Code", 5);
                Console.WriteLine(header);
                foreach (var r in ctlMain.reservation.reservations)
                {
                    string label = "";
                    label += vMenu.EqualWidthCol(r.ID.ToString(), 3);
                    label += vMenu.EqualWidthCol(r.DiningTable.Places.ToString(), 10);
                    label += vMenu.EqualWidthCol(r.DueDateTimeStr, 10);
                    label += vMenu.EqualWidthCol(r.ReserveCode, 5);
                    listoptions.Add(new Option(label, EditItem, r.ID));
                }
            }
            else
            {
                Console.WriteLine("Geen");
            }
            listoptions.Add(new Option("Terug", Navigation));
            new vMenu(listoptions);
        }
        public virtual void EditItem(int aID)
        {
            SelectedItem = ctlMain.reservation.FindById(aID);
            Edit();
        }
        public virtual void Edit()
        {
            Console.WriteLine("Reserveringen aanpassen");
            List<Option> listoptions = new List<Option>()
            {
                new Option("Terug", Navigation),
                new Option("Aantal personen: "+SelectedItem.NumberGuests, InsertValue, 1),
                new Option("Reseringsdatum: "+SelectedItem.DueDateTimeStr, InsertValue, 2),
                new Option("Producten: "+SelectedItem.ProductSummary, InsertValue, 3),
                new Option("Opmerking: "+SelectedItem.Comment, InsertValue, 4),
                new Option("Annuleren? ", InsertValue, 5),
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
            if (SelectedItem.DueDateTime < DateTime.Now) {
                IsValid = false;
                Console.WriteLine("Datum is in het verleden");
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
            SaveItem();
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
            FieldOpeningTimes();
            Console.WriteLine("Reseringsdatum (Formaat dd/MM/yyyy):");
            string line = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Incorrecte datum formaat, probeer opnieuw");
                line = Console.ReadLine();
            }
            SelectedItem.DueDateTime = dt;
        }
        public void FieldOpeningTimes()
        {
            foreach (Restaurant.doOpeningTimes obj in ctlMain.openingtimes.GetOpeningTimes())
            {
                Console.WriteLine(obj.Summary());
            }
        }
        public void FieldDueTime()
        {
            Console.WriteLine("Reseringstijd (Formaat HH:mm):");
            string line = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParseExact(line, "HH:mm", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Incorrecte tijds formaat, probeer opnieuw");
                line = Console.ReadLine();
            }
            Console.WriteLine(dt.ToString("dd/MM/yyyy HH:mm"));
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
        }
        public void ItemCancel() 
        {
            TimeSpan diffDate = SelectedItem.DueDateTime - DateTime.Now;
            if (diffDate.TotalHours < 24)
            {
                Console.WriteLine("Annulering kan alleen 24 uur van tevoren gedaan worden.");
                Console.WriteLine("Maak contact met het restaurant.");
                Console.ReadKey();
            }
            else
            {
                SelectedItem.Status = "Geannuleerd";
                SaveConfirm();
                Console.ReadKey();
            }
        }
        public void FieldComment()
        {
            Console.WriteLine("Opmerking (Optioneel):");
            SelectedItem.Comment = Console.ReadLine();
        }
        public void FieldListProducts()
        {
            List<Option> listoptions = new List<Option>();
            Console.WriteLine("Verwijder uit reservering:");
            foreach (var l in SelectedItem.ListProducts)
            {
                var label = $"ID:{l.ID}; Naam gerecht:{l.Name}; Prijs: {l.Price}";
                listoptions.Add(new Option(label, FieldListProductsRemove, l.ID));
            }
            listoptions.Add(new Option("Voeg een artikel toe", FieldListProductsAdd));
            listoptions.Add(new Option("Opslaan", SaveConfirm));
            listoptions.Add(new Option("Terug", Edit));
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
        public virtual void InsertValue(int idx)
        {
            switch (idx)
            {
                case 1:
                    FieldPersons();
                    break;
                case 2:
                    FieldDueDate();
                    break;
                case 3:
                    FieldListProducts();
                    break;
                case 4:
                    FieldComment();
                    break;
                case 5:
                    ItemCancel();
                    break;
                default:
                    break;
            }
            Edit();
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
            string header = "  ";
            if (ctlMain.reservation.reservations != null)
            {

                header += vMenu.EqualWidthCol("ID", 3);
                header += vMenu.EqualWidthCol("Bezoekernaam", 20);
                header += vMenu.EqualWidthCol("Tafel", 10);
                header += vMenu.EqualWidthCol("Datum", 10);
                header += vMenu.EqualWidthCol("#Code", 5);
                Console.WriteLine(header);
                foreach (var r in ctlMain.reservation.reservations)
                {
                    string label = "";
                    label += vMenu.EqualWidthCol(r.ID.ToString(), 3);
                    label += vMenu.EqualWidthCol(r.User.Username, 20);
                    label += vMenu.EqualWidthCol(r.DiningTable.Places.ToString(), 10);
                    label += vMenu.EqualWidthCol(r.DueDateTimeStr, 10);
                    label += vMenu.EqualWidthCol(r.ReserveCode, 5);
                    listoptions.Add(new Option(label, EditItem, r.ID));
                }
            }
            else
            {
                Console.WriteLine("Geen");
            }
            listoptions.Add(new Option("Terug", Navigation));
            new vMenu(listoptions);
        }
        public override void Edit()
        {
            Console.WriteLine("Reservering aanpassen.");
            List<Option> listoptions = new List<Option>()
            {
                new Option("Terug", Navigation),
                new Option("Contactpersoon: "+(SelectedItem.User != null ? SelectedItem.User.FirstName: ""), InsertValue, 0),
                new Option("Aantal personen: "+SelectedItem.NumberGuests, InsertValue, 1),
                new Option("Reseringsdatum: "+SelectedItem.DueDateTimeStr, InsertValue, 2),
                new Option("Producten: "+SelectedItem.ProductSummary, InsertValue, 3),
                new Option("Opmerking: "+SelectedItem.Comment, InsertValue, 4),
                new Option("Reservering annuleren? ", InsertValue, 5),
                new Option("Reservering verwijderen? ", InsertValue, 6),
                new Option("Opslaan? ", SaveConfirm),
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
        public override void InsertValue(int idx)
        {
            switch (idx)
            {
                case 0:
                    FieldUserSelect();
                    break;
                case 1:
                    FieldPersons();
                    break;
                case 2:
                    FieldDueDate();
                    break;
                case 3:
                    FieldListProducts();
                    break;
                case 4:
                    FieldComment();
                    break;
                case 5:
                    ItemCancel();
                    break;
                case 6:
                    Delete();
                    break;
                default:
                    break;
            }
            Edit();
        }
    }
}
