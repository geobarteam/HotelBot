using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HotelBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace HotelBot.Dialogs
{
    [LuisModel("00e967f8-18fe-4852-8e77-a758a3f45a6c", "1e117a3da9204470adf8562aed349cb9")]
    [Serializable]
    public class LuisDialog: LuisDialog<RoomsReservation>
    {
        private readonly BuildFormDelegate<RoomsReservation> _reserveRoom;

        public LuisDialog(BuildFormDelegate<RoomsReservation> reserveRoom)
        {
            this._reserveRoom = reserveRoom;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't understand.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            //context.Call(new GreetingDialog(), CallBack);
    
            var userName = context.Activity.From.Name;
            await context.PostAsync($"Hi {userName} I'm John Bot, how can I help you?");
            context.Wait(MessageReceived);
        }

        private async Task CallBack(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

        [LuisIntent("ReserveRoom")]
        public async Task RoomReservation(IDialogContext context, LuisResult result)
        {
            var enrollmentForm = new FormDialog<RoomsReservation>(new RoomsReservation(), this._reserveRoom, FormOptions.PromptInStart);
            context.Call<RoomsReservation>(enrollmentForm, CallBack);
        }

        [LuisIntent("QueryAmenities")]
        public async Task QueryAmenities(IDialogContext context, LuisResult result)
        {
            foreach (var entity in result.Entities.Where(Entity => Entity.Type == "Amenity"))
            {
                var value = entity.Entity.ToLower();
                if (value == "pool" || value == "gym" || value == "wifi" || value == "towels")
                {
                    await context.PostAsync("Yes we have that!");
                    context.Wait(MessageReceived);
                    return;
                }
                else
                {
                    await context.PostAsync("I'm sorry we don't have this type of amenity.");
                    context.Wait(MessageReceived);
                    return;
                }
            }
            await context.PostAsync("I'm sorry we don't have that.");
            context.Wait(MessageReceived);
            return;
        }

    }
}