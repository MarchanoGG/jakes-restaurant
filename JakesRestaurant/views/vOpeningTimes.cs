using System;
using System.Collections.Generic;

namespace JakesRestaurant.views
{
    class vOpeningTimes
    {
        private Restaurant.ctlOpeningTimes controller = new Restaurant.ctlOpeningTimes();
        private Restaurant.doOpeningTimes chosenTime = null;

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
                UpdateTimes();
            }
            else
            {
                ShowTimes();
            }
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
            this.menu = new vMenu(options, "Openingstijden");
        }

        private void UpdateTimes()
        {
            vMain mainmenu = new vMain();

            options = new List<Option> { };

            foreach (Restaurant.doOpeningTimes obj in controller.GetOpeningTimes())
            {
                options.Add(new Option(obj.Summary(), UpdateTime, obj.ID));
            }

            options.Add(new Option("Terug", () => mainmenu.Navigation()));
            this.menu = new vMenu(options, "Openingstijden");
        }

        public void UpdateTime(int ID) {
            vMain mainmenu = new vMain();

            options = new List<Option> { };

            foreach (Restaurant.doOpeningTimes obj in controller.GetOpeningTimes())
            {
                if (obj.ID == ID) {
                    this.chosenTime = obj;
                }
            }

            if (this.chosenTime != null)
            {
                options.Add(new Option("Openingstijden aanpassen van " + this.chosenTime.Summary()));
                options.Add(new Option("Geopend:                    " + this.chosenTime.Opened, this.InsertValue, 1));
                options.Add(new Option("Openingstijd (uren):        " + this.chosenTime.StartingHours, this.InsertValue, 2));
                options.Add(new Option("Openingstijd (minuten):     " + this.chosenTime.StartingMinutes, this.InsertValue, 3));
                options.Add(new Option("Sluitingstijd: (uren):      " + this.chosenTime.ClosingHours, this.InsertValue, 4));
                options.Add(new Option("Sluitingstijd: (minuten):   " + this.chosenTime.ClosingMinutes, this.InsertValue, 5));
            }

            options.Add(new Option("Terug", this.UpdateTimes));
            this.menu = new vMenu(options, "Wijzig de openingstijden van " + this.chosenTime.Summary());
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
