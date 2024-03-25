using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceUser.Abstraction;
using ServiceUser.Models.Dto;
using ServiceUser.Services;

namespace ServiceUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserAuthentificateService _auth;
        public LoginController(IUserAuthentificateService roleService)
        {
            _auth = roleService;
        }

        [AllowAnonymous]
        [HttpPost("Authentificate")]
        public IActionResult Login([FromBody] LoginModelDto login)
        {
            var token = _auth.Authentificate(login);
            if (token != null)
            {
                
                return Ok(token);
            }
            else
            {
                return NotFound("Проверьте правильность написания логина и пароля");
            }
        }
    }
}
