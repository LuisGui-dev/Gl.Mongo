using System;
using System.Threading.Tasks;
using Gl.Core.Shared.ModelInput.Customer;
using Gl.Core.Shared.ModelViews.Customer;
using Gl.Manager.Interfaces.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Gl.WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _manager;

        public CustomerController(ICustomerManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _manager.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _manager.GetAsync(id);
            if(customer is null)
                return NotFound(new { message = "Cliente não encontrado" });
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewCustomer newCustomer)
        {
            try
            {
                var customer = await _manager.InsertAsync(newCustomer);
                return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] EditCustomer customerIn, string id)
        {
            if (id != customerIn.Id)
                return BadRequest(new { message = "Cliente não encontrado" });
            var customer = await _manager.GetAsync(id);
            if (customer is null)
                return NotFound(new { message = "Cliente não encontrado" });
            await _manager.UpdateAsync(customerIn, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _manager.GetAsync(id);
            if (customer is null)
                return NotFound(new { message = "Cliente não encontrado" });
            await _manager.DeleteAsync(id);
            return NoContent();
        }
    }
}