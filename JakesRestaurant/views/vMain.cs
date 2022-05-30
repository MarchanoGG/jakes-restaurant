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
        public vOpeningTimes openingTimesView { get; set; }
        public List<vMenu> breadcrumbs{ get; set; }
        public vMain()
        {
            vProducts = new vProducts();
            vusers = new vUsers();
            vDiningtable = new vDiningtable();
            loginView = new vLogin();
            openingTimesView = new vOpeningTimes();

            if (Program.MyUser.HasPrivilege() == true)
            {
                vReservation = new vAdminReservation();
                options = new List<Option>
                {
                    new Option("Jake's restaurant"),
                    new Option("Thema: <To be implemented>"),
                    new Option("Locatie: Wijnhaven 107, 3011 WN in Rotterdam"),
                    new Option(""),
                    new Option("Openingstijden", openingTimesView.Navigation),
                    new Option("Thema's", loginView.CheckRes),
                    new Option("Producten", vProducts.Navigation),
                    new Option("Gebruikers", loginView.UsersList),
                    new Option("Tafels", vDiningtable.Navigation),
                    new Option("Reserveringen", vReservation.Navigation),
                    new Option("Pas profiel aan", loginView.UpdateProfile),
                    new Option("Terug naar login", loginView.BackToLogin),
                    new Option("Afsluiten", () => Environment.Exit(0)),
                };
            }
            else
            {
                vReservation = new vReservation();
                options = new List<Option>
                {
                    new Option("Jake's restaurant"),
                    new Option("Thema: <To be implemented>"),
                    new Option("Locatie: Wijnhaven 107, 3011 WN in Rotterdam"),
                    new Option(""),
                    new Option("Maak een reservering", vReservation.Add),
                    new Option("Mijn reserveringen", vReservation.View),
                    new Option("Pas profiel aan", loginView.UpdateProfile),
                    new Option("Terug naar login", loginView.BackToLogin),
                    new Option("Afsluiten", () => Environment.Exit(0)),
                };
            }
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
