using BlazorShoppingCart.DataAccess.Services.Interfaces;
using BlazorShoppingCart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorShoppingCart.Models.DTO;

namespace BlazorShoppingCart.API.Controllers
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

        [HttpPost]
        [Route("addCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (category is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Category field/s are null" });
            }

            var result = await _categoryService.AddCategory(category);
            if (result is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "An Error Occurred while created Category" });
            }
            return Ok(result);
        }


        [HttpGet]
        [Route("allCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategories()!;
            if (result is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   new Response { Status = "Error", Message = "An Error Occurred while fetching Categories" });
            }
            return Ok(result.ToList());
        }
    }
}
