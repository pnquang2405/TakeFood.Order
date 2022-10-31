
using StoreService.Model.Entities.Store;
using TakeFood.StoreService.ViewModel.Dtos.Store;

namespace Order.Service
{
    public interface IStoreService
    {
        List<Store> getAllStores();
        Task CreateStore(string ownerID, CreateStoreDto store);
        Task<List<CardStoreDto>> GetStoreNearByAsync(GetStoreNearByDto getStoreNearByDto);
        Task<List<CardStoreDto>> FilterStoreNearByAsync(FilterStoreByCategoryId filterStoreByCategory);
        /// <summary>
        /// Insert crawl data from foody
        /// </summary>
        /// <returns></returns>
        Task InertCrawlData();
        /// <summary>
        /// Insert menu store data from foody
        /// </summary>
        /// <returns></returns>
        Task InertMenuCrawlDataAsync();
        /// <summary>
        /// Find store by name and sort by distance
        /// </summary>
        /// <returns></returns>
        Task<List<CardStoreDto>> FindStoreByNameAsync(string keyword, double lat, double lng, int start);
        /// <summary>
        /// Get Store details
        /// </summary>
        /// <returns></returns>
        Task<StoreDetailsDto> GetStoreDetailAsync(string storeId, double lat, double lng);
    }
}
