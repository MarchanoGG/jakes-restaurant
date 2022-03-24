using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Authentication
{
    class User
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "auth.json");

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("birthdate")]
        public int BirthDate { get; set; }

        public string Summary()
        {
            return FirstName + " " + Surname;
        }

        public User CheckCredentials(string aUsername, string aPassword)
        {
            User res = null;

            List<User> users = ReadList<User>(path);

            User myUser = users.Find(match: i => i.Username == aUsername);

            if (myUser != null)
            {
                if (myUser.Password == aPassword)
                {
                    res = myUser;
                }
            }

            return res;
        }

        public bool CreateCredentials(string aUsername, string aPassword)
        {
            bool res = false;

            List<User> existingUsers = ReadList<User>(path);
            if (existingUsers.Find(match: i => i.Username == aUsername) == null)
            {
                User obj = new User
                {
                    Username = aUsername,
                    Password = aPassword
                };

                existingUsers.Add(obj);

                WriteList(path, existingUsers);

                res = true;
            }

            return res;
        }

        public static T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);

            T res = JsonSerializer.Deserialize<T>(text);

            return res;
        }

        public static List<T> ReadList<T>(string filePath)
        {
            List<T> res = new List<T>();
            string text = File.ReadAllText(filePath);

            T[] arr = JsonSerializer.Deserialize<T[]>(text);

            foreach (T obj in arr)
            {
                res.Add(obj);
            }

            return res;
        }

        public void WriteList<T>(string filePath, List<T> aList)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(aList));
        }
    }
}
