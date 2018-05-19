using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using Microsoft.Bot.Builder.FormFlow;

namespace MonikaBot
{
    [Serializable]
    public class PlaceOrderDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("You can order anything (except cigarettes) on CrowdPotato. " +
                "You can choose the amount of delivery fee you would like to offer. " +
                "This whole amount will go to the courier. " +
                "We will also charge a small transaction fee of 3% on the total cost of the items you order.");

            var orderForm = new FormDialog<OrderForm>(new OrderForm(), OrderForm.BuildOrderForm, FormOptions.PromptInStart, null);
            context.Call(orderForm, ChildDialogComplete);
            // context.Call(MakeOrderDialog(), ChildDialogComplete);
        }
        
        public virtual async Task ChildDialogComplete(IDialogContext context, IAwaitable<object> response)
        {

            Cart cart = context.UserData.GetValueOrDefault<Cart>("Cart");
            
            // Actually process the sandwich order...
            await context.PostAsync(cart.PrintCart());
            PromptDialog.Choice(
                context: context,
                resume: ChoiceReceivedAsync,
                options: new[] { "Yes", "No" },
                prompt: "Are the order details so far correct?",
                promptStyle: PromptStyle.Auto
                );  

            context.Done(this);
        }

        public async Task ChoiceReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            string reponse = await argument;

            switch(reponse)
            {
                case "No":
                    {
                        // make some stuff happen here to edit order

                            break;
                    }
                default:
                    {
                        context.Done(this);
                        break;
                    }
            }
        }
    }
}