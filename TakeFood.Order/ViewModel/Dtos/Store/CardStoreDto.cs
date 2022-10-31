using System.Text.Json.Serialization;

namespace TakeFood.StoreService.ViewModel.Dtos.Store;

public class CardStoreDto
{
    [JsonPropertyName("storeId")]
    public string StoreId { get; set; }
    [JsonPropertyName("name")]
    public string StoreName { get; set; }
    [JsonPropertyName("numOfReview")]
    public int NumOfReView { get; set; }
    [JsonPropertyName("start")]
    public double Star { get; set; }
    [JsonPropertyName("disstance")]
    public double Distance { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("img")]
    public string Image { get; set; }
}
