using Order.ViewModel.Dtos.Order;
using TakeFood.Order.ViewModel.Dtos.Order;
using TakeFood.Order.ViewModel.Dtos.Revenue;
using TakeFood.UserOrder.ViewModel.Dtos;

namespace Order.Service
{
    public interface IOrderService
    {
        Task<List<ViewOrderDto>> GetAllOrder(string storeID);
        Task<List<ViewOrderDto>> GetAllOrderByStatus(string storeID, string status);
        Task<string> UpdateStatusOrder(string status, string idOrder);
        Task<OrderDetailsDto> GetDetailsOrder(string orderId);
        Task<List<ToppingOrderDto>> GetToppingsByFoodOrderID(string FoodOrderID);
        Task<List<ViewOrderDto>> FilterByKey(string storeID, string key, string status);
        Task<List<ViewOrderDto>> FilterByDate(string StoreID, DateTime timeStart, DateTime timeEnd);
        Task<NotifyDto> GetNotifyInfo(string storeId);
        Task<RevenueDto> Revenue(string storeID, int month, int year);
        Task<List<RevenueDto>> GetRevenueList(string storeID, int year);
        Task<FoodSold> GetBestSellingFood(string storeID, int month, int year);
    }
}
