using EntityFramework;
using Microsoft.EntityFrameworkCore;

public class OrderService
{
    private AppDBContext _appDbContext;
    public OrderService(AppDBContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Order>> GetAllOrdersService()
    {
        return await _appDbContext.Orders.Include(order => order.Products).ToListAsync();
    }

    public async Task<Order?> GetOrderById(Guid orderId)
    {
        return await _appDbContext.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<Guid> CreateOrderService(OrderModel newOrder)
    {
        // Create record
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            Status = OrderStatus.Pending,
            Payment = newOrder.Payment,
            Amount = newOrder.Amount,
            UserId = newOrder.UserId
        };

        // Add the record to the context
        await _appDbContext.Orders.AddAsync(order);
        // Save to database
        await _appDbContext.SaveChangesAsync();

        return order.OrderId;
    }

    public async Task AddProductToOrder(Guid orderId, Guid productId)
    {
        var order = await _appDbContext.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId);
        var product = await _appDbContext.Products.FindAsync(productId);

        if (order != null && product != null)
        {
            if (product.Quantity == 0)
            {
                throw new InvalidOperationException("This product is unavailable");
            }

            order.Products.Add(product);
            product.Quantity--;
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("This Product has already added to the Order");
        }
    }

    public async Task<bool> UpdateOrderService(Guid orderId, OrderModel updateOrder)
    {
        var existingOrder = _appDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        if (existingOrder != null)
        {
            existingOrder.Status = updateOrder.Status;

            // Add the record to the context
            _appDbContext.Orders.Update(existingOrder);
            // Save to database
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteOrderService(Guid orderId)
    {
        var orderToRemove = _appDbContext.Orders.FirstOrDefault(order => order.OrderId == orderId);
        if (orderToRemove != null)
        {
            // Use context to remove
            _appDbContext.Orders.Remove(orderToRemove);
            // Save to database
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
