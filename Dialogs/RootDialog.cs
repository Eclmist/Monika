using System;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;


namespace MonikaBot
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi! Monika here! What kind of poem shall we write today?");

            PromptDialog.Choice(
                context: context,
                resume: ChoiceReceivedAsync,
                options: new[] { "Place New Order", "Fulfil an Order" },
                prompt: "Menu",
                retry: "What do you mean? :c",
                promptStyle: PromptStyle.Auto
            );

            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            // Do i still need this after prompt dialog?
            var msg = await argument;

            if (msg.Text.ToLower().Contains("fulfil"))
            {
                // context.Call<object>(new PlaceOrderDialog(), ChildDialogComplete);
            }
            else if (msg.Text.ToLower().Contains("order"))
            {
                // any better way to just call choicereceivedasync from here?
                context.Call<object>(new PlaceOrderDialog(), ChildDialogComplete);
            }
            else
            {
                await context.PostAsync("I didn't understand what you meant :c");
                context.Wait(MessageReceivedAsync);
            }
        }

        public async Task ChoiceReceivedAsync(IDialogContext context, IAwaitable<string> argument)
        {
            string reponse = await argument;

            switch(reponse)
            {
                case "Place New Order":
                    {
                        context.Call<object>(new PlaceOrderDialog(), ChildDialogComplete);
                            break;
                    }
                case "Fulfil an Order":
                    {
                        //context.Call<object>(new FulfilOrderDialog(), () => { context.Done(this); });
                            break;
                    }
                default:
                    {
                        context.Done(this);
                        break;
                    }
            }
        }
        public virtual async Task ChildDialogComplete(IDialogContext context, IAwaitable<object> response)
        {
            //await context.PostAsync("We hope this has been useful :)");
            //NyxPromptDialog<string>.Choice(
            //                context: context,
            //                resume: ChoiceReceivedAsync,
            //                options: MainMenuOptions,
            //                prompt: "Hi I am the SST Bot. Nice to meet you! Feel free to ask me anything you wish to know about our school.",
            //                retry: "I didn't understand. Please try again.",
            //                promptStyle: PromptStyle.Auto
            //                );

            await context.PostAsync("Okay, let me know if you need anything else!");
            context.Done(this);
        }
    }
}