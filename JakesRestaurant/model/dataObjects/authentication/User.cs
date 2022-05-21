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
        public int ID { get; set; }

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
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
        public User() { 
        }
        public User(User obj) {
            this.ID = obj.ID;
            this.Username = obj.Username;
            this.Password = obj.Password;
            this.FirstName = obj.FirstName;
            this.Surname = obj.Surname;
            this.Email = obj.Email;
            this.Phone = obj.Phone;
            this.BirthDate = obj.BirthDate;
            this.Status = obj.Status;
        }

        public string Summary()
        {
            return Username;//FirstName + " " + Surname;
        }

        public User CheckCredentials(string aUsername, string aPassword)
        {
            User res = null;

            List<User> users = ReadList<User>(path);

            User myUser = users.Find(match: i => i.Username == aUsername);

            if (myUser != null)
            {
                if (myUser.Password == HashString(aPassword))
                {
                    res = myUser;
                }
            }

            return res;
        }

        public List<User> GetUsers()
        {
            List<User> existingUsers = ReadList<User>(path);

            return existingUsers;
        }

        public bool CreateCredentials(string aUsername, string aPassword)
        {
            bool res = false;

            List<User> existingUsers = ReadList<User>(path);
            if (existingUsers.Find(match: i => i.Username == aUsername) == null)
            {
                int newID = 1;

                if (existingUsers.Count >= 1)
                {
                    newID = existingUsers[existingUsers.Count - 1].ID + 1;
                }

                User obj = new User
                {
                    Username = aUsername,
                    Password = HashString(aPassword),
                    ID = newID
                };

                existingUsers.Add(obj);

                WriteList(path, existingUsers);

                res = true;
            }

            return res;
        }

        public bool UpdateUser(User aObject)
        {
            bool res = false;

            List<User> existingUsers = ReadList<User>(path);

            User myUser = existingUsers.Find(match: i => i.Username == aObject.Username);

            if (myUser != null)
            {
                existingUsers[existingUsers.IndexOf(myUser)] = aObject;

                WriteList(path, existingUsers);

                res = true;
            }

            return res;
        }

        public bool HasPrivilege()
        {
            return Status == 1;
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

        static string HashString(string text, string salt = "")
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
    }
}
