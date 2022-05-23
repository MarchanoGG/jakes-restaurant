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
        public string title { get; set; }
        protected int origRow { get; set; }
        protected int origCol { get; set; }
        public vMenu(List<Option> options, string title = "")
        {
            // Set the default index of the selected item to be the first
            int index = 0;
            origRow = 0;
            origCol = 0;
            this.options = options;
            this.title = title;

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
                        WriteAt(" ", 0, index);
                        WriteAt(">", 0, ++index);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        WriteAt(" ", 0, index);
                        WriteAt(">", 0, --index);
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
                    break;
                    //index = 0;
                    //WriteMenu(options, options[index]);
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();
        }
        protected void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
                Console.SetCursorPosition(0, options.Count);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        public void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();
            if (title.Length > 0)
            {
                Console.WriteLine(title);
                ++this.origRow;
            }
            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
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

        //public Option(string name, object v)
        //{
        //    Name = name;
        //    V = v;
        //}
    }
}
