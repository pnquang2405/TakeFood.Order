using System.Text.Json.Serialization;

namespace TakeFood.Order.ViewModel.Dtos.Order
{
    public class OrderDetailsDto
    {
        [JsonPropertyName("OrderID")]
        public string? OrderId { get; set; }
        [JsonPropertyName("NameUser")]
        public string? NameUser { get; set; }
        [JsonPropertyName("DateOrder")]
        public DateTime? DateOrder { get; set; }
        [JsonPropertyName("Phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("PaymentMethod")]
        public string? PaymentMethod { get; set; }
        [JsonPropertyName("Address")]
        public string? Address { get; set; }
        [JsonPropertyName("Note")]
        public string? Note { get; set; }

        [JsonPropertyName("TempTotalPrice")]
        public double TempTotalPrice { get; set; }
        [JsonPropertyName("Discount")]
        public string? Discount { get; set; }
        [JsonPropertyName("FeeShip")]
        public double FeeShip { get; set; }
        [JsonPropertyName("TotalPrice")]
        public double TotalPrice { get; set; }
        [JsonPropertyName("ListFood")]
        public List<FoodOrderDto>? ListFood { get; set; }
    }
}
