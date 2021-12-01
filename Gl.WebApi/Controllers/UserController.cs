using System;
using System.Threading.Tasks;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelViews.User;
using Gl.Manager.Interfaces.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Gl.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _manager;

        public UserController(IUserManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var userLogin = await _manager.ValidPasswordAndGenereteToken(user);
            if (userLogin != null)
            {
                return Ok(userLogin);
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _manager.GetAsync();
            return Ok(user);
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> Get(string login)
        {
            var user = await _manager.GetAsync(login);
            if (user is null)
                return NotFound(new { message = "Usuário não encontrado" });
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewUser user)
        {
            try
            {
                var userIn = await _manager.InsertAsync(user);
                return CreatedAtAction(nameof(Get), new { login = user.Login }, userIn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}