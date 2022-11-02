using System.Text.Json.Serialization;

namespace TakeFood.OrderService.ViewModel.Dtos.Address
{
    public class AddressDto
    {
        [JsonPropertyName("Information")]
        public string information { get; set; }
        [JsonPropertyName("Address")]
        public string address { get; set; }
        [JsonPropertyName("AddressType")]
        public string addressType { get; set; }
        [JsonPropertyName("Lat")]
        public double lat { get; set; }
        [JsonPropertyName("Lng")]
        public double lng { get; set; }
    }
}
