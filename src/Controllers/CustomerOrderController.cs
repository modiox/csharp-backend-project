using api.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/api/customer-order")]
    public class CustomerOrderController : ControllerBase
    {

        private readonly CustomerOrderService _customerOrderService;

        public CustomerOrderController(AppDbContext appDbContext)
        {
            _customerOrderService = new CustomerOrderService(appDbContext);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var orders = await _customerOrderService.GetAllOrdersService();
            return Ok(orders);
        }
    }
}
