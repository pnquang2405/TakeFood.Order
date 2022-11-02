
using TakeFood.OrderService.ViewModel.Dtos.Image;

namespace TakeFood.OrderService.Service
{
    public interface IImageService
    {
        Task<List<ImageDto>> GetAllImages();
        Task<ImageDto> GetImageById(String id);
        Task CreateImage(string storeID, string cateigoryID, ImageDto image);
        Task UpdateImage(string id, ImageDto image);
        Task DeleteImage(String id);
        Task<String> GetStoreSlug(string storeId);
    }
}
