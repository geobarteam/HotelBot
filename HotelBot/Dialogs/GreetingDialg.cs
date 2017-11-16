using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HotelBot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi I'm John bot!");
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var message = await activity;
            var userName = String.Empty;

            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("Hi what is your name?");
                userName = message.Text;
                context.UserData.SetValue("Name", userName);
            }
            else
            {
                await context.PostAsync($"Hi {userName}, how can I help you?");
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}