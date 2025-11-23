using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Products;
using ShopApp.Domain.Models.Products;

namespace ShopApp.pressent.Controllers.ProductsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        // GET: api/<ProductsController>
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> Get()
        {
            var result = await _productsService.GetAllProductsAsync();

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // GET api/<ProductsController>/5
        [HttpGet("GetProductsById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productsService.GetProductsByIdAsync(id);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // POST api/<ProductsController>
        [HttpPost("CreateProducts")]
        public async Task<IActionResult> Post([FromBody] ProductsCreateModel model)
        {
            var result = await _productsService.CreateProductsAsync(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("UpdateProducts")]
        public async Task<IActionResult> Put([FromBody] ProductsUpdateModel model)
        {
            var result = await _productsService.UpdateProducts(model);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("DeleteProducts")]
        public async Task<IActionResult> Delete(int id, int delete_user)
        {
            var result = await _productsService.DeleteProductsByIdAsync(id, delete_user);

            if (!result.IsSucces)
                return BadRequest(result);


            return Ok(result);
        }
    }
}
