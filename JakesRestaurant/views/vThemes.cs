using System;
using controllers;
using management;
using System.Collections.Generic;
using System.Linq;


namespace JakesRestaurant.views
{
    internal class vThemes
    {
        public List<Option> options;
        private static Theme d_currentTheme { get; set; }
        public vThemes()
        {
        }
        void DefaultMenu()
        {
            options = new List<Option>();
            options.Add(new Option("Voeg thema toe", AddTheme));
            options.Add(new Option("Lijst van themas", ViewThemes));
            options.Add(new Option("Terug", BackToMain));
            options.Add(new Option("Exit", () => Environment.Exit(0)));
        }
        public void Navigation()
        {
            DefaultMenu();
            new vMenu(options, "Thema instellingen");
        }
        public void AddTheme()
        {
            d_currentTheme = new Theme();
            d_currentTheme.ID = ctlMain.themes.IncrementID();
            FieldName();
            FieldStartDate();
            FieldEndDate();
            ctlMain.themes.UpdateList(d_currentTheme);
            Navigation();
        }
        public void ViewThemes()
        {
            Console.WriteLine("Reserveringen.");
            List<Option> listoptions = new List<Option>();
            string header = "  ";
            if (ctlMain.themes.GetThemes() != null)
            {

                header += vMenu.EqualWidthCol("ID", 3);
                header += vMenu.EqualWidthCol("Naam", 10);
                header += vMenu.EqualWidthCol("Start", 10);
                header += vMenu.EqualWidthCol("Eind", 10);
                Console.WriteLine(header);
                foreach (var r in ctlMain.themes.GetThemes())
                {
                    string label = "";
                    label += vMenu.EqualWidthCol(r.ID.ToString(), 3);
                    label += vMenu.EqualWidthCol(r.Name, 10);
                    label += vMenu.EqualWidthCol(r.StartDateStr, 10);
                    label += vMenu.EqualWidthCol(r.EndDateStr, 10);
                    listoptions.Add(new Option(label, EditItem, r.ID));
                }
            }
            else
            {
                Console.WriteLine("Geen");
            }
            listoptions.Add(new Option("Terug", Navigation));
            new vMenu(listoptions);
        }
        public virtual void EditItem(int aID)
        {
            d_currentTheme = ctlMain.themes.GetByID(aID);
            Edit();
        }
        public void SaveItem()
        {
            ctlMain.themes.UpdateList(d_currentTheme);
            d_currentTheme = null;
            ViewThemes();
        }
        public void Edit()
        {
            List <Option> itemlist= new List<Option>();
            if (d_currentTheme != null)
            {
                itemlist = new List<Option>();
                itemlist.Add(new Option("Terug", ViewThemes));
                itemlist.Add(new Option($"Naam: {d_currentTheme.Name}", InsertValue, 1));
                itemlist.Add(new Option($"Start datum: {d_currentTheme.StartDateStr}", InsertValue, 2));
                itemlist.Add(new Option($"Eind datum: {d_currentTheme.EndDateStr}", InsertValue, 3));
                itemlist.Add(new Option("Verwijderen?", Delete));
                itemlist.Add(new Option("Opslaan?", SaveItem));
                new vMenu(itemlist, $"Thema: {d_currentTheme.Name}");
            }
            else
            {
                Console.WriteLine("Kan thema niet vinden!");
                ViewThemes();
            }
        }
        private void FieldName()
        {
            if (d_currentTheme != null)
            {
                Console.WriteLine("Naam:");
                d_currentTheme.Name = Console.ReadLine();
                ctlMain.themes.UpdateList(d_currentTheme);
            }
        }
        private void FieldStartDate()
        {
            Console.WriteLine("Start datum (Formaat dd/MM/yyyy):");
            DateTime startDate;

            while (true)
            {
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
                {
                    d_currentTheme.StartDate = startDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Probeer opnieuw!");
                }
            }
        }
        private void FieldEndDate()
        {
            Console.WriteLine("Eind datum (Formaat dd/MM/yyyy):");
            DateTime endDate;

            while (true)
            {
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
                {
                    d_currentTheme.EndDate = endDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Probeer opnieuw!");
                }
            }
        }
        public void Delete()
        {
            ctlMain.themes.Delete(d_currentTheme.ID);
            ViewThemes();
        }
        public void InsertValue(int idx)
        {
            switch (idx)
            {
                case 1:
                    FieldName();
                    break;
                case 2:
                    FieldStartDate();
                    break;
                case 3:
                    FieldEndDate();
                    break;
                default:
                    break;
            }
            Edit();
        }
        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
}