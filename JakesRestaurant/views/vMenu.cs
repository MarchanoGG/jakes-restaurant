using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JakesRestaurant.views
{
    internal class vMenu
    {
        public List<Option> options { get; set; }
        public vMenu(List<Option> options)
        {
            // Set the default index of the selected item to be the first
            int index = 0;
            this.options = options;
            // Write the menu out
            WriteMenu(options, options[index]);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        WriteMenu(options, options[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(options, options[index]);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if(options[index].ID != 0)
                        options[index].Selected(options[index].ID);
                    else
                        options[index].VoidSelected();

                    Console.ReadKey();
                    index = 0;
                    WriteMenu(options, options[index]);
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();
        }

        public void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();

            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
        }
    }
    public class Option
    {
        public string Name { get; }
        public Action VoidSelected { get; }
        public Action<int> Selected { get; }
        public int ID { get; set; }

        public Option(string name, Action selected)
        {
            Name = name;
            VoidSelected = selected;
        }

        public Option(string name, Action<int> selected, int id = 0) 
        {
            Name = name;
            Selected = selected;
            ID = id;
        }
    }
}
