using System;
using System.Collections.Generic;
using System.Linq;

namespace MonikaBot
{
    [Serializable]
    class OrderEntry
    {
        public Item item;
        public ushort quantity;
        public string shop;

        public OrderEntry(Item i, string s)
        {
            item = i;
            quantity = 1;
            shop = s;
        }
    }

    [Serializable]
    class Cart
    {
        List<OrderEntry> orderEntries;

        public Cart()
        {
            orderEntries = new List<OrderEntry>();
        }

        public void AddItem(Item i, ushort quantity = 1, string shop = "")
        {
            var e = orderEntries.FirstOrDefault(entry => entry.item.Id == i.Id);

            if (e != null)
            {
                e.quantity += quantity; 
            }
            else
            {
                orderEntries.Add(new OrderEntry(i, shop));
            }
        }

        public void RemoveItem(Item i)
        {
            var e = orderEntries.FirstOrDefault(entry => entry.item.Id == i.Id);

            if (e != null)
            {
                e.quantity--;
                if (e.quantity <= 0)
                {
                    orderEntries.Remove(e);
                }
            }
        }

        // TODO: Replace with cards eventually
        public string PrintCart()
        {
            string cart = "Current Cart: \n";

            foreach (OrderEntry entry in orderEntries)
            {
                cart += entry.item.Name + "\t" + entry.quantity + "\n";
            }

            return cart;

        }

        public bool Any()
        {
            return orderEntries.Any();
        }
    }
}