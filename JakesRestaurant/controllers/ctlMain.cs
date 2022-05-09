using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JakesRestaurant.controllers
{
    internal class ctlMain
    {
        public static List<Option> options;
        public ctlMenu menu { get; set; }
        public ctlExampleProducts productscontroller { get; set; }
        public ctlExampleUsers userscontroller { get; set; }
        public List<ctlMenu> breadcrumbs{ get; set; }
        public List<string> test{ get; set; }
        public ctlMain()
        {
            productscontroller = new ctlExampleProducts();
            userscontroller = new ctlExampleUsers();
            options = new List<Option>
            {
                new Option("Products", productscontroller.Navigation),
                new Option("Users", userscontroller.Navigation),
                new Option("Exit", () => Environment.Exit(0)),
            };
        }
        public void Navigation()
        {
            menu = new ctlMenu(options);
            //breadcrumbs.Add(menu);
            //productscontroller.breadcrumbs = breadcrumbs;
            userscontroller.test = new List<string>();
            userscontroller.test.Add("fooba");
        }
    }
}
