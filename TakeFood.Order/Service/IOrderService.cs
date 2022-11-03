using Order.ViewModel.Dtos.Order;

namespace Order.Service
{
    public interface IOrderService
    {
        Task<List<ViewOrderDto>> GetAllOrder();
        Task<List<ViewOrderDto>> GetAllOrderByStatus(string status);
    }
}
