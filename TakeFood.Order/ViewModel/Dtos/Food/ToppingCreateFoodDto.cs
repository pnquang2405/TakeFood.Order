using System.Text.Json.Serialization;

namespace TakeFood.OrderService.ViewModel.Dtos.Food
{
    public class ToppingCreateFoodDto
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
