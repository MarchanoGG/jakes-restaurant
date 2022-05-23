using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Authentication;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace controllers
{
    internal class ctlUsers
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "auth.json");
        public List<User> users;

        public ctlUsers()
        {
            Load();
        }
        public User FindById(int id)
        {
            return users.Find(i => i.ID == id);
        }
        public void Load()
        {
            if (!File.Exists(path))
            {
                var myFile = File.Create(path);
                myFile.Close();
            }

            string json = File.ReadAllText(path);
            if (json != "")
            {
                users = JsonSerializer.Deserialize<List<User>>(json);
            }
            else
            {
                users = new List<User>();
            }

        }
    }
}
