using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Services;

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
        [Route("AddItem")]
        [Authorize]
        public async Task<IActionResult> AddItem(ModifyCart modifyCart)
        {
            try
            {
                return Ok(await _cartService.AddItem(modifyCart));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut]
        [Route("RemoveItem")]
        [Authorize]
        public async Task<IActionResult> RemoveItem(ModifyCart modifyCart)
        {
            try
            {
                return Ok(await _cartService.RemoveItem(modifyCart));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut]
        [Route("RemoveProduct")]
        [Authorize]
        public async Task<IActionResult> RemoveProduct(ModifyCart modifyCart)
        {
            try
            {
                return Ok(await _cartService.RemoveProductOfCart(modifyCart));
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
