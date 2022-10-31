using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using TakeFood.StoreService.ViewModel.Dtos.Address;

namespace TakeFood.StoreService.ViewModel.Dtos.Store
{
    public class CreateStoreDto
    {
        [JsonPropertyName("name")]
        [NotNull]
        public string StoreName { get; set; }

        [JsonPropertyName("phone")]
        [NotNull]
        public string StorePhone { get; set; }

        [JsonPropertyName("address")]
        [NotNull]
        public StoreAddressDto StoreAddress { get; set; }
        
        [JsonPropertyName("urlStoreImage")]
        [NotNull]
        public string urlStoreImage { get; set; }

        [JsonPropertyName("urlKitchenImage")]
        [NotNull]
        public string urlKitchenImage { get; set; }

        [JsonPropertyName("urlMenuImage")]
        [NotNull]
        public string urlMenuImage { get; set; }

        [JsonPropertyName("nameOwner")]
        [NotNull]
        public string nameOwner { get; set; }

        [JsonPropertyName("cmnd")]
        [NotNull]
        public string cmnd { get; set; }

        [JsonPropertyName("urlFontCmndImage")]
        [NotNull]
        public string urlFontCmndImage { get; set; }

        [JsonPropertyName("urlBackCmndImage")]
        [NotNull]
        public string urlBackCmndImage { get; set; }

        [JsonPropertyName("urlLicenseImage")]
        [NotNull]
        public string urlLicenseImage { get; set; }

        [JsonPropertyName("nameSTKOwner")]
        [NotNull]
        public string nameSTKOwner { get; set; }

        [JsonPropertyName("STK")]
        [NotNull]
        public string STK { get; set; }

        [JsonPropertyName("NameBank")]
        [NotNull]
        public string NameBank { get; set; }

        [JsonPropertyName("BankBranch")]
        [NotNull]
        public string BankBranch { get; set; }

        [JsonPropertyName("TaxID")]
        [NotNull]
        public string TaxID { get; set; }

        [JsonPropertyName("Categories")]
        [NotNull]
        public List<StoreCategoryDto> Categories { get; set; }
    }
}
