using System.Text.Json.Serialization;

namespace DemoCanalNet_State.Models
{
    public class DrawCardResponse
    {
        public bool Success { get; set; }
        public Card[] Cards { get; set; }
        [JsonPropertyName("deck_id")]
        public string DeckId { get; set; }
        public int Remaining { get; set; }
    }
}
