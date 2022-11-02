using Order.Model.Entities.Image;
using Order.Model.Repository;
using TakeFood.OrderService.ViewModel.Dtos.Image;

namespace TakeFood.OrderService.Service.Implement
{
    public class ImageService : IImageService
    {
        private readonly IMongoRepository<Image> ImageRepository;

        public ImageService(IMongoRepository<Image> mongoRepository)
        {
            ImageRepository = mongoRepository;
        }

        public async Task CreateImage(string storeID, string categoryID, ImageDto image)
        {
            Image _image = new Image()
            {
                StoreId = storeID,
                CategoryId = categoryID,
                Url = image.Url,
            };
            await ImageRepository.InsertAsync(_image);
        }

        public Task DeleteImage(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ImageDto>> GetAllImages()
        {
            throw new NotImplementedException();
        }

        public Task<ImageDto> GetImageById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetStoreSlug(string storeId)
        {
            return (await ImageRepository.FindOneAsync(x => x.StoreId == storeId)).Url;
        }

        public Task UpdateImage(string id, ImageDto image)
        {
            throw new NotImplementedException();
        }
    }
}
