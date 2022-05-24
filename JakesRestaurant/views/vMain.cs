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
        public vExampleProducts productscontroller { get; set; }
        public vExampleUsers userscontroller { get; set; }
        public vLogin loginView { get; set; }
        public List<vMenu> breadcrumbs{ get; set; }
        public List<string> test{ get; set; }
        public vMain()
        {
            productscontroller = new vExampleProducts();
            loginView = new vLogin();

            if (Program.MyUser.HasPrivilege() == true)
            {
                options = new List<Option>
                {
                    new Option("Thema's", loginView.CheckRes),
                    new Option("Producten", productscontroller.Navigation),
                    new Option("Gebruikers", loginView.UsersList),
                    new Option("Pas profiel aan", loginView.UpdateProfile),
                    new Option("Terug naar login", loginView.BackToLogin),
                    new Option("Afsluiten", () => Environment.Exit(0)),
                };
            } else
            {
                options = new List<Option>
                {
                    new Option("Reserveer een tafel", loginView.CheckRes),
                    new Option("Bekijk uw reserveringen", loginView.CheckRes),
                    new Option("Producten", productscontroller.Navigation),
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
