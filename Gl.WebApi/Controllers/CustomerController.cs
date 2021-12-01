using System;
using System.Threading.Tasks;
using Gl.Core.Shared.ModelViews.Customer;
using Gl.Manager.Interfaces.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Gl.WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _customerManager.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _customerManager.GetAsync(id);
            if(customer is null)
                return NotFound(new { message = "Cliente não encontrado" });
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewCustomer newCustomer)
        {
            try
            {
                var customerIn = await _customerManager.InsertAsync(newCustomer);
                return CreatedAtAction(nameof(Get), new { id = customerIn.Id }, customerIn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] EditCustomer customer, string id)
        {
            if (id != customer.Id)
                return BadRequest(new { message = "Cliente não encontrado" });
            var customerIn = await _customerManager.GetAsync(id);
            if (customerIn is null)
                return NotFound(new { message = "Cliente não encontrado" });
            await _customerManager.UpdateAsync(customer, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customerIn = await _customerManager.GetAsync(id);
            if (customerIn is null)
                return NotFound(new { message = "Cliente não encontrado" });
            await _customerManager.DeleteAsync(id);
            return NoContent();
        }
    }
}