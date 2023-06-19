using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Services;
using System.Data;

namespace ShopPrint_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : Controller
    {
        public readonly CheckoutService _checkoutService;

        public CheckoutController(CheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> CreateCheckout([FromBody] CheckoutDTO checkout)
        {
            try
            {
                var checkoutId = await _checkoutService.Create(checkout);
                return Ok(checkoutId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCheckoutById([FromRoute] string id)
        {
            try
            {
                var checkout = await _checkoutService.GetById(id);
                return Ok(checkout);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAllByUser/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByUser([FromRoute] string id)
        {
            try
            {
                var checkouts = await _checkoutService.GetAllByUser(id);
                return Ok(checkouts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCheckout([FromRoute] string id)
        {
            try
            {
                var result = await _checkoutService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCheckout([FromRoute] string id, [FromBody] CheckoutDTO checkout)
        {
            try
            {
                var result = await _checkoutService.Update(id,checkout);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }
    }
}
