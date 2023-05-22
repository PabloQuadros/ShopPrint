using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Services;
using System.Data;

namespace ShopPrint_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColorController : Controller
    {
        public readonly ColorService _colorService;

        public ColorController(ColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreadColor([FromBody] ColorDTO color)
        {
            try
            {
                var colorId = await _colorService.Create(color);
                return Ok(colorId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetColorById([FromRoute] string id)
        {
            try
            {
                var color = await _colorService.GetColorById(id);
                return Ok(color);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllColor()
        {
            try
            {
                var colorList = await _colorService.GetAllColor();
                return Ok(colorList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }


        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateColor([FromBody] ColorDTO color)
        {
            try
            {
                var colorId = await _colorService.UpdateColor(color);
                return Ok(colorId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                return Ok(await _colorService.DeleteColor(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }
    }
}
