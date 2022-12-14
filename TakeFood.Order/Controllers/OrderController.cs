using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Order.Service;
using Order.ViewModel.Dtos.Order;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using TakeFood.Order.ViewModel.Dtos.Order;
using TakeFood.UserOrder.Hubs;

namespace TakeFood.Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        public readonly IOrderService orderService;
        private readonly IHubContext<NotificationHub> notificationUserHubContext;

        public OrderController(IOrderService orderService, IHubContext<NotificationHub> hubContext)
        {
            this.orderService = orderService;
            notificationUserHubContext = hubContext;
        }

        [HttpGet("GetAllOrder")]
        public async Task<JsonResult> GetAllOrder(string storeID)
        {
            List<ViewOrderDto> result = await orderService.GetAllOrder(storeID);
            try
            {
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet("GetAllOrderByStatus")]
        public async Task<JsonResult> GetAllOrder(string storeID, string status)
        {
            List<ViewOrderDto> result = await orderService.GetAllOrderByStatus(storeID, status);
            try
            {
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet("GetPagingOrder")]
        public async Task<JsonResult> GetPagingOrder([FromQuery]GetPagingOrderDto dto, string storeID,[Optional] string status)
        {
            try
            {
                var rs = await orderService.GetPagingOrder(dto, storeID, status);
                return new JsonResult(rs);
            }
            catch(Exception e)
            {
                return new JsonResult(e);
            }
        }

        private async Task NotifyAsync(string orderId)
        {
            using var client = new HttpClient();
            var result = await client.GetAsync("https://takefood-userorderservice.azurewebsites.net/Notify?orderId=" + orderId);
        }

        [HttpPut]
        public async Task<string> UpdateStatus(string status, string idOrder)
        {
            string result = await orderService.UpdateStatusOrder(status, idOrder);
            if (result != null)
            {
                await NotifyAsync(idOrder);
                return result;
            }
            else
            {
                return "can't handle";
            }
        }

        [HttpGet("GetOrderDetails")]
        public async Task<JsonResult> getDetailsOrder(string OrderID)
        {
            try
            {
                OrderDetailsDto orderDetailsDto = await orderService.GetDetailsOrder(OrderID);
                return new JsonResult(orderDetailsDto);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }

        [HttpGet("GetToppingByFoodOrder")]
        public async Task<JsonResult> GetToppingByFoodOrder(string FoodOrderID)
        {
            try
            {
                List<ToppingOrderDto> result = await orderService.GetToppingsByFoodOrderID(FoodOrderID);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }

        [HttpGet("FilterByKey")]
        public async Task<JsonResult> FilterByKey(string storeID, string key, [Optional] string status)
        {
            try
            {
                List<ViewOrderDto> result = await orderService.FilterByKey(storeID, key, status);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }

        [HttpGet]
        [Route("Notify")]
        public async Task<IActionResult> NotifyOrderStateChangeAsync([Required] string orderId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var rs = await orderService.GetNotifyInfo(orderId);
                foreach (var connectionId in NotificationHub._connections.GetConnections(rs.UserId))
                {
                    await notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", rs.Header, rs.Message);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetOrderByDate")]
        public async Task<JsonResult> FilterOrderByDate(string StoreID, DateTime dateStart, DateTime endStart)
        {
            try
            {
                List<ViewOrderDto> viewOrders = await orderService.FilterByDate(StoreID, dateStart, endStart);
                return new JsonResult(viewOrders);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }
    }
}
