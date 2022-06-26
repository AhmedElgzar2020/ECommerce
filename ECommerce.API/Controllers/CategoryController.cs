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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("GetCategory/{CategoryId}")]
        public IActionResult GetCategory(int CategoryId)
        {
            if (!_categoryService.CategoryExists(c=>c.Id==CategoryId))
            {
                return NotFound();
            }
            
            var category = _categoryService.Get(CategoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("GetCategoriesOfCategory/{CategoryId}")]
        public IActionResult GetCategoriesOfCategory(int CategoryId)
        {
            if (!_categoryService.CategoryExists(c => c.Id == CategoryId))
            {
                return NotFound();
            }
            
            var categories = _categoryService.Get(c=>c.ParentId==CategoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
    }
}
