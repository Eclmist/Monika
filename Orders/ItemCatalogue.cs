using System;
using System.Collections.Generic;
using System.Linq;

namespace MonikaBot
{
    class ItemCatalogue
    {
        static Item[] catalogue = new[]
        {
            new Item(0, "Sayuri", 0.01d),
            new Item(1, "McSpicy", 4.95d),
            new Item(2, "McWings", 4.50),
            new Item(3, "Cheese Burger", 2.95d),
            new Item(4, "Pizza", 11.60d),
            new Item(5, "Cheese", 1.0d),
            new Item(6, "Orange", 0.5d),
            new Item(7, "Apple", 0.4d),
        };

        //public static void AddToCatalogue(string name, double price)
        //{
        //    if (catalogue.Any(i => i.Name == name))
        //    {
        //        return;
        //    }

        //    Item item = new Item((uint)catalogue.Count(), name, price);
        //    catalogue.Add(item);
        //}

        public static Item FindItemByName(string name)
        {
            var item = catalogue.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());

            return item;
        }
    }
}