using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Services;
using System.Data;

namespace ShopPrint_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        public readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }


        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> CreateCart([FromBody] CartDTO cart)
        {
            try
            {
                var cartId = await _cartService.CreateCart(cart);
                return Ok(cartId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut]
        [Route("AddItem/{userId}")]
        [Authorize]
        public async Task<IActionResult> AddItem([FromRoute] string userId, [FromBody] string productId)
        {
            try
            {
                return Ok(await _cartService.AddItem(userId, productId));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut]
        [Route("RemoveItem/{userId}")]
        [Authorize]
        public async Task<IActionResult> RemoveItem([FromRoute] string userId, [FromBody] string productId)
        {
            try
            {
                return Ok(await _cartService.RemoveItem(userId, productId));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut]
        [Route("RemoveProduct/{userId}")]
        [Authorize]
        public async Task<IActionResult> RemoveProduct([FromRoute] string userId, [FromBody] string productId)
        {
            try
            {
                return Ok(await _cartService.RemoveProductOfCart(userId, productId));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetByUserId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetCartByUserId([FromRoute] string id)
        {
            try
            {
                var cart = await _cartService.GetCartByUserId(id);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }
    }
}
