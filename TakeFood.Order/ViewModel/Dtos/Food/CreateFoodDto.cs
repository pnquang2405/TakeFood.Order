using System.Text.Json.Serialization;
using TakeFood.OrderService.ViewModel.Dtos.Topping;

namespace TakeFood.OrderService.ViewModel.Dtos.Food
{
    public class CreateFoodDto
    {
        [JsonPropertyName("UrlImage")]
        public string urlImage { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Descript { get; set; }

        [JsonPropertyName("Price")]
        public double Price { get; set; }

        [JsonPropertyName("CategoryID")]
        public string CategoriesID { get; set; }

        [JsonPropertyName("ListTopping")]
        public List<ToppingCreateFoodDto> ListTopping { get; set; }

        [JsonPropertyName("State")]
        public bool State { get; set; }
    }
}
