using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TakeFood.StoreService.ViewModel.Dtos.Image
{
    public class ImageDto
    {
        [JsonPropertyName("url")]
        [NotNull]
        public string Url { get; set; }
    }
}
