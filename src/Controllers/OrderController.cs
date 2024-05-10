using System.Security.Claims;
using api.Controllers;
using api.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("/api/order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var orders = await _orderService.GetAllOrdersService();
            return ApiResponse.Success(orders);
        }


        [Authorize(Roles = "notBanned")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetCOrderById(string orderId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User Id is missing from token");
            }
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                throw new BadRequestException("Invalid user ID Format");
            }

            var order = await _orderService.GetOrderById(orderIdGuid);

            if (order == null)
            {
                throw new NotFoundException("Order Not Found");
            }

            return ApiResponse.Success(order);
        }

        [Authorize(Roles = "notBanned")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderModel newOrder)
        {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    throw new UnauthorizedAccessException("User Id is missing from token");
                }
                await _orderService.CreateOrderService(newOrder);
                return ApiResponse.Created("Order has added successfully!");
        }

        [Authorize(Roles = "notBanned")]
        [HttpPost("{orderId}")]
        public async Task<IActionResult> AddProductToOrder(Guid orderId, Guid productId)
        {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    throw new UnauthorizedAccessException("User Id is missing from token");
                }
                await _orderService.AddProductToOrder(orderId, productId);
                return ApiResponse.Created("Products Added to the order successfully");
        }


        [Authorize(Roles = "notBanned")]
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(string orderId, OrderModel updateOrder)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                 throw new UnauthorizedAccessException("User Id is missing from token");
            }
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                throw new BadRequestException("Invalid user ID Format");
            }
            var result = await _orderService.UpdateOrderService(orderIdGuid, updateOrder);
            if (result)
            {
                return ApiResponse.Updated("Order has updated successfully");
            }
            throw new NotFoundException("Order Not Found");
        }

        [Authorize(Roles = "notBanned")]
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User Id is missing from token");
            }
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                throw new BadRequestException("Invalid user ID Format");
            }
            var result = await _orderService.DeleteOrderService(orderIdGuid);
            if (result)
            {
                return ApiResponse.Deleted("Order has deleted Successfully");
            }
             throw new NotFoundException("Order Not Found");
        }
    }
}