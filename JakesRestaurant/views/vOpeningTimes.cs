using System;
using System.Collections.Generic;
using controllers;

namespace JakesRestaurant.views
{
    class vOpeningTimes
    {
        private ctlOpeningTimes controller = ctlMain.openingtimes;
        private doOpeningTimes chosenTime = null;

        public vOpeningTimes()
        {
        }

        public void Navigation()
        {
            List<Option> options = new List<Option>();
            vMain mainmenu = new vMain();
            if (Program.MyUser.HasPrivilege() == true)
            {
                options = new List<Option>
                {
                    new Option("Openingstijden bekijken", ShowTimes),
                    new Option("Openingstijden aanpassen", UpdateTimes),
                    new Option("Terug", () => mainmenu.Navigation())
                };
            }
            else
            {
                options = new List<Option>
                {
                    new Option("Openingstijden bekijken", ShowTimes),
                    new Option("Terug", () => mainmenu.Navigation())
                };
            }
            new vMenu(options, "Openingstijden");
        }

        public void ShowTimes()
        {
            vMain mainmenu = new vMain();
            List<Option> itemlist = new List<Option>();
            foreach (doOpeningTimes obj in ctlMain.openingtimes.GetOpeningTimes())
            {
                Console.WriteLine(obj.Summary());
            }
            //foreach (doOpeningTimes obj in controller.GetOpeningTimes())
            //{
            //    options.Add(new Option(obj.Summary()));
            //}
            itemlist.Add(new Option("Terug", () => mainmenu.Navigation()));
            new vMenu(itemlist);
        }

        private void UpdateTimes()
        {
            vMain mainmenu = new vMain();
            List<Option> itemlist = new List<Option>();
            foreach (doOpeningTimes obj in controller.GetOpeningTimes())
            {
                itemlist.Add(new Option(obj.Summary(), UpdateTime, obj.ID));
            }

            itemlist.Add(new Option("Terug", () => mainmenu.Navigation()));
            new vMenu(itemlist, "Openingstijden aanpassen");
        }

        public void UpdateTime(int ID) {
            vMain mainmenu = new vMain();
            List<Option> itemlist = new List<Option>();
            foreach (doOpeningTimes obj in controller.GetOpeningTimes())
            {
                if (obj.ID == ID) {
                    this.chosenTime = obj;
                }
            }

            if (this.chosenTime != null)
            {
                itemlist.Add(new Option("Geopend:                    " + this.chosenTime.Opened, this.InsertValue, 1));
                itemlist.Add(new Option("Openingstijd (uren):        " + this.chosenTime.StartingHours, this.InsertValue, 2));
                itemlist.Add(new Option("Openingstijd (minuten):     " + this.chosenTime.StartingMinutes, this.InsertValue, 3));
                itemlist.Add(new Option("Sluitingstijd: (uren):      " + this.chosenTime.ClosingHours, this.InsertValue, 4));
                itemlist.Add(new Option("Sluitingstijd: (minuten):   " + this.chosenTime.ClosingMinutes, this.InsertValue, 5));
            }

            itemlist.Add(new Option("Terug", this.UpdateTimes));
            new vMenu(itemlist, "Openingstijden aanpassen van " + chosenTime.Summary());
        }

        private void InsertValue(int idx)
        {
            switch (idx)
            {
                case 1:
                    Console.WriteLine("Voer in of het restaurant op deze dag is geopend (J / N):");
                    string Opened = Console.ReadLine();

                    while (Opened != "J" && Opened != "N" && Opened != "j" && Opened != "n")
                    {
                        Console.WriteLine("Invalide keuze, keuzes zijn (J)a of (N)ee");
                        Opened = Console.ReadLine();
                    }

                    if (Opened == "J" || Opened == "j")
                    {
                        this.chosenTime.Opened = true;
                    }
                    else
                    {
                        this.chosenTime.Opened = false;
                    }

                    controller.UpdateTime(this.chosenTime);
                    break;
                case 2:
                    Console.WriteLine("Voer nieuwe openingstijd in uren in (1 t/m 24):");
                    string OpeningHours = Console.ReadLine();
                    while (!int.TryParse(OpeningHours, out _) || int.Parse(OpeningHours) > 24 || int.Parse(OpeningHours) < 1)
                    {
                        Console.WriteLine("Invalide keuze, keuze kan alleen bestaan uit een cijfer tussen de 1 en 24!");
                        OpeningHours = Console.ReadLine();
                    }
                    this.chosenTime.StartingHours = int.Parse(OpeningHours);

                    controller.UpdateTime(this.chosenTime);
                    break;
                case 3:
                    Console.WriteLine("Voer nieuwe openingstijd in minuten in (0 t/m 59):");
                    string OpeningMinutes = Console.ReadLine();
                    while (!int.TryParse(OpeningMinutes, out _) || int.Parse(OpeningMinutes) > 59 || int.Parse(OpeningMinutes) < 0)
                    {
                        Console.WriteLine("Invalide keuze, keuze kan alleen bestaan uit een cijfer tussen de 0 en 59!");
                        OpeningMinutes = Console.ReadLine();
                    }
                    this.chosenTime.StartingMinutes = int.Parse(OpeningMinutes);

                    controller.UpdateTime(this.chosenTime);
                    break;
                case 4:
                    Console.WriteLine("Voer nieuwe openingstijd in uren in (1 t/m 24):");
                    string ClosingHours = Console.ReadLine();
                    while (!int.TryParse(ClosingHours, out _) || int.Parse(ClosingHours) > 24 || int.Parse(ClosingHours) < 1)
                    {
                        Console.WriteLine("Invalide keuze, keuze kan alleen bestaan uit een cijfer tussen de 1 en 24!");
                        ClosingHours = Console.ReadLine();
                    }
                    this.chosenTime.ClosingHours = int.Parse(ClosingHours);

                    controller.UpdateTime(this.chosenTime);
                    break;
                case 5:
                    Console.WriteLine("Voer nieuwe openingstijd in minuten in (0 t/m 59):");
                    string ClosingMinutes = Console.ReadLine();
                    while (!int.TryParse(ClosingMinutes, out _) || int.Parse(ClosingMinutes) > 59 || int.Parse(ClosingMinutes) < 0)
                    {
                        Console.WriteLine("Invalide keuze, keuze kan alleen bestaan uit een cijfer tussen de 0 en 59!");
                        ClosingMinutes = Console.ReadLine();
                    }
                    this.chosenTime.ClosingMinutes = int.Parse(ClosingMinutes);

                    controller.UpdateTime(this.chosenTime);
                    break;
                default:
                    break;
            }

            UpdateTime(this.chosenTime.ID);
        }
    }
}
