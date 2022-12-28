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
        Task<OrderPagingRespone> GetPagingOrder(GetPagingOrderDto dto, string storeID, string status);
        Task<string> UpdateStatusOrder(string status, string idOrder);
        Task<OrderDetailsDto> GetDetailsOrder(string orderId);
        Task<List<ToppingOrderDto>> GetToppingsByFoodOrderID(string FoodOrderID);
        Task<List<ViewOrderDto>> FilterByKey(string storeID, string key, string status);
        Task<List<ViewOrderDto>> FilterByDate(string StoreID, DateTime timeStart, DateTime timeEnd, string paymentMethod = "All");
        Task<NotifyDto> GetNotifyInfo(string storeId);
        Task<RevenueDto> Revenue(string storeID, int month, int year, string paymentMethod = "All");
        Task<RevenueDto> Revenue1(string storeID, DateTime start, DateTime end, string paymentMethod = "All");
        Task<List<RevenueDto>> Revenue(string storeID, DateTime start, DateTime end, string paymentMethod = "All");
        Task<List<RevenueDto>> GetRevenueList(string storeID, int year, string paymentMethod= "All");
        Task<FoodSold> GetBestSellingFood(string storeID, int month, int year);
        Task<List<RevenueDto>> GetRevenueSystemList(int year);
    }
}
