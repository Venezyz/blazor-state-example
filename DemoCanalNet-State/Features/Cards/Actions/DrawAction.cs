using BlazorState;

namespace DemoCanalNet_State.Features.Cards
{
    public partial class CardState
    {
        public class DrawAction : IAction
        {
            public string DeckId { get; private set; }

            public DrawAction(string deckId)
            {
                DeckId = deckId;
            }
        }
    }
}
