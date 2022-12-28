using Microsoft.AspNetCore.Mvc;
using Order.Service;
using TakeFood.Order.ViewModel.Dtos.Revenue;

namespace TakeFood.Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevenueController : BaseController
    {
        public readonly IOrderService orderService;

        public RevenueController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("RevenueDayToDay")]
        public async Task<JsonResult> Revenue(string storeID, DateTime start, DateTime end, string paymentMethod = "All")
        {
            try
            {
                if(end.Date - start.Date < TimeSpan.FromDays(7))
                {
                    var revenue = await orderService.Revenue(storeID, start, end, paymentMethod);
                    return new JsonResult(revenue);
                }
                else
                {
                    var revenue = await orderService.Revenue1(storeID, start, end, paymentMethod);
                    List<RevenueDto> rs = new List<RevenueDto>();
                    rs.Add(revenue);
                    return new JsonResult(rs);
                }
                
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpGet("Revenue")]
        public async Task<JsonResult> Revenue(string storeID, int month, int year, string paymentMethod = "All")
        {
            try
            {
                RevenueDto revenue = await orderService.Revenue(storeID, month, year, paymentMethod);
                return new JsonResult(revenue);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpGet("RevenueOfYear")]
        public async Task<JsonResult> RevenueOfYear(string storeID, int year, string paymentMethod = "All")
        {
            try
            {
                List<RevenueDto> list = await orderService.GetRevenueList(storeID, year, paymentMethod);

                return new JsonResult(list);
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }

        [HttpGet("RevenueOfSystemYear")]
        public async Task<JsonResult> RevenueOfSystemYear(int year)
        {
            try
            {
                List<RevenueDto> list = await orderService.GetRevenueSystemList(year);

                return new JsonResult(list);
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }
    }
}
