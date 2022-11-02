using System.Text.Json.Serialization;

namespace TakeFood.OrderService.ViewModel.Dtos.Address
{
    public class StoreAddressDto
    {
        [JsonPropertyName("Province")]
        public string province { get; set; }
        [JsonPropertyName("District")]
        public string district { get; set; }
        [JsonPropertyName("Town")]
        public string town { get; set; }
        [JsonPropertyName("Stress")]
        public string stress { get; set; }
        [JsonPropertyName("Lat")]
        public double lat { get; set; }
        [JsonPropertyName("lng")]
        public double lng { get; set; }
    }
}
