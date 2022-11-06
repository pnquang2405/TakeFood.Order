using MongoDB.Driver.Linq;
using Order.Model.Entities.Address;
using Order.Model.Entities.Food;
using Order.Model.Entities.Order;
using Order.Model.Entities.Topping;
using Order.Model.Entities.User;
using Order.Model.Repository;
using Order.ViewModel.Dtos.Order;
using TakeFood.Order.ViewModel.Dtos.Order;
using TakeFood.Order.ViewModel.Dtos.Revenue;
using TakeFood.UserOrder.ViewModel.Dtos;

namespace Order.Service.Implement
{
    public class OrderSerivce : IOrderService
    {
        private readonly IMongoRepository<Order.Model.Entities.Order.Order> _MongoRepository;
        private readonly IMongoRepository<User> _UserRepository;
        private readonly IMongoRepository<Address> _AddressRepository;
        private readonly IMongoRepository<FoodOrder> _FoodOrderRepository;
        private readonly IMongoRepository<Food> _FoodRepository;
        private readonly IMongoRepository<Topping> _ToppingRepository;
        private readonly IMongoRepository<ToppingOrder> _ToppingOrderRepository;
        
        public OrderSerivce(IMongoRepository<Order.Model.Entities.Order.Order> mongoRepository, IMongoRepository<User> mongoUser
            , IMongoRepository<Address> addressRepository, IMongoRepository<FoodOrder> foodOrderRepository, IMongoRepository<Food> foodRepository
            , IMongoRepository<Topping> toppingRepository, IMongoRepository<ToppingOrder> toppingOrderReppsitory)
        {
            _MongoRepository = mongoRepository;
            _UserRepository = mongoUser;
            _AddressRepository = addressRepository;
            _FoodOrderRepository = foodOrderRepository;
            _FoodRepository = foodRepository;
            _ToppingRepository = toppingRepository;
            _ToppingOrderRepository = toppingOrderReppsitory;
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

        public async Task<OrderDetailsDto> GetDetailsOrder(string orderId)
        {
            Order.Model.Entities.Order.Order order = await _MongoRepository.FindByIdAsync(orderId);
            OrderDetailsDto orderDetailsDto = new();
            if(order != null)
            {
                orderDetailsDto.OrderId = order.Id;
                orderDetailsDto.Note = order.Note;
                orderDetailsDto.DateOrder = order.ReceiveTime;
                orderDetailsDto.NameUser = await _UserRepository.FindByIdAsync(order.UserId) != null ? (await _UserRepository.FindByIdAsync(order.UserId)).Name : "Phạm Ngọc Quang";
                orderDetailsDto.PaymentMethod = order.PaymentMethod;
                orderDetailsDto.Address = await _AddressRepository.FindByIdAsync(order.AddressId) != null ? (await _AddressRepository.FindByIdAsync(order.AddressId)).Addrress : "08 Hà Văn Tính - Hòa Khánh Nam - Liên Chiểu - TP.Đà Nẵng";
                List<FoodOrder>? foodOrders = await _FoodOrderRepository.FindAsync(x => x.OrderId == orderId) != null ? ((List<FoodOrder>)await _FoodOrderRepository.FindAsync(x => x.OrderId == orderId)) : null;
                List<FoodOrderDto> foodListOrder = new();
                if(foodOrders != null)
                {
                    foreach(FoodOrder foodOrder in foodOrders)
                    {
                        FoodOrderDto tempItem = new()
                        {
                            FoodOrderId = foodOrder.Id,
                            FoodId = foodOrder.FoodId,
                            FoodName = await _FoodRepository.FindByIdAsync(foodOrder.FoodId) != null ? (await _FoodRepository.FindByIdAsync(foodOrder.FoodId)).Name : "No name",
                            Quantity = foodOrder.Quantity,
                            Price = await _FoodRepository.FindByIdAsync(foodOrder.FoodId) != null ? (await _FoodRepository.FindByIdAsync(foodOrder.FoodId)).Price : 10000,
                        };
                        foodListOrder.Add(tempItem);
                    }
                    orderDetailsDto.ListFood = foodListOrder;
                }
            }
            return orderDetailsDto;
        }

        public async Task<List<ToppingOrderDto>> GetToppingsByFoodOrderID(string FoodOrderID)
        {
            List<ToppingOrderDto> ListToppingOrder = new();
            List<ToppingOrder> toppingOrders = new();
            if (await _ToppingOrderRepository.FindAsync(x => x.FoodOrderId == FoodOrderID) != null)
            {
                toppingOrders = (List<ToppingOrder>)await _ToppingOrderRepository.FindAsync(x => x.FoodOrderId == FoodOrderID);
                foreach(ToppingOrder topingOrder in toppingOrders)
                {
                    ToppingOrderDto temp = new()
                    {
                        ToppingId = topingOrder.ToppingId,
                        ToppingName = await _ToppingRepository.FindByIdAsync(topingOrder.ToppingId) != null ? (await _ToppingRepository.FindByIdAsync(topingOrder.ToppingId)).Name : "Topping thêm",
                        Price = await _ToppingRepository.FindByIdAsync(topingOrder.ToppingId) != null ? (await _ToppingRepository.FindByIdAsync(topingOrder.ToppingId)).Price : 10000,
                        Quantity = topingOrder.Quantity
                    };
                    ListToppingOrder.Add(temp);
                }
            }
            return ListToppingOrder;
        }

        public async Task<List<ViewOrderDto>> FilterByKey(string key, string status)
        {
            List<ViewOrderDto> list = new();
            if(status == null || status == "")
            {
                list = await GetAllOrder();
            }
            else
            {
                list = await GetAllOrderByStatus(status);
            }

            if(key != null && key != "")
            {
                list = list.FindAll(x => x.Phone.Contains(key));
                return list;
            }
            else
            {
                return list;
            }
        }

        public async Task<string> UpdateStatusOrder(string status, string idOrder)
        {
            Order.Model.Entities.Order.Order order = await _MongoRepository.FindByIdAsync(idOrder);
            if (order != null)
            {
                order.Sate = status;
                await _MongoRepository.UpdateAsync(order);
                return status;
            }
            else
            {
                return "";
            }
        }

        public async Task<List<ViewOrderDto>> FilterByDate(string StoreID, DateTime timeStart, DateTime timeEnd)
        {
            List<ViewOrderDto> result = new();
            List<Order.Model.Entities.Order.Order> orders = (List<Model.Entities.Order.Order>)await _MongoRepository.FindAsync(x => x.CreatedDate >= timeStart && x.CreatedDate <= timeEnd && x.StoreId == StoreID);
            if(orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    ViewOrderDto item = new()
                    {
                        ID = order.Id,
                        NameUser = await _UserRepository.FindByIdAsync(order.UserId) != null ? (await _UserRepository.FindByIdAsync(order.UserId)).Name : "no Name",
                        Address = await _AddressRepository.FindByIdAsync(order.AddressId) != null ? ((await _AddressRepository.FindByIdAsync(order.AddressId)).Addrress) : "no Address",
                        Phone = order.PhoneNumber,
                        TotalPrice = order.Total,
                        DateOrder = order.CreatedDate,
                        State = order.Sate
                    };
                    result.Add(item);
                }
            }
            return result;
        }

