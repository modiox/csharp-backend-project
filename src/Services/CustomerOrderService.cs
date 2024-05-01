using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.EntityFramework;
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
            ProductId = row.ProductId,
            UserId = row.UserId,
            Product = _appDbContext.Products
            .Where(p => p.ProductId == row.ProductId)
            .Select(p => new ProductModel
            {
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ShippingPrice = p.ShippingPrice,
                CategoryId = p.CategoryId
            })
            .ToList(),
            User = new UserModel
            {
                UserId = row.User.UserId,
                Name = row.User.Name,
                FirstName = row.User.FirstName,
                LastName = row.User.LastName,
                Email = row.User.Email,
                Password = row.User.Password,
                Address = row.User.Address,
                IsAdmin = row.User.IsAdmin,
                IsBanned = row.User.IsBanned,
                CreatedAt = row.User.CreatedAt,
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

}
