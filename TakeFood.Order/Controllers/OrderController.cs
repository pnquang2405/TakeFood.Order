using Microsoft.AspNetCore.Mvc;
using Order.Service;
using Order.ViewModel.Dtos.Order;

namespace TakeFood.Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        public readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("GetAllOrder")]
        public async Task<JsonResult> GetAllOrder()
        {
            List<ViewOrderDto> result =await orderService.GetAllOrder();
            try
            {
                return Json(result);
            }catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet("GetAllOrderByStatus")]
        public async Task<JsonResult> GetAllOrder(string status)
        {
            List<ViewOrderDto> result = await orderService.GetAllOrderByStatus(status);
            try
            {
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}
