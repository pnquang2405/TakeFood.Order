using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TakeFood.OrderService.ViewModel.Dtos.Topping
{
    public class CreateToppingDto
    {
        [JsonPropertyName("Name")]
        [NotNull]
        public string Name { get; set; }

        [JsonPropertyName("Price")]
        [NotNull]
        public double Price { get; set; }
    }
}
