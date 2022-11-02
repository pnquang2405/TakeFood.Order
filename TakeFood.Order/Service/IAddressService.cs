using TakeFood.OrderService.ViewModel.Dtos.Address;

namespace TakeFood.OrderService.Service
{
    public interface IAddressService
    {
        Task CreateAddress(AddressDto address);
        Task DeleteAddress(String id);
    }
}
