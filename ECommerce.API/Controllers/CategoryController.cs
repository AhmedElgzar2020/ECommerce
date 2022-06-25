using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetProducts")]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.Get();
            return Ok(categories);
        }

        [HttpGet("GetCategory/{CategoryId}")]
        public IActionResult GetCategory(int CategoryId)
        {
            var category = _categoryService.Get(CategoryId);
            return Ok(category);
        }

        [HttpGet("GetCategoriesOfCategory/{CategoryId}")]
        public IActionResult GetCategoriesOfCategory(int CategoryId)
        {
            var categories = _categoryService.Get(c=>c.ParentId==CategoryId);
            return Ok(categories);
        }
    }
}
