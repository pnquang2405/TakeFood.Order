using System.Text.Json.Serialization;

namespace Order.ViewModel.Dtos.Order
{
    public class ViewOrderDto
    {
        [JsonPropertyName("id")]
        public string? ID { get; set; }
        [JsonPropertyName("nameUser")]
        public string? NameUser { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("sdt")]
        public string? Phone { get; set; }
        [JsonPropertyName("totalPrice")]
        public double? TotalPrice { get; set; }
        [JsonPropertyName("dateOrder")]
        public DateTime? DateOrder { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }
    }
}
