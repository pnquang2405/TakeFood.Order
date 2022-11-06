using Microsoft.AspNetCore.Mvc;
using Order.Service;

namespace TakeFood.Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevenueController:BaseController
    {
        public readonly IOrderService orderService;

        public RevenueController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("Revenue")]
        public async Task<double> Revenue(string storeID, int month, int year)
        {
            try
            {
                return await orderService.Revenue(storeID, month, year);
            }
            catch
            {
                return 0;
            }
        }
    }
}
