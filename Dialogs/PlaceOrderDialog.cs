using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using Microsoft.Bot.Builder.FormFlow;

namespace MonikaBot
{
    [Serializable]
    public class PlaceOrderDialog : RootDialog // i mean.. since there are virtual functions in rootdialog.... no reason not to.. right?
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

                        // Actually process the sandwich order...
                        await context.PostAsync("Processed your order!");
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

    }
}