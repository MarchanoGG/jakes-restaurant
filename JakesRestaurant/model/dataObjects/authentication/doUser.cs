using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Authentication
{
    class doUser
    {
        [JsonPropertyName("id")]
        public int ID { get; }

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
        public int Status { get; }

        public doUser() {
            Status = 0;
        }

        public doUser(doUser obj) {
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
            return Username; //FirstName + " " + Surname;
        }

        public bool HasPrivilege()
        {
            return Status == 1;
        }
    }
}
