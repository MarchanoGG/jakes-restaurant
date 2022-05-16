﻿using System;
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
        public List<vMenu> breadcrumbs{ get; set; }
        public List<string> test{ get; set; }
        public vMain()
        {
            productscontroller = new vExampleProducts();
            userscontroller = new vExampleUsers();
            options = new List<Option>
            {
                new Option("Products", productscontroller.Navigation),
                new Option("Users", userscontroller.Navigation),
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
