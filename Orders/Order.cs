using System;
using System.Collections.Generic;
using System.Linq;

namespace MonikaBot
{
    class OrderEntry
    {
        public Item item;
        public ushort quantity;   

        public OrderEntry(Item i)
        {
            item = i;
            quantity = 1;
        }
    }

    class Order
    {
        List<OrderEntry> orderEntries;

        public Order()
        {
            orderEntries = new List<OrderEntry>();
        }

        public void AddItem(Item i)
        {
            var e = orderEntries.FirstOrDefault(entry => entry.item.Id == i.Id);

            if (e != null)
            {
                e.quantity++; 
            }
            else
            {
                orderEntries.Add(new OrderEntry(i));
            }
        }
    }
}