using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TakeFood.OrderService.ViewModel.Dtos.Store;

public class FilterStoreByCategoryId
{
    [JsonPropertyName("categoryId")]
    [Required]
    public string CategoryId { get; set; }
    [JsonPropertyName("lat")]
    [Required]
    public double Lat { get; set; }
    [JsonPropertyName("lng")]
    [Required]
    public double Lng { get; set; }
    [JsonPropertyName("radiusIn")]
    [Required]
    public double RadiusIn { get; set; }
    [JsonPropertyName("radiusOut")]
    [Required]
    public double RadiusOut { get; set; }
}
