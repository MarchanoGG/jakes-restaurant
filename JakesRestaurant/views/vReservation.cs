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
                new Option("Afsluiten", () => Environment.Exit(0)),
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
            FieldDueDateTime();
            FieldPersons();
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
                header += vMenu.EqualWidthCol("Aantal personen", 20);
                header += vMenu.EqualWidthCol("Tafel", 20);
                header += vMenu.EqualWidthCol("Datum", 20);
                header += vMenu.EqualWidthCol("#Code", 5);
                Console.WriteLine(header);
                foreach (var r in ctlMain.reservation.reservations)
                {
                    string label = "";
                    label += vMenu.EqualWidthCol(r.ID.ToString(), 3);
                    label += vMenu.EqualWidthCol($"{r.NumberGuests.ToString()} personen", 20);
                    label += vMenu.EqualWidthCol($"{r.DiningTable.Places.ToString()} zit plaatsen", 20);
                    label += vMenu.EqualWidthCol(r.DueDateTimeStr, 20);
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
            Console.WriteLine("Reservering aanpassen");
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
        public void SaveConfirm()
        {
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
            Console.Clear();
            while (SelectedItem.DiningTable == null)
            {
                Console.WriteLine("Aantal personen:");
                int persons = int.Parse(Console.ReadLine());
                SelectedItem.NumberGuests = persons;
                SelectedItem.DiningTable = ctlMain.diningtable.FindByPersonByDate(persons, SelectedItem.DueDateTime);
                SelectedItem.DiningTable.Status = "Bezet";
                if (SelectedItem.DiningTable == null)
                {
                    Console.WriteLine("Geen tafel/zitplek beschikbaar");
                    //Navigation();
                }
            }
        }
        public void FieldDueDateTime()
        {
            Console.Clear();
            FieldOpeningTimes();
            Console.WriteLine("\nReseringsdatum (Formaat dd/MM/yyyy HH:mm):");
            string line = Console.ReadLine();
            DateTime dt;
            if (!DateTime.TryParseExact(line, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Incorrecte datum formaat. Probeer opnieuw. Druk Enter.");
                Console.ReadKey();
                FieldDueDateTime();
            }
            if (dt < DateTime.Now)
            {
                Console.WriteLine("Datum is in het verleden. Probeer opnieuw. Druk Enter.");
                Console.ReadKey();
                FieldDueDateTime();
            }
            if (!ctlMain.reservation.IsAvailableByDate(dt))
            {
                Console.WriteLine("Opgegeven datum is niet beschikbaar. Probeer opnieuw. Druk Enter.");
                Console.ReadKey();
                FieldDueDateTime();
            }
            SelectedItem.DueDateTime = dt;
            Console.WriteLine($"Opgegeven datum & tijd: {SelectedItem.DueDateTimeStr}.");
            SelectedItem.Theme = ctlMain.reservation.GetThemeByDate(dt);
            if (SelectedItem.Theme != null)
                Console.WriteLine($"Thema: {SelectedItem.Theme.Name}.");
            else
                Console.WriteLine($"Voorlopig geen thema & arrangement beschikbaar.");
            Console.ReadKey();
        }
        public void FieldOpeningTimes()
        {
            foreach (doOpeningTimes obj in ctlMain.openingtimes.GetOpeningTimes())
            {
                Console.WriteLine(obj.Summary());
            }
        }
        public void FieldStatus()
        {
            List<Option> listoptions = new List<Option>()
            {
                new Option("Ja", ItemCancel),
                new Option("Nee", Edit),
            };
            new vMenu(listoptions, "Wilt u deze reserviering annuleren?");
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
            Console.Clear();
            Console.WriteLine("Opmerking (Optioneel):");
            SelectedItem.Comment = Console.ReadLine();
        }
        public void FieldListProducts()
        {
            Console.Clear();
            List<Option> listoptions = new List<Option>();
            if (SelectedItem.Theme != null)
                Console.WriteLine($"Thema: {SelectedItem.Theme.Name} \n");
            else
                SaveConfirm();
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
            foreach (var l in ctlMain.products.GetProductsByThemeID(SelectedItem.Theme.ID))
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
            Product Product = ctlMain.products.GetByID(aId);
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
                    FieldDueDateTime();
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
                new Option("Afsluiten", () => Environment.Exit(0)),
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
            FieldDueDateTime();
            FieldPersons();
            FieldComment();
            FieldUserSelectAdd();
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
                header += vMenu.EqualWidthCol("Aantal personen", 20);
                header += vMenu.EqualWidthCol("Tafel", 20);
                header += vMenu.EqualWidthCol("Datum", 20);
                //header += vMenu.EqualWidthCol("Thema", 20);
                header += vMenu.EqualWidthCol("#Code", 5);
                Console.WriteLine(header);
                foreach (var r in ctlMain.reservation.reservations)
                {
                    string label = "";
                    label += vMenu.EqualWidthCol(r.ID.ToString(), 3);
                    label += vMenu.EqualWidthCol(r.User != null ? r.User.Username : "", 20);
                    label += vMenu.EqualWidthCol($"{r.NumberGuests.ToString()} personen", 20);
                    label += vMenu.EqualWidthCol($"{r.DiningTable.Places.ToString()} zit plaatsen", 20);
                    label += vMenu.EqualWidthCol(r.DueDateTimeStr, 20);
                    //label += vMenu.EqualWidthCol(r.t, 20);
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
            Console.Clear();
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
        }
        public void FieldUserSelectAdd()
        {
            Console.Clear();
            List<Option> listoptions = new List<Option>();
            foreach (var l in ctlMain.users.users)
            {
                var label = $"{l.Username}";
                listoptions.Add(new Option(label, UserSelectAdd, l.ID));
            }
            new vMenu(listoptions);
        }
        public void UserSelectAdd(int aId)
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
                    FieldDueDateTime();
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
