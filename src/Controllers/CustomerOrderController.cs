
using EntityFramework;
// using api.Models;
// using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("/api/customer-order")]
    public class CustomerOrderController : ControllerBase
    {

        private readonly CustomerOrderService _customerOrderService;


        public CustomerOrderController(AppDBContext appDbContext)
        {
            _customerOrderService = new CustomerOrderService(appDbContext);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var orders = await _customerOrderService.GetAllOrdersService();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetCustomerOrderById(string orderId)
        {
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return BadRequest("Invalid customer order ID Format");
            }
            // ! it just the name of service, actually you can chose what ever you want.
            // var order = _customerOrderService.GetCustomerOrderByIdService(orderIdGuid);
            var order = _customerOrderService.GetOrderById(orderIdGuid);

            if (order == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(order);
            }
        }

        [HttpPost]
        public IActionResult CreateOrder(CustomerOrderModel newOrder)
        {
            try
            {
                _customerOrderService.CreateOrderService(newOrder);
                return StatusCode(201, "Order added");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(string orderId, CustomerOrderModel updateOrder)
        {
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return BadRequest("Invalid user ID Format");
            }
            var result = await _customerOrderService.UpdateOrderService(orderIdGuid, updateOrder);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return BadRequest("Invalid user ID Format");
            }
            var result = await _customerOrderService.DeleteOrderService(orderIdGuid);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}

