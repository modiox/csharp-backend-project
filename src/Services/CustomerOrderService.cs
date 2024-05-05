
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework;
using Microsoft.EntityFrameworkCore;


public class CustomerOrderService
{
    private AppDBContext _appDbContext;
    public CustomerOrderService(AppDBContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<CustomerOrderModel>> GetAllOrdersService()
    {
        await Task.CompletedTask; // Simulate an asynchronous operation without delay
        var dataList = _appDbContext.CustomerOrders.
        Select(row => new CustomerOrderModel
        {
            OrderId = row.OrderId,
            Status = row.Status,
            Payment = row.Payment,
            Amount = row.Amount,
            ProductId = row.ProductId,
            UserId = row.UserId,
            // ! When we done from the product uncomment me 
            // Product = _appDbContext.Products
            // .Where(p => p.ProductId == row.ProductId)
            // .Select(p => new ProductModel
            // {
            //     ProductName = p.ProductName,
            //     Price = p.Price,
            //     Quantity = p.Quantity,
            //     // ShippingPrice = p.ShippingPrice,
            //     CategoryID = p.CategoryID
            // })
            // .ToList(),
            User = new UserModel
            {
                UserID = row.User.UserID,
                Username = row.User.Username,
                FirstName = row.User.FirstName,
                LastName = row.User.LastName,
                Email = row.User.Email,
                Password = row.User.Password,
                Address = row.User.Address,
                IsAdmin = row.User.IsAdmin,
                IsBanned = row.User.IsBanned,
                // CreatedAt = row.User.CreatedAt,
            }
        }
        ).ToList();
        return dataList.AsEnumerable();
    }


    // TODO : Fix it to return a single order with the user information
    public Task<CustomerOrder?> GetOrderById(Guid orderId)
    {
        return Task.FromResult(_appDbContext.CustomerOrders.Find(orderId));
    }

    public async void CreateOrderService(CustomerOrderModel newOrder)
    {
        await Task.CompletedTask; // Simulate an asynchronous operation without delay

        // Create record
        var order = new CustomerOrder
        {
            OrderId = Guid.NewGuid(),
            Status = OrderStatus.Pending,
            Payment = newOrder.Payment,
            Amount = newOrder.Amount,
            ProductId = newOrder.ProductId,
            UserId = newOrder.UserId
        };

        // Add the record to the context
        _appDbContext.CustomerOrders.Add(order);
        // Save to database
        _appDbContext.SaveChanges();
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
