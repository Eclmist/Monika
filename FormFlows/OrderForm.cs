﻿using Microsoft.Bot.Builder.FormFlow;
using System;

namespace MonikaBot
{
    [Serializable]
    public class OrderForm
    {
        [Prompt("What would you like to order? Please input each item individually")]
        public string ItemName { get; set; }

        [Prompt("How many of those do would you like?")]
        public ushort Quantity { get; set; }

        public static IForm<OrderForm> BuildOrderForm()
        {
            return new FormBuilder<OrderForm>().AddRemainingFields().Build();
        }
    }
}