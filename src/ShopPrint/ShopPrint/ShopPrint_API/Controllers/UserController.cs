using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Services;

namespace ShopPrint_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        public readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            try
            {
                var userId = await _userService.CreateUser(user);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {
            try
            {
                var token = await _userService.Login(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] string id, [FromBody] string password)
        {
            try
            {
                var result = await _userService.DeleteUser(id,password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            try
            {
                var result = await _userService.GetUserById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            try
            {
                var result = await _userService.UpdateUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }
    }
}
