using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controllers;
using reservation;

namespace JakesRestaurant.views
{
    internal class vDiningtable
    {
        public ctlDiningTable ctl { get; set; } = new ctlDiningTable();
        public static List<Option> options = new List<Option> { };
        public vMenu menu { get; set; }
        public DiningTable SelectedItem { get; set; }
        public vDiningtable()
        {
        }
        public void Navigation()
        {
            Console.Clear();
            options = new List<Option> { };
            foreach (var l in ctlMain.diningtable.GetList())
            {
                var label = $"Tafel met {l.Places} zit plaatsen. Omschrijving: {l.Description}";
                options.Add(new Option(label, EditItem, l.ID));
            }
            options.Add(new Option("Voeg toe", Add));
            options.Add(new Option("Terug", BackToMain));
            options.Add(new Option("Afsluiten", () => Environment.Exit(0)));
            new vMenu(options, "Zit plaatsen instellingen.");
        }
        public void Add()
        {
            Console.Clear();
            SelectedItem = new DiningTable();
            SelectedItem.ID = ctlMain.diningtable.IncrementID();
            SelectedItem.Status = "Vrij";
            FieldPlaces();
            FieldDescription();
            ctlMain.diningtable.UpdateList(SelectedItem);
            Navigation();
        }
        public void EditItem(int aID)
        {
            SelectedItem = ctlMain.diningtable.GetID(aID);
            Edit();
        }
        public void Edit()
        {
            Console.Clear();
            Console.WriteLine("Reserveringen aanpassen");
            List<Option> listoptions = new List<Option>
            {
                new Option("Terug", Navigation),
                new Option("Zit plaatsen: "+SelectedItem.Places, InsertValue, 1),
                new Option("Omschrijving: "+SelectedItem.Description, InsertValue, 2),
                new Option("Verwijderen? ", Delete, SelectedItem.ID),
                new Option("Opslaan? ", SaveItem),
            };
            new vMenu(listoptions);
        }
        public void FieldPlaces()
        {
            Console.WriteLine("Plaatsen:");
            SelectedItem.Places = int.Parse(Console.ReadLine());
        }
        public void FieldDescription()
        {
            Console.WriteLine("Omschrijving:");
            SelectedItem.Description = Console.ReadLine();
        }
        public void SaveItem()
        {
            ctlMain.diningtable.UpdateList(SelectedItem);
            SelectedItem = null;
            Navigation();
        }
        public void InsertValue(int idx)
        {
            switch (idx)
            {
                case 1:
                    FieldPlaces();
                    break;
                case 2:
                    FieldDescription();
                    break;
                default:
                    break;
            }
            Edit();
        }
        public void Delete(int aID)
        {
            ctlMain.diningtable.DeleteByItem(SelectedItem);
            SelectedItem = null;
            Navigation();
        }
        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
    }
}
