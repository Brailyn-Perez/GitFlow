using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.OrderDetails;
using ShopApp.Domain.Models.OrderDetails.OrderDetailsBaseModel;

namespace ShopApp.pressent.Controllers.OrderDetailsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;

        public OderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }
        // GET: api/<OderDetailsController>
        [HttpGet("GetAllOrderDetails")]
        public async Task<IActionResult> Get()
        {
            var result = await _orderDetailsService.GetAllOrderDetailsAsync();

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // GET api/<OderDetailsController>/5
        [HttpGet("GetOrderDetailsById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderDetailsService.GetOrderDetailsByIdAsync(id);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // POST api/<OderDetailsController>
        [HttpPost("CreateOrderDetails")]
        public async Task<IActionResult> Post([FromBody] OrderDetailsModel model)
        {
            var result = await _orderDetailsService.CreateOrderDetailsAsync(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // PUT api/<OderDetailsController>/5
        [HttpPut("UpdateOrderDetails")]
        public async Task<IActionResult> Put([FromBody] OrderDetailsModel model)
        {
            var result = await _orderDetailsService.UpdateOrderDetails(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // DELETE api/<OderDetailsController>/5
        [HttpDelete("DeleteOrderDetails")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderDetailsService.DeleteOrderDetailsByIdAsync(id);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }
    }
}
