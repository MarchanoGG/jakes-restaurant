using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication;
using controllers;

namespace JakesRestaurant.views
{
    internal class vUsers
    {
        private ctlUsers ctlU;
        public static List<Option> options;
        public vMenu menu { get; set; }
        public vUsers()
        {
            ctlU = new ctlUsers();
            options = new List<Option>
            {
                new Option("Toevoegen", this.Add),
                new Option("View", this.View),
                new Option("Back to menu", this.BackToMain),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }
        public void Navigation()
        {
            this.menu = new vMenu(options, "Users");
        }
        public void View()
        {
            Console.WriteLine("Gebruikers.");
            List<Option> listoptions = new List<Option>();

            listoptions.Add(new Option("Terug", Navigation));

            string header = $" + - ID - Naam";
            foreach (var l in ctlU.users)
            {
                string label = $" - {l.ID} - {l.Username} - ";
                listoptions.Add(new Option(label, Edit, l.ID));
            }

            this.menu = new vMenu(listoptions);
        }
        public void Edit(int aID)
        {
            // Get from parameter
            doUser p = ctlU.FindById(aID);

            //Form(ref p);

            // If success than update product
            ctlU.UpdateList(p);

            // Go back to View navigation
            View();
        }

        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
        public void Add()
        {
            Console.WriteLine("Add text: ");
            string inp = Console.ReadLine();
            Console.WriteLine(inp);
        }
    }
}
