using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JakesRestaurant.views
{
    internal class vMain
    {
        public static List<Option> options;
        public vMenu menu { get; set; }
        public vProducts productscontroller { get; set; }
        public vUsers vusers { get; set; }
        public vDiningtable diningtablecontroller { get; set; }
        public vReservation vreservations { get; set; }
        public vLogin loginView { get; set; }
        public List<vMenu> breadcrumbs{ get; set; }
        public List<string> test{ get; set; }
        public vMain()
        {
            productscontroller = new vProducts();
            vusers = new vUsers();
            diningtablecontroller = new vDiningtable();
            vreservations = new vReservation();
            loginView = new vLogin();
            options = new List<Option>
            {
                new Option("Products", productscontroller.Navigation),
                new Option("Tafels", diningtablecontroller.Navigation),
                new Option("Reserveringen", vreservations.Navigation),
                new Option("Users", vusers.Navigation),
                new Option("Pas gebruiker aan", loginView.UpdateProfile),
                new Option("Terug naar login", loginView.BackToLogin),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }
        public void Navigation()
        {
            this.menu = new vMenu(options);
            //breadcrumbs.Add(menu);
            //productscontroller.breadcrumbs = breadcrumbs;
            //userscontroller.test = new List<string>();
            //userscontroller.test.Add("fooba");
        }
    }
}
