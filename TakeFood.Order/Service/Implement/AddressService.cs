using StoreService.Model.Entities.Address;
using StoreService.Model.Repository;
using TakeFood.StoreService.ViewModel.Dtos.Address;

namespace TakeFood.StoreService.Service.Implement
{
    public class AddressService : IAddressService
    {
        private readonly IMongoRepository<Address> _AddressRepository;

        public AddressService(IMongoRepository<Address> addressRepository)
        {
            _AddressRepository = addressRepository;
        }

        public async Task CreateAddress(AddressDto address)
        {
            Address _address = new Address()
            {
                Information = address.information != null ? address.information : address.address,
                Addrress = address.address,
                AddressType = address.addressType != null ? address.addressType : "Cửa hàng",
                Lat = address.lat,
                Lng = address.lng,
            };

            await _AddressRepository.InsertAsync(_address);
        }

        public async Task DeleteAddress(string id)
        {
            await _AddressRepository.RemoveAsync(id);
        }
    }
}
