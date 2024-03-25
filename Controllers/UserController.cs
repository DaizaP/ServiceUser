using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceUser.Abstraction;
using ServiceUser.Models.Dto;
using ServiceUser.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ServiceUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] AddUserModelDto userModel)
        {
            try
            {
                var res = _userService.AddUser(userModel);
                if (res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Ошибка при добавлении пользователя");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("DelUser")]
        public IActionResult DelUser([FromBody] string userEmail)
        {
            try
            {
                var userid = HttpContext.User.Claims.Where(x => x.Type == "UserId").Select(c => c.Value).SingleOrDefault();
                var res = _userService.DeleteUser(userEmail, new Guid(userid));
                if (res)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Пользователя не существует или администратор пытается удалить сам себя");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var res = _userService.GetUsers();
                if (res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetUserId")]
        public IActionResult GetUserId()
        {
            var userid = HttpContext.User.Claims.Where(x => x.Type == "UserId").Select(c => c.Value).SingleOrDefault();
            var userRole = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
            return Ok(userid + $" Role = {userRole}");
        }
    }
}
