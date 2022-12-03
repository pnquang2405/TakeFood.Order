using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Order.Model.Entities.Address;
using Order.Model.Entities.Food;
using Order.Model.Entities.Order;
using Order.Model.Entities.Store;
using Order.Model.Entities.Topping;
using Order.Model.Entities.User;
using Order.Model.Entities.Voucher;
using Order.Model.Repository;
using Order.ViewModel.Dtos.Order;
using System.Linq;
using System.Net.WebSockets;
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
        private readonly IMongoRepository<Store> _StoreRepository;
        
        public OrderSerivce(IMongoRepository<Order.Model.Entities.Order.Order> mongoRepository, IMongoRepository<User> mongoUser
            , IMongoRepository<Address> addressRepository, IMongoRepository<FoodOrder> foodOrderRepository, IMongoRepository<Food> foodRepository
            , IMongoRepository<Topping> toppingRepository, IMongoRepository<ToppingOrder> toppingOrderReppsitory, IMongoRepository<Store> storeRepository)
        {
            _MongoRepository = mongoRepository;
            _UserRepository = mongoUser;
            _AddressRepository = addressRepository;
            _FoodOrderRepository = foodOrderRepository;
            _FoodRepository = foodRepository;
            _ToppingRepository = toppingRepository;
            _ToppingOrderRepository = toppingOrderReppsitory;
            _StoreRepository = storeRepository;
        }

        public async Task<List<ViewOrderDto>> GetAllOrder(string storeID)
        {
            List<ViewOrderDto> orderList = new();
            List<Order.Model.Entities.Order.Order> orders = (List<Model.Entities.Order.Order>)await _MongoRepository.FindAsync(x => x.StoreId == storeID);

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

        public async Task<List<ViewOrderDto>> GetAllOrderByStatus(string storeID, string status)
        {
            List<Order.Model.Entities.Order.Order> orders = (List<Order.Model.Entities.Order.Order>)await _MongoRepository.FindAsync(x => x.Sate == status && x.StoreId == storeID);
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

        public async Task<OrderPagingRespone> GetPagingOrder(GetPagingOrderDto dto, string storeID, string status)
        {
            if (status != "Ordered" && status != "Delivering" && status != "Processing" && status != "Delivered") status = "";
            var filter = CreateFilter(dto.StartDate, dto.EndDate, dto.QueryString, storeID, status);
            var sort = CreateSortFilter(dto.SortType, dto.SortBy);
            if (dto.PageNumber <= 0 || dto.PageSize <= 0)
            {
                throw new Exception("Pagenumber or pagesize can not be  zero or negative");
            }
            var rs = await _MongoRepository.GetPagingAsync(filter, dto.PageNumber - 1, dto.PageSize, sort);
            
            var list = new List<ViewOrderDto>();
            foreach (var order in rs.Data)
            {
                list.Add(new ViewOrderDto()
                {
                    ID = order.Id,
                    NameUser = await _UserRepository.FindByIdAsync(order.UserId) != null ? (await _UserRepository.FindByIdAsync(order.UserId)).Name : "no Name",
                    Address = await _AddressRepository.FindByIdAsync(order.AddressId) != null ? ((await _AddressRepository.FindByIdAsync(order.AddressId)).Addrress) : "no Address",
                    Phone = order.PhoneNumber,
                    TotalPrice = order.Total,
                    DateOrder = order.CreatedDate,
                    State = order.Sate
                });
            }

            var info = new OrderPagingRespone()
            {
                Total = rs.Count,
                PageIndex = dto.PageNumber,
                PageSize = dto.PageSize,
                viewOrderDtos = list
            };

            return info;
        }

        private FilterDefinition<Order.Model.Entities.Order.Order> CreateFilter(DateTime? startDate, DateTime? endDate, string query, string storeId, string status)
        {
            var filter = Builders<Order.Model.Entities.Order.Order>.Filter.Eq(x => x.StoreId, storeId);
            if (status != "") filter &= Builders<Order.Model.Entities.Order.Order>.Filter.Eq(x => x.Sate, status);
            if (startDate != null && endDate != null)
            {
                filter &= Builders<Order.Model.Entities.Order.Order>.Filter.Gte(x => x.CreatedDate, startDate);
                filter &= Builders<Order.Model.Entities.Order.Order>.Filter.Lte(x => x.CreatedDate, endDate);
            }
            if(query != null)
            {
                filter &= Builders<Order.Model.Entities.Order.Order>.Filter.Where(x => x.PhoneNumber.Contains(query));
            }

            return filter;
        }

        private SortDefinition<Order.Model.Entities.Order.Order> CreateSortFilter(string sortType, string sortBy)
        {
            if (sortType == "Asc")
            {
                switch (sortBy)
                {
                    case "OrderId": return Builders<Order.Model.Entities.Order.Order>.Sort.Ascending(x => x.Id);
                    case "Total": return Builders<Order.Model.Entities.Order.Order>.Sort.Ascending(x => x.Total);
                    case "CreateDate": return Builders<Order.Model.Entities.Order.Order>.Sort.Ascending(x => x.CreatedDate);
                    default: return Builders<Order.Model.Entities.Order.Order>.Sort.Ascending(x => x.CreatedDate);
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "OrderId": return Builders<Order.Model.Entities.Order.Order>.Sort.Descending(x => x.Id);
                    case "Total": return Builders<Order.Model.Entities.Order.Order>.Sort.Descending(x => x.Total);
                    case "CreateDate": return Builders<Order.Model.Entities.Order.Order>.Sort.Descending(x => x.CreatedDate);
                    default: return Builders<Order.Model.Entities.Order.Order>.Sort.Descending(x => x.CreatedDate);
                }
            }

        }


        public async Task<OrderDetailsDto> GetDetailsOrder(string orderId)
        {
            Order.Model.Entities.Order.Order order = await _MongoRepository.FindByIdAsync(orderId);
            OrderDetailsDto orderDetailsDto = new();
            if(order != null)
            {
                orderDetailsDto.OrderId = order.Id;
                orderDetailsDto.Note = order.Note;
                orderDetailsDto.DateOrder = order.CreatedDate;
                orderDetailsDto.NameUser = await _UserRepository.FindByIdAsync(order.UserId) != null ? (await _UserRepository.FindByIdAsync(order.UserId)).Name : "no Name";
                orderDetailsDto.PaymentMethod = order.PaymentMethod;
                orderDetailsDto.Address = await _AddressRepository.FindByIdAsync(order.AddressId) != null ? (await _AddressRepository.FindByIdAsync(order.AddressId)).Addrress : "No Address";
                orderDetailsDto.Phone = order.PhoneNumber;
                orderDetailsDto.status = order.Sate;
                orderDetailsDto.Discount = order.Discount;
                orderDetailsDto.TotalPrice = order.Total;
                orderDetailsDto.TempTotalPrice = order.Total + order.Discount;
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
                            OriPrice = await _FoodRepository.FindByIdAsync(foodOrder.FoodId) != null ? (await _FoodRepository.FindByIdAsync(foodOrder.FoodId)).Price : 0,
                            Price = await _FoodRepository.FindByIdAsync(foodOrder.FoodId) != null ? (await _FoodRepository.FindByIdAsync(foodOrder.FoodId)).Price* foodOrder.Quantity : 0,
                        };
                        tempItem.ListTopping = new();
                        List<ToppingOrder> toppingOrders = (List<ToppingOrder>)await _ToppingOrderRepository.FindAsync(x => x.FoodOrderId == foodOrder.Id);
                        foreach(ToppingOrder toppingOrder in toppingOrders)
                        {
                            ToppingOrderDto toppingOrderDto = new()
                            {
                                ToppingId = toppingOrder.ToppingId,
                                ToppingName = await _ToppingRepository.FindByIdAsync(toppingOrder.ToppingId) != null ? (await _ToppingRepository.FindByIdAsync(toppingOrder.ToppingId)).Name : "topping thêm", 
                                Price = await _ToppingRepository.FindByIdAsync(toppingOrder.ToppingId) != null ? (await _ToppingRepository.FindByIdAsync(toppingOrder.ToppingId)).Price*toppingOrder.Quantity : 0,
                                Quantity = toppingOrder.Quantity
                            };
                            tempItem.Price += toppingOrderDto.Price;
                            tempItem.ListTopping.Add(toppingOrderDto);
                        }
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

        public async Task<List<ViewOrderDto>> FilterByKey(string storeID, string key, string status)
        {
            List<ViewOrderDto> list = new();
            if(status == null || status == "")
            {
                list = await GetAllOrder(storeID);
            }
            else
            {
                list = await GetAllOrderByStatus(storeID, status);
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

        public async Task<NotifyDto> GetNotifyInfo(string orderID)
        {
            var order = await _MongoRepository.FindByIdAsync(orderID);
            var store = await _StoreRepository.FindByIdAsync(order.StoreId);
            
            if (order == null)
            {
                throw new Exception("Order's not exist");
            }
            string message = orderID;
            
            var dto = new NotifyDto()
            {
                UserId = store.OwnerId,
                Header = "Có đơn hàng mới!!!",
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

        public async Task<FoodSold> GetBestSellingFood(string storeID, int month, int year)
        {
            DateTime dateStart = new DateTime(year, month, 1);
            DateTime dateEnd = dateStart.AddMonths(1).AddDays(-1);
            List<ViewOrderDto> listOrder = await FilterByDate(storeID, dateStart, dateEnd);
            List<FoodSold> foodSolds = new();

            foreach(var order in listOrder)
            {
                List<FoodOrder> foodOrder = (List<FoodOrder>)await _FoodOrderRepository.FindAsync(x => x.OrderId == order.ID);
                foreach(var food in foodOrder)
                {
                    FoodSold temp = new()
                    {
                        FoodID = food.FoodId,
                        quantity = food.Quantity,
                        Name = (await _FoodRepository.FindByIdAsync(food.FoodId)) != null ? (await _FoodRepository.FindByIdAsync(food.FoodId)).Name : "",
                        urlImage = (await _FoodRepository.FindByIdAsync(food.FoodId)) != null ? (await _FoodRepository.FindByIdAsync(food.FoodId)).ImgUrl : ""
                    };
                    if(foodSolds.Any(x => x.FoodID == temp.FoodID)){
                        int index = foodSolds.FindIndex(x => x.FoodID == temp.FoodID);
                        foodSolds[index].quantity += temp.quantity;
                    }
                    else
                    {
                        foodSolds.Add(temp);
                    }
                }
            }

            List<FoodSold> result = foodSolds.OrderBy(x => x.quantity).ToList();

            return result.Last();
        }
    }
}
