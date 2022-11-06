using Microsoft.AspNetCore.Mvc;
using Order.Service;
using TakeFood.Order.ViewModel.Dtos.Revenue;

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
        public async Task<JsonResult> Revenue(string storeID, int month, int year)
        {
            try
            {
                RevenueDto revenue = await orderService.Revenue(storeID, month, year);
                return new JsonResult(revenue);
            }
            catch(Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpGet("RevenueOfYear")]
        public async Task<JsonResult> RevenueOfYear(string storeID, int year)
        {
            try
            {
                List<RevenueDto> list = await orderService.GetRevenueList(storeID, year);

                return new JsonResult(list);
            }catch(Exception e)
            {
                return new JsonResult(e);
            }
        }

        [HttpGet("BestSellingFood")]
        public async Task<JsonResult> GetBestFoodSoled(string storeID, int month, int year)
        {
            try
            {
                FoodSold foodSold = await orderService.GetBestSellingFood(storeID, month, year);
                return new JsonResult(foodSold);
            }
            catch(Exception e)
            {
                return new JsonResult(e.Message);
            }
        }
    }
}
