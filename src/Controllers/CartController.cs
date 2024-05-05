using System;
using System.Threading.Tasks;
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
            return Ok(cartItems);
        }
        else
        {
            return BadRequest("Failed to fetch cart items.");
        }
    }


    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(Guid productId, Guid userId)
    {
        if (await _cartService.AddToCartAsync(productId, userId))
        {
            return Ok("Product added to cart successfully.");
        }
        else
        {
            return BadRequest("Failed to add product to cart.");
        }
    }
}
