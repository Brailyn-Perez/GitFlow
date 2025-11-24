using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Suppliers;
using ShopApp.Domain.Models.Suppliers;

namespace ShopApp.pressent.Controllers.SupplierController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISuppliersService _suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            _suppliersService = suppliersService;
        }
        // GET: api/<SuppliersController>
        [HttpGet("GetAllSuppliers")]
        public async Task<IActionResult> Get()
        {
            var result = await _suppliersService.GetAllSupplierAsync();

            if (!result.IsSucces)
                return BadRequest(result);

             
            return Ok(result);
        }

        // GET api/<SuppliersController>/5
        [HttpGet("GetSuppliersById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _suppliersService.GetSupplierByIdAsync(id);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // POST api/<SuppliersController>
        [HttpPost("CreateSuppliers")]
        public async Task<IActionResult> Post([FromBody] SuppliersCreateModel model)
        {
            var result = await _suppliersService.CreateSupplierAsync(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("UpdateSuppliers")]
        public async Task<IActionResult> Put([FromBody] SuppliersUpdateModel model)
        {
            var result = await _suppliersService.UpdateSupplier(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("DeleteSuppliers")]
        public async Task<IActionResult> Delete(int id, int delete_user)
        {
            var result = await _suppliersService.DeleteSupplierByIdAsync(id, delete_user);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }
    }
}
