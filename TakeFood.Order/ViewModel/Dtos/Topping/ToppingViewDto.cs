using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TakeFood.OrderService.ViewModel.Dtos.Topping
{
    public class ToppingViewDto
    {
        [JsonPropertyName("ID")]
        [NotNull]
        public string ID { get; set; }

        [JsonPropertyName("Name")]
        [NotNull]
        public string Name { get; set; }

        [JsonPropertyName("State")]
        [NotNull]
        public string State { get; set; }

        [JsonPropertyName("Price")]
        [NotNull]
        public double Price { get; set; }
    }
}
