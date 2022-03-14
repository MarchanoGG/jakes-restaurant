using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Authentication
{
    class TctlLogin
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "auth.conf.txt");

        public bool Login() 
        {
            bool res = false;

            Console.WriteLine(path);

            return res;
        }
    }
}
