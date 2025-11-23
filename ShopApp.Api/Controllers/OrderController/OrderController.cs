using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Order;
using ShopApp.Domain.Models.Order.OrderBaseModel;



namespace ShopApp.pressent.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: api/<OrderController>
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> Get()
        {
            var result = await _orderService.GetAllOrderAsync();

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // GET api/<OrderController>/5
        [HttpGet("GetOrdersById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // POST api/<OrderController>
        [HttpPost("CreateOrders")]
        public async Task<IActionResult> Post([FromBody] OrderModel model)
        {
            var result = await _orderService.CreateOrderAsync(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // PUT api/<OrderController>/5
        [HttpPut("UpdateOrders")]
        public async Task<IActionResult> Put([FromBody] OrderModel model)
        {
            var result = await _orderService.UpdateOrder(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("DeleteOrdersById")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.DeleteOrderByIdAsync(id);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }
    }
}
