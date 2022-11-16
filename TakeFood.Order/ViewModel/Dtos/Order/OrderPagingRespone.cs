using Order.ViewModel.Dtos.Order;

namespace TakeFood.Order.ViewModel.Dtos.Order
{
    public class OrderPagingRespone
    {
        public int Total { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<ViewOrderDto> viewOrderDtos { get; set; }
    }
}  
