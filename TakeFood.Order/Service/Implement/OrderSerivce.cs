using Order.Model.Entities.Address;
using Order.Model.Entities.User;
using Order.Model.Repository;
using Order.ViewModel.Dtos.Order;

namespace Order.Service.Implement
{
    public class OrderSerivce : IOrderService
    {
        private readonly IMongoRepository<Order.Model.Entities.Order.Order> _MongoRepository;
        private readonly IMongoRepository<User> _UserRepository;
        private readonly IMongoRepository<Address> _AddressRepository;
        
        public OrderSerivce(IMongoRepository<Order.Model.Entities.Order.Order> mongoRepository, IMongoRepository<User> mongoUser
            , IMongoRepository<Address> addressRepository)
        {
            _MongoRepository = mongoRepository;
            _UserRepository = mongoUser;
            _AddressRepository = addressRepository;
        }

        public async Task<List<ViewOrderDto>> GetAllOrder()
        {
            List<ViewOrderDto> orderList = new();
            List<Order.Model.Entities.Order.Order> orders = (List<Model.Entities.Order.Order>)await _MongoRepository.GetAllAsync();

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    ViewOrderDto item = new ViewOrderDto()
                    {
                        ID = order.Id,
                        NameUser = await _UserRepository.FindByIdAsync(order.UserId) != null ? (await _UserRepository.FindByIdAsync(order.UserId)).Name : "no Name",
                        Address = await _AddressRepository.FindByIdAsync(order.AddressId) != null ? ((await _AddressRepository.FindByIdAsync(order.AddressId)).Addrress) : "no Address",
                        Phone = order.PhoneNumber,
                        TotalPrice = order.Total,
                        DateOrder = order.CreatedDate,
                        State = order.Sate
                    };
                    orderList.Add(item);
                }
            }
            return orderList;
        }

        public async Task<List<ViewOrderDto>> GetAllOrderByStatus(string status)
        {
            List<Order.Model.Entities.Order.Order> orders = (List<Order.Model.Entities.Order.Order>)await _MongoRepository.FindAsync(x => x.Sate == status);
            List<ViewOrderDto> orderList = new();

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    ViewOrderDto item = new ViewOrderDto()
                    {
                        ID = order.Id,
                        NameUser = await _UserRepository.FindByIdAsync(order.UserId) != null ? (await _UserRepository.FindByIdAsync(order.UserId)).Name : "no Name",
                        Address = await _AddressRepository.FindByIdAsync(order.AddressId) != null ? ((await _AddressRepository.FindByIdAsync(order.AddressId)).Addrress) : "no Address",
                        Phone = order.PhoneNumber,
                        TotalPrice = order.Total,
                        DateOrder = order.CreatedDate,
                        State = order.Sate
                    };
                    orderList.Add(item);
                }
            }
            return orderList;
        }
    }
}
