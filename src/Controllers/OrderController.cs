using System.Security.Claims;
using api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("/api/customer-order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _OrderService;

        public OrderController(AppDBContext appDbContext)
        {
            _OrderService = new OrderService(appDbContext);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var orders = await _OrderService.GetAllOrdersService();
            return ApiResponse.Success(orders);
        }

        [Authorize(Roles = "Banned")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetCOrderById(string orderId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return ApiResponse.UnAuthorized("User Id is missing from token");
            }
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return ApiResponse.BadRequest("Invalid user ID Format");
            }

            var order = await _OrderService.GetOrderById(orderIdGuid);

            if (order == null)
            {
                return ApiResponse.NotFound();
            }

            return ApiResponse.Success(order);
        }

        [Authorize(Roles = "Banned")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderModel newOrder)
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    return ApiResponse.UnAuthorized("User Id is missing from token");
                }
                await _OrderService.CreateOrderService(newOrder);
                return ApiResponse.Created("Order has added successfully!");
            }
            catch (Exception e)
            {
                return ApiResponse.ServerError(e.Message);
            }
        }

        [Authorize(Roles = "Banned")]
        [HttpPost("{orderId}")]
        public async Task<IActionResult> AddProductToOrder(Guid orderId, Guid productId)
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    return ApiResponse.UnAuthorized("User Id is missing from token");
                }
                await _OrderService.AddProductToOrder(orderId, productId);
                return ApiResponse.Created("Products Added to the order successfully");
            }
            catch (Exception e)
            {
                return ApiResponse.ServerError(e.Message);
            }
        }

        [Authorize(Roles = "Banned")]
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(string orderId, OrderModel updateOrder)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return ApiResponse.UnAuthorized("User Id is missing from token");
            }
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return ApiResponse.BadRequest("Invalid user ID Format");
            }
            var result = await _OrderService.UpdateOrderService(orderIdGuid, updateOrder);
            if (result)
            {
                return ApiResponse.Updated("Order has updated successfully");
            }
            return ApiResponse.NotFound();
        }

        [Authorize(Roles = "Banned")]
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return ApiResponse.UnAuthorized("User Id is missing from token");
            }
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return ApiResponse.BadRequest("Invalid user ID Format");
            }
            var result = await _OrderService.DeleteOrderService(orderIdGuid);
            if (result)
            {
                return ApiResponse.Deleted("Order has deleted Successfully");
            }
            return ApiResponse.NotFound("Order not Found");
        }
    }
}