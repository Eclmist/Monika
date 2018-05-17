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

            context.Call(MakeOrderDialog(), ChildDialogComplete);
        }
        
        internal static IDialog<OrderForm> MakeOrderDialog()
        {
            return Chain.From(() => FormDialog.FromForm(OrderForm.BuildOrderForm))
                .Do(async (context, order) =>
                {
                    try
                    {
                        var completed = await order;

                        // cart keeps calling constructor wtf
                        Cart cart = context.UserData.GetValueOrDefault<Cart>("Cart");
                        cart.AddItem(ItemCatalogue.FindItemByName(completed.ItemName), completed.Quantity, completed.Shop);

                  
                    }
                    catch (FormCanceledException<OrderForm> e)
                    {
                        string reply;
                        if (e.InnerException == null)
                        {
                            reply = $"You quit on {e.Last} -- maybe you can finish next time!";
                        }
                        else
                        {
                            reply = "Sorry, I've had a short circuit. Please try again.";
                        }
                        await context.PostAsync(reply);
                    }
                });
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            // doesnt make any sense to handle 'back' here but we can't possibly wrap EVERYTHING in a giant form flow
            // rightttttttttttt?
            var message = await argument;

            // potential bug here if a food item somehow has the word back in it :thinking:
            if (message.Text.ToLower().Contains("back"))
            {
                context.Done(this);
            }
            else
            {
            }
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