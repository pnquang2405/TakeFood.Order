using System.Text.Json.Serialization;

namespace TakeFood.StoreService.ViewModel.Dtos.Food
{
    public class ToppingCreateFoodDto
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
