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
        var cartItems = await _cartService.GetCartItemsAsync(userId);

        if (cartItems != null)
        {
            return ApiResponse.Success(cartItems, "Cart returned");
        }
        else
        {
            return ApiResponse.ServerError("Failed to fetch cart items.");
        }
    }


    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(Guid productId, Guid userId)
    {
        if (await _cartService.AddToCartAsync(productId, userId))
        {
            return ApiResponse.Created("Product added to cart successfully.");
        }
        else
        {
            return ApiResponse.ServerError("Failed to add product to cart.");
        }
    }
}
