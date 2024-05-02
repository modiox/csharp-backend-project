using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/api/customerOrders")] //the commen route
    public class CustomerOrderController : ControllerBase
    {

        private readonly CustomerOrderService _customerOrderService;

        public CustomerOrderController()
        {
            _customerOrderService = new CustomerOrderService();
        }

        [HttpGet]
        public IActionResult GetAllCustomerOrders()
        {
            try
            {
                var orders = _customerOrderService.GetAllCustomerOrdersService();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{orderId}")]
        public IActionResult GetCustomerOrderById(string orderId)
        {
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return BadRequest("Invalid customer order ID Format");
            }

            var order = _customerOrderService.GetCustomerOrderByIdService(orderIdGuid);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateCustomerOrder(CustomerOrderModel newOrder)
        {
            var createdOrder = _customerOrderService.CreateCustomerOrderService(newOrder);
            return CreatedAtAction(nameof(GetCustomerOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
        }

        [HttpPut("{orderId}")]
        public IActionResult UpdateCustomerOrder(string orderId, CustomerOrderModel updatedOrder)
        {
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return BadRequest("Invalid order ID Format");
            }

            var order = _customerOrderService.UpdateCustomerOrderService(orderIdGuid, updatedOrder);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpDelete]
        public IActionResult DeleteCustomerOrder(string orderId)
        {
            if (!Guid.TryParse(orderId, out Guid orderIdGuid))
            {
                return BadRequest("Invalid order ID Format");
            }

            var order = _customerOrderService.DeleteCustomerOrderService(orderIdGuid);
            if (!order)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}