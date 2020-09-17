using BlazorState;
using DemoCanalNet_State.Models;
using MediatR;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DemoCanalNet_State.Features.Cards
{
    public partial class CardState
    {
        public class ShuffleActionHandler : ActionHandler<ShuffleAction>
        {
            private readonly HttpClient _httpClient;

            public ShuffleActionHandler(IStore store, HttpClient httpClient) : base(store)
            {
                _httpClient = httpClient;
            }

            CardState State => Store.GetState<CardState>();

            public override async Task<Unit> Handle(ShuffleAction aAction, CancellationToken aCancellationToken)
            {
                var response = await _httpClient.GetFromJsonAsync<ShuffleResponse>("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");

                if (response.Success)
                {
                    State.DeckId = response.DeckId;
                    State.RemainingCards = response.Remaining;
                }
                else
                {
                    State.DeckId = null;
                    State.RemainingCards = 0;
                }

                return await Unit.Task;
            }
        }
    }
}
