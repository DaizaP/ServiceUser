using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceUser.Abstraction;
using ServiceUser.Models;
using ServiceUser.Models.Dto;
using ServiceUser.Services;

namespace ServiceUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        //создаем этим методом начальные роли
        [AllowAnonymous]
        [HttpPost("GetRoles")]
        public IActionResult GetRoles()
        {
            var res = _roleService.GetRoles();
            return Ok(res);
        }
    }
}
