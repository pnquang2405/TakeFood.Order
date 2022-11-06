using System.Text.Json.Serialization;

namespace TakeFood.Order.ViewModel.Dtos.Order
{
    public class ToppingOrderDto
    {
        [JsonPropertyName("ToppingID")]
        public string? ToppingId { get; set; }
        [JsonPropertyName("ToppingName")]
        public string? ToppingName { get; set; }
        [JsonPropertyName("Price")]
        public double Price { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
    }
}
