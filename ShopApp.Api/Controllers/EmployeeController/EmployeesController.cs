using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Employees;
using ShopApp.Domain.Models.Employees;

namespace ShopApp.pressent.Controllers.EmployeeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }
        // GET: api/<EmployeesController>
        [HttpGet("GetEmployees")]
        public async Task<IActionResult> Get()
        {
            var result = await _employeesService.GetAllEmployeesAsync();

            if (!result.IsSucces)
                return BadRequest(result);

             
            return Ok(result);
        }

        // GET api/<EmployeesController>/5
        [HttpGet("GetEmployeesById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeesService.GetEmployeesByIdAsync(id);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // POST api/<EmployeesController>
        [HttpPost("CreateEmployees")]
        public async Task<IActionResult> Post([FromBody] EmployeesCreateModel model)
        {
            var result = await _employeesService.CreateEmployeesAsync(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("UpdateEmployees")]
        public async Task<IActionResult> Put([FromBody] EmployeesUpdateModel model)
        {
            var result = await _employeesService.UpdateEmployees(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("DeleteEmployees")]
        public async Task<IActionResult> Delete(int id, int delete_user)
        {
            var result = await _employeesService.DeleteEmployeesByIdAsync(id,delete_user);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }
    }
}
