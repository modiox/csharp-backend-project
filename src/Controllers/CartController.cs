using System.Security.Claims;
using api.Controllers;
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
            return ApiResponse.UnAuthorized("User Id is missing from token");
        }
        var cartItems = await _cartService.GetCartItemsAsync(userId);

        if (cartItems != null)
        {
            return ApiResponse.Success(cartItems);
        }
        else
        {
            return ApiResponse.ServerError("Failed to fetch cart items.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId, string userId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            return ApiResponse.UnAuthorized("User Id is missing from token");
        }
        if (!Guid.TryParse(userId, out Guid userIDGuid))
        {
            return ApiResponse.BadRequest("Invalid user ID Format");
        }

        if (await _cartService.AddToCartAsync(productId, userIDGuid))
        {
            return ApiResponse.Created("Product added to cart successfully.");
        }
        else
        {
            return ApiResponse.ServerError("Failed to add product to cart.");
        }
    }
}
