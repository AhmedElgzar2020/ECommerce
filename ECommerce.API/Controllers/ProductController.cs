using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryServices;
        public ProductController(IProductService productService, ICategoryService categoryServices)
        {
            _productService = productService;
            _categoryServices = categoryServices;
        }
        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            var categories = _productService.Get();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("GetProduct/{ProductId}")]
        public IActionResult GetProduct(int ProductId)
        {
            if (!_productService.ProductExists(p=>p.Id==ProductId))
            {
                return NotFound();
            }
            var category = _productService.Get(ProductId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("GetProductsOfCategory/{categoryId}")]
        public IActionResult GetProductsOfCategory(int categoryId)
        {
            if (!_categoryServices.CategoryExists(p => p.Id == categoryId))
            {
                return NotFound();
            }
            var categories = _productService.Get(p=>p.CategoryId == categoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
    }
}
