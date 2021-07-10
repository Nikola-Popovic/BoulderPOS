using System.Collections.Generic;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using BoulderPOS.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoulderPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<Role>))]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _roleService.GetRoles());
        }

        [HttpGet("{id}")]
        [Produces(typeof(Role))]
        public async Task<IActionResult> GetRole(int id)
        {
            return Ok(await _roleService.GetRoleBydId(id));
        }
    }
}
