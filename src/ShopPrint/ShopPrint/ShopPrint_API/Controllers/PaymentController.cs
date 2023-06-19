using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;
using ShopPrint_API.Services;

namespace ShopPrint_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        public readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("Pay/{checkoutId}")]
        [Authorize]
        public async Task<IActionResult> Pay([FromRoute] string checkoutId, [FromBody] PaymentMetodDTO paymentMetod)
        {
            try
            {
                if(paymentMetod.pix != null)
                {
                    var paymentId = await _paymentService.Pay(checkoutId, paymentMetod.pix, null);
                    return Ok(paymentId);
                }
                else
                {
                    var paymentId = await _paymentService.Pay(checkoutId, null,paymentMetod.bankSlip);
                    return Ok(paymentId);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("FinalizePayment/{paymentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> FinalizePayment([FromRoute] string paymentId)
        {
            try
            {
                var checkout = await _paymentService.finalizePayment(paymentId);
                return Ok(checkout);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaymentById([FromRoute] string id)
        {
            try
            {
                var payment = await _paymentService.GetById(id);
                return Ok(payment);
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
                var payments = await _paymentService.GetAllByUser(id);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }
    }
}
