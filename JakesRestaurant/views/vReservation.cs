using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controllers;
using reservation;

namespace JakesRestaurant.views
{
    internal class vReservation
    {
        private ctlReservation ctl;
        public static List<Option> options;
        public vMenu menu { get; set; }
        public vReservation()
        {
            ctl = new ctlReservation();
            Console.WriteLine("Reserveringen");
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
            this.menu = new vMenu(options);
        }

        public void Add()
        {
            Reservations p = new Reservations();
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

            foreach (var l in ctl.GetList())
            {
                string createdt = l.CreateDateTime.ToString("dd/MM/yyyy");
                string duedt = l.DueDateTime.ToString("dd/MM/yyyy");
                string label = $" - {l.ID} - {l.UserId} - {l.TableId} - {createdt} - {duedt}";
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
                string createdt = l.CreateDateTime.ToString("dd/MM/yyyy");
                string duedt = l.DueDateTime.ToString("dd/MM/yyyy");
                string label = $" - {l.ID} - {l.UserId} - {l.TableId} - {createdt} - {duedt}";
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
            Console.WriteLine("user id:");
            p.UserId = int.Parse(Console.ReadLine());

            Console.WriteLine("tabel id:");
            p.TableId = int.Parse(Console.ReadLine());

            string line = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Invalid date, please retry");
                line = Console.ReadLine();
            }
            p.DueDateTime = dt;
        }

        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
}
