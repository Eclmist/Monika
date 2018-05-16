using System;

namespace MonikaBot
{
    class Item
    {
        public uint Id
        {
            get; private set;
        }

        public String Name
        {
            get; private set;
        }

        public double Price
        {
            get; private set;
        }
        
        public Item(uint id, String name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}