using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Attributes;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet] // [Get] BaseUrl/api/Products(Controller name)  << EndPoint
        [Cache(10)] // per days
        [Authorize]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpecParams)
        {
           var result = await _productService.GetAllProductsAsync(productSpecParams);
            return Ok(result); // 200
        }

        [HttpGet("Brands")] // [Get] BaseUrl/api/Products/Brands(Controller name)  << EndPoint
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);
        }

        [HttpGet("Types")] // [Get] BaseUrl/api/Products/Types(Controller name)  << EndPoint
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")] // [Get] BaseUrl/api/Products(Controller name)  << EndPoint
        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null)
            {
                return BadRequest("Invalid Id !");
            }
            var result = await _productService.GetProductById(id.Value);
            if(result is null)
            {
                return NotFound("This product is not found!");
            }
            return Ok(result);

        }



    }
}
