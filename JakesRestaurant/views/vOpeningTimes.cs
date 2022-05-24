using System;
using System.Collections.Generic;

namespace JakesRestaurant.views
{
    class vOpeningTimes
    {
        private Restaurant.ctlOpeningTimes controller = new Restaurant.ctlOpeningTimes();

        public static List<Option> options;
        public vMenu menu { get; set; }
        public List<vMenu> breadcrumbs { get; set; }
        public vOpeningTimes()
        {
        }

        public void Navigation()
        {
            vMain mainmenu = new vMain();
            if (Program.MyUser.HasPrivilege() == true)
            {
                options = new List<Option>
                {
                    new Option("Openingstijden bekijken", this.ShowTimes),
                    new Option("Openingstijden aanpassen", this.UpdateTimes),
                    new Option("Terug", () => mainmenu.Navigation())
                };
            }
            else
            {
                options = new List<Option>
                {
                    new Option("Openingstijden bekijken", this.ShowTimes),
                    new Option("Terug", () => mainmenu.Navigation())
                };
            }
            this.menu = new vMenu(options);
        }

        private void ShowTimes()
        {
            vMain mainmenu = new vMain();

            options = new List<Option> { };

            foreach (Restaurant.doOpeningTimes obj in controller.GetOpeningTimes())
            {
                options.Add(new Option(obj.Summary()));
            }

            options.Add(new Option("Terug", () => mainmenu.Navigation()));
            this.menu = new vMenu(options);
        }

        private void UpdateTimes() { 
        }
    }
}
