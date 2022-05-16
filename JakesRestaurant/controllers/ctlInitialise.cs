using System;
using JakesRestaurant.model.dataObjects.initialisation;

namespace JakesRestaurant.controllers
{
    public class ctlInitialise
    {
        public ctlInitialise()
        {
            string[] Files = { "auth.json", "products.json" };
            Initialisation initialisation = new Initialisation(Files);
        }   
    }
}
