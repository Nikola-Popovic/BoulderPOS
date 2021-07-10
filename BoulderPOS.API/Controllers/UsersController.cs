using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BoulderPOS.API.Configuration;
using BoulderPOS.API.Models;
using BoulderPOS.API.Models.DTO;
using BoulderPOS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BoulderPOS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<AuthSettings> _authOptions;

        public UsersController(IUserService userService, IOptions<AuthSettings> authOptions)
        {
            _userService = userService;
            _authOptions = authOptions;
        }

        [HttpPost]
        [Produces(typeof(User))]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(LoginInfoRequest request)
        {
            var user = await _userService.Authenticate(request.UserName, request.Password);
            
            if (user == null)
            {
                return BadRequest("Username or password is wrong");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authOptions.Value.Secret);

            var canAdmin = user.Roles.Any(x => x.RoleId == Roles.AdminRole.Id);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                CanAdmin = canAdmin,
                Token = tokenString
            });
        }


        [HttpPost]
        [Produces(typeof(User))]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(LoginInfoRequest request)
        {
            await _userService.CreateUser(request);
            return Ok();
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("{id}")]
        [Produces(typeof(User))]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddUserRole(AddRoleRequest request)
        {
            await _userService.AddUserRole(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
