using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Customers;
using ShopApp.Domain.Models.Customers;

namespace ShopApp.pressent.Controllers.CustomerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerServie;

        public CustomerController(ICustomerService customerServie)
        {
            _customerServie = customerServie;
        }
        // GET: api/<CustomerController>
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerServie.GetAllCustmersAsync();

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);

        } 

        // GET api/<CustomerController>/5
        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _customerServie.GetCustmersByIdAsync(id);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);

        }

        // POST api/<CustomerController>
        [HttpPost("CreatetCustomer")]
        public async Task<IActionResult> Post([FromBody] CustomersCreateModel model)
        {
            var result = await _customerServie.CreateCustmersAsync(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("UpdateCoustomer")]
        public async Task<IActionResult> Put( [FromBody] CustomersUpdateModel model)
        {
            var result = await _customerServie.UpdateCustmersAsync(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> Delete(int id, int delete_user)
        {
            var result = await _customerServie.DeleteCustmersByIdAsync(id, delete_user);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }
    }
}
