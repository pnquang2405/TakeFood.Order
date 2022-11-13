using System.Text.Json.Serialization;

namespace TakeFood.Order.ViewModel.Dtos.Order
{
    public class FoodOrderDto
    {
        [JsonPropertyName("FoodOrderID")]
        public string? FoodOrderId { get; set; }

        [JsonPropertyName("FoodID")]
        public string? FoodId { get; set; }
        [JsonPropertyName("FoodName")]
        public string? FoodName { get; set; }
        [JsonPropertyName("Price")]
        public double Price { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("ListTopping")]
        public List<ToppingOrderDto> ListTopping { get; set; }
    }
}
