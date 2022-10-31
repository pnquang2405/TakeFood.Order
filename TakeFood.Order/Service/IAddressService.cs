using TakeFood.StoreService.ViewModel.Dtos.Address;

namespace TakeFood.StoreService.Service
{
    public interface IAddressService
    {
        Task CreateAddress(AddressDto address);
        Task DeleteAddress(String id);
    }
}
