using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controllers;

namespace JakesRestaurant.views
{
    internal class vMain
    {
        public static List<Option> options;
        public vMenu menu { get; set; }
        public vProducts vProducts { get; set; }
        public vUsers vusers { get; set; }
        public vDiningtable vDiningtable { get; set; }
        public vReservation vReservation { get; set; }
        public vLogin loginView { get; set; }
        public List<vMenu> breadcrumbs{ get; set; }
        public vMain()
        {
            vProducts = new vProducts();
            vusers = new vUsers();
            vDiningtable = new vDiningtable();
            vReservation = new vReservation();
            loginView = new vLogin();
            options = new List<Option>
            {
                new Option("Products", vProducts.Navigation),
                new Option("Tafels", vDiningtable.Navigation),
                new Option("Reserveringen", vReservation.Navigation),
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
