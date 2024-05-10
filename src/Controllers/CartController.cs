using System.Security.Claims;
using api.Controllers;
using api.Middlewares;
using Microsoft.AspNetCore.Mvc;

[Route("api/carts")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCartItems(Guid userId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            throw new UnauthorizedAccessException("User Id is missing from token");
        }
        var cartItems = await _cartService.GetCartItemsAsync(userId);

        if (cartItems != null)
        {
            return ApiResponse.Success(cartItems);
        }
        else
        {
            throw new Exception("Failed to fetch cart items.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId, string userId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
             throw new UnauthorizedAccessException("User Id is missing from token");
        }
        if (!Guid.TryParse(userId, out Guid userIDGuid))
        {
            throw new BadRequestException("Invalid user ID Format");
        }

        if (await _cartService.AddToCartAsync(productId, userIDGuid))
        {
            return ApiResponse.Created("Product added to cart successfully.");
        }
        else
        {
            throw new Exception("Failed to add product to cart.");
        }
    }
}
