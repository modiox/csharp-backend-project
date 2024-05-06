
using EntityFramework;
using Microsoft.EntityFrameworkCore;

public class CustomerOrderService
{
    private AppDBContext _appDbContext;
    public CustomerOrderService(AppDBContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<CustomerOrder>> GetAllOrdersService()
    {
        return await _appDbContext.CustomerOrders.Include(order => order.Products).ToListAsync();
    }

    public async Task<CustomerOrder?> GetOrderById(Guid orderId)
    {
        return await _appDbContext.CustomerOrders.Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<Guid> CreateOrderService(CustomerOrderModel newOrder)
    {
        // Create record
        var order = new CustomerOrder
        {
            OrderId = Guid.NewGuid(),
            Status = OrderStatus.Pending,
            Payment = newOrder.Payment,
            Amount = newOrder.Amount,
            UserId = newOrder.UserId
        };

        // Add the record to the context
        await _appDbContext.CustomerOrders.AddAsync(order);
        // Save to database
        await _appDbContext.SaveChangesAsync();

        return order.OrderId;
    }

    public async Task AddProductToOrder(Guid orderId, Guid productId)
    {
        var order = await _appDbContext.CustomerOrders.Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId);
        var product = await _appDbContext.Products.FindAsync(productId);

        if (order != null && product != null && !order.Products.Contains(product))
        {
            order.Products.Add(product);
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("This Product has already added to the Order");
        }
    }

    public async Task<bool> UpdateOrderService(Guid orderId, CustomerOrderModel updateOrder)
    {
        await Task.CompletedTask;
        var existingOrder = _appDbContext.CustomerOrders.FirstOrDefault(o => o.OrderId == orderId);
        if (existingOrder != null)
        {
            existingOrder.Status = updateOrder.Status;

            // Add the record to the context
            _appDbContext.CustomerOrders.Update(existingOrder);
            // Save to database
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    
    public async Task<bool> DeleteOrderService(Guid orderId)
    {
        await Task.CompletedTask; // Simulate an asynchronous operation without delay
        var orderToRemove = _appDbContext.CustomerOrders.FirstOrDefault(order => order.OrderId == orderId);
        if (orderToRemove != null)
        {
            // Use context to remove
            _appDbContext.CustomerOrders.Remove(orderToRemove);
            // Save to database
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }
}
