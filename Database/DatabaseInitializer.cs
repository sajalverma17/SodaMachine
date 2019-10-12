using SodaMachine.Models;
using System.Collections.Generic;

namespace SodaMachine.Database
{
    public class DatabaseInitializer
    {
        public static void Initialize(SodaMachineDBContext dBContext)
        {
            //Todo : Read invetory from a local JSON file placed .
            var sodaInventory = new List<Soda>() { 
                new Soda { Id = 1, Name = "coke", Nr = 5, Price = 20 }, 
                new Soda { Id = 2, Name = "sprite", Nr = 3, Price = 10 }, 
                new Soda { Id = 3, Name = "fanta", Nr = 3, Price = 5 } };

            var sodaMachine = new Machine { Id = 1, Money = 10, Sodas = sodaInventory };

            dBContext.Add(sodaMachine);
            dBContext.SaveChanges();
        }
    }
}
