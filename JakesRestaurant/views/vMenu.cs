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
        protected static int index { get; set; }
        protected static int origRow { get; set; }
        protected static int origCol { get; set; }
        public vMenu(List<Option> options, string title = "")
        {
            // Set the default index of the selected item to be the first
            index = 0;
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
                    options[index].Call();
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            //Console.ReadKey();
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
            origRow = Console.CursorTop;
            if (title.Length > 0)
            {
                Console.WriteLine(title);
                ++origRow;
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
        public bool ActionIsSet { get; } = false;
        public Action VoidSelected { get; }
        public Action<int> Selected { get; }
        public int ID { get; } = 0;
        public OptionDelegateObj Callback { get; }
        public dynamic AnyObject { get; }

        public Option()
        {
        }
        public Option(string name)
        {
            Name = name;
            ActionIsSet = false;
        }

        public Option(string name, Action selected)
        {
            Name = name;
            VoidSelected = selected;
            ActionIsSet = true;
        }

        public Option(string name, Action<int> selected, int id = 0)
        {
            Name = name;
            Selected = selected;
            ID = id;
            ActionIsSet = true;
        }
        public Option(string name, OptionDelegateObj selected, dynamic obj)
        {
            Name = name;
            Callback = selected;
            ActionIsSet = true;
            AnyObject = obj;
        }
        public void Call()
        {
            if (ActionIsSet == true)
                if (Callback != null && AnyObject != null)
                    Callback(AnyObject);
                else if (Selected != null && ID != 0)
                    Selected(ID);
                else if (VoidSelected != null)
                    VoidSelected();
        }

        //public Option(string name, object v)
        //{
        //    Name = name;
        //    V = v;
        //}
    }
    public delegate void OptionDelegateObj(dynamic obj);

    public class Table
    {

    }
    //public class Text: Option
    //{

    //}

}
