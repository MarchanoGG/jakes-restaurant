using System;
using JakesRestaurant.model.dataObjects.initialisation;

namespace JakesRestaurant.controllers
{
    public class ctlInitialise
    {
        public ctlInitialise()
        {
            // Escape String -> 
            string[][] Files = new string[][]
            {
                new string[] {"auth.json", "[\r\n\t{\r\n\t\t\"id\": 1,\r\n\t\t\"username\": \"admin\",\r\n\t\t\"password\": \"8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918\",\r\n\t\t\"firstName\": null,\r\n\t\t\"surname\": null,\r\n\t\t\"email\": null,\r\n\t\t\"phone\": null,\r\n\t\t\"birthdate\": \"0001-01-01T00:00:00\",\r\n\t\t\"status\": 0\r\n\t}\r\n]" }
            };
            Initialisation initialisation = new Initialisation(Files);
        }   
    }
}
