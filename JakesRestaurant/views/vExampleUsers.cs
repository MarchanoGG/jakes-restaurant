using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JakesRestaurant.views
{
    internal class vExampleUsers
    {
        public static List<Option> options;
        public vMenu menu { get; set; }
        public List<vMenu> breadcrumbs { get; set; }
        public List<string> test { get; set; }
        public vExampleUsers()
        {
            Console.WriteLine("Users");
            options = new List<Option>
            {
                new Option("Add", this.Add),
                new Option("View", this.View),
                new Option("Back to menu", this.BackToMain),
                new Option("Exit", () => Environment.Exit(0)),
            };
            test = new List<string>();
        }
        public void Navigation()
        {
            this.menu = new vMenu(options);
        }

        public void BackToMain()
        {
            vMain main = new vMain();
            main.Navigation();
        }
        public void Add()
        {
            Console.WriteLine("Add text: ");
            string inp =  Console.ReadLine();
            Console.WriteLine(inp);
        }
        public void View()
        {
            Console.WriteLine("View data here.");
            string inp = Console.ReadLine();
            Console.WriteLine(inp);
        }
    }
}