        public async Task<NotifyDto> GetNotifyInfo(string storeId)
        {
            var order = await _MongoRepository.FindByIdAsync(storeId);
            if (order == null)
            {
                throw new Exception("Order's not exist");
            }
            string message = "Có đơn hàng mới";
            /*switch (order.Sate)
            {
                case "Đã xác nhận":
                    {
                        break;
                    }
                case "Sẵn sàng":
                    {
                        message = "Đơn hàng đã sẵn sáng để giao/ lấy";
                        break;
                    }
                case "Hoàn tất":
                    {
                        message = "Đơn hàng đã hoàn tất";
                        break;
                    }
                default: message = "Message nay chi de test thoi"; break;
            }*/

            var dto = new NotifyDto()
            {
                UserId = order.UserId,
                Header = "Cập nhật trạng thái đơn hàng",
                Message = message
            };
            return dto;
        }

        public async Task<RevenueDto> Revenue(string storeID, int month, int year)
        {
            DateTime dateStart = new DateTime(year, month, 1);
            DateTime dateEnd = dateStart.AddMonths(1).AddDays(-1);
            List<ViewOrderDto> listOrder = await FilterByDate(storeID, dateStart, dateEnd);
            double revenue = 0;

            if (listOrder != null)
            {
                Func<ViewOrderDto, double?> selector = x => x.TotalPrice;
                revenue = (double)listOrder.Sum(selector);
            }

            RevenueDto revenueDto = new()
            {
                month = month,
                revenue = revenue,
            };

            return revenueDto;
        }

        public async Task<List<RevenueDto>> GetRevenueList(string storeID, int year)
        {
            List<RevenueDto> list = new List<RevenueDto>();
            for(var i = 1; i<=12; i++)
            {
                RevenueDto dto = await Revenue(storeID, i, year);
                list.Add(dto);
            }

            return list;
        }
    }
}
