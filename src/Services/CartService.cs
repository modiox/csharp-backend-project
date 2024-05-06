using Microsoft.EntityFrameworkCore;
public class CartService
{
    private readonly AppDBContext _dbContext;
    private readonly ILogger<UserService> _logger;

    public CartService(AppDBContext dbContext, ILogger<UserService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<CartModel>> GetCartItemsAsync(Guid userId)
    {
        try
        {
            var cartItems = await _dbContext.Carts
                .Where(c => c.UserID == userId)
                .Include(c => c.Product)
                .ToListAsync();

            // Map Cart entities to CartModel
            var cartModels = cartItems.Select(c => new CartModel
            {
                ProductID = c.ProductID,
                UserID = c.UserID,
                Product = new ProductModel
                {
                    ProductID = c.Product.ProductID,
                    ProductName = c.Product.ProductName,
                    Price = c.Product.Price,
                    Quantity = c.Product.Quantity,
                    CategoryID = c.Product.CategoryId
                }
            }).ToList();

            return cartModels;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while performing cart operation.");
            return null;
        }
    }


    public async Task<bool> AddToCartAsync(Guid productId, Guid userId)
    {
        try
        {
            var existingCart = await _dbContext.Carts
                .Where(c => c.ProductID == productId && c.UserID == userId)
                .FirstOrDefaultAsync();

            if (existingCart != null)
            {
                // Product already exists in the user's cart
                return false;
            }

            var newCart = new Cart
            {
                ProductID = productId,
                UserID = userId
            };

            _dbContext.Carts.Add(newCart);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while performing cart operation.");
            return false;
        }
    }

    public async Task<bool> RemoveFromCartAsync(Guid productId, Guid userId)
    {
        try
        {
            var cartItem = await _dbContext.Carts
                .Where(c => c.ProductID == productId && c.UserID == userId)
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                // Product not found in the user's cart
                return false;
            }

            _dbContext.Carts.Remove(cartItem);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while performing cart operation.");
            return false;
        }
    }
}
