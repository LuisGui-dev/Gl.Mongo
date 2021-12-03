using System;
using System.Threading.Tasks;
using Gl.Core.Shared.ModelInput.User;
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
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var userLogin = await _manager.ValidPasswordAndGenereteToken(user);
            if (userLogin != null)
            {
                return Ok(userLogin);
            }

            return Unauthorized(new { message = "E-mail ou senha inválido" });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _manager.GetAsync();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _manager.GetAsync(id);
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
                return CreatedAtAction(nameof(Get), new { login = user.Email }, userIn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] NewUser newUser, string id)
        {
            if (id != newUser.Id)
                return BadRequest(new { message = "Usuário não encontrado" });
            var user = await _manager.GetAsync(id);
            if(user is null)
                return BadRequest(new { message = "Usuário não encontrado" });
            await _manager.UpdateAsync(newUser, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _manager.GetAsync(id);
            if (user is null)
                return NotFound(new { message = "Usuário não encontrado" });
            await _manager.DeleteAsync(id);
            return NoContent();
        }
    }
}