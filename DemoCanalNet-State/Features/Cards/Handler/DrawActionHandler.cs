using BlazorState;
using DemoCanalNet_State.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DemoCanalNet_State.Features.Cards
{
    public partial class CardState
    {
        public class DrawActionHandler : ActionHandler<DrawAction>
        {
            private readonly HttpClient _httpClient;

            public DrawActionHandler(IStore aStore, HttpClient httpClient) : base(aStore)
            {
                _httpClient = httpClient;
            }

            CardState State => Store.GetState<CardState>();

            public override async Task<Unit> Handle(DrawAction aAction, CancellationToken aCancellationToken)
            {
                State.Loading = true;
                await Task.Delay(5000);
                var response = await _httpClient.GetFromJsonAsync<DrawCardResponse>($"https://deckofcardsapi.com/api/deck/{aAction.DeckId}/draw/?count=1");
                if (response.Success)
                {
                    var card = response.Cards.First();

                    State.CurrentCard = card.Value;
                    State.CurrentSuit = card.Suit;
                    State.CurrentImage = card.Image;
                    State.RemainingCards = response.Remaining;
                    State.DeckId = response.DeckId;
                }
                else
                {
                    State.CurrentCard = "None";
                    State.CurrentSuit = "None";
                    State.CurrentImage = "https://i.pinimg.com/originals/10/80/a4/1080a4bd1a33cec92019fab5efb3995d.png";
                    State.RemainingCards = response.Remaining;
                }
                State.Loading = false;
                return await Unit.Task;
            }
        }
    }
}
