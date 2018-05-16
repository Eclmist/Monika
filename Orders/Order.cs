using System;
using System.List;
using System.LINQ;

namespace MonikaBot
{
    struct OrderEntry
    {
        public Item item;
        public ushort quantity;   

        public OrderEntry(Item i)
        {
            item = i;
            quantity = 0;
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
            if (orderEntries.Any(entry => entry.item.Id == i.Id))
            {
                orderEntries.Where(entry => entry.item.id == i.Id)
                {
                    entry.quantity++;
                }
            }
            else
            {
                orderEntries.Add(new OrderEntry(i));
            }
        }
    }
}