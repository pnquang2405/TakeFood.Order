using Order.ViewModel.Dtos.Order;
using TakeFood.Order.ViewModel.Dtos.Order;
using TakeFood.UserOrder.ViewModel.Dtos;

namespace Order.Service
{
    public interface IOrderService
    {
        Task<List<ViewOrderDto>> GetAllOrder();
        Task<List<ViewOrderDto>> GetAllOrderByStatus(string status);
        Task<string> UpdateStatusOrder(string status, string idOrder);
        Task<OrderDetailsDto> GetDetailsOrder(string orderId);
        Task<List<ToppingOrderDto>> GetToppingsByFoodOrderID(string FoodOrderID);
        Task<List<ViewOrderDto>> FilterByKey(string key, string status);
        /*Task<List<ViewOrderDto>> FilterByDate(DateTime timeStart, DateTime timeEnd);*/
        Task<NotifyDto> GetNotifyInfo(string storeId);
    }
}
