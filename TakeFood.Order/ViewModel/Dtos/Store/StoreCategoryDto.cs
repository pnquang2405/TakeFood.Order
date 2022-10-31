using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TakeFood.StoreService.ViewModel.Dtos.Store
{
    public class StoreCategoryDto
    {
        [JsonPropertyName("CategoryID")]
        [NotNull]
        public string? CategoryId { get; set; }
    }
}
