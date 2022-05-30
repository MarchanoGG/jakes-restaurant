using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Authentication
{
    class ctlUsers
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "auth.json");

        public string HashString(string text, string salt = "")
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

        public doUser FindById(int aID)
        {
            doUser res = null;

            foreach (doUser user in GetUsers())
            {
                if (user.ID == aID)
                {
                    res = user;
                }
            }

            return res;
        }

        public List<doUser> GetUsers()
        {
            List<doUser> existingUsers = ReadList<doUser>(path);

            return existingUsers;
        }

        public doUser CheckCredentials(string aUsername, string aPassword)
        {
            doUser res = null;

            List<doUser> users = ReadList<doUser>(path);

            doUser myUser = users.Find(match: i => i.Username == aUsername);

            if (myUser != null)
            {
                if (myUser.Password == HashString(aPassword))
                {
                    res = myUser;
                }
            }

            return res;
        }

        public bool CreateCredentials(string aUsername, string aPassword)
        {
            bool res = false;

            List<doUser> existingUsers = ReadList<doUser>(path);
            if (existingUsers.Find(match: i => i.Username == aUsername) == null)
            {
                int newID = 1;

                if (existingUsers.Count >= 1)
                {
                    newID = existingUsers[existingUsers.Count - 1].ID + 1;
                }

                doUser obj = new doUser
                {
                    Username = aUsername,
                    Password = HashString(aPassword)
                };

                existingUsers.Add(obj);

                WriteList(path, existingUsers);

                res = true;
            }

            return res;
        }

        public bool UpdateUser(doUser aUser)
        {
            bool res = false;

            List<doUser> existingUsers = ReadList<doUser>(path);

            doUser myUser = existingUsers.Find(match: i => i.Username == aUser.Username);

            if (myUser != null)
            {
                existingUsers[existingUsers.IndexOf(myUser)] = aUser;

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
