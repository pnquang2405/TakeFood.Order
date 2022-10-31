using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TakeFood.StoreService.ViewModel.Dtos.Store;

public class GetStoreNearByDto
{
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
