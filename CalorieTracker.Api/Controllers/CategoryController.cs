using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using CalorieTracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService)); 
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get([FromQuery] string? searchStr, [FromQuery] string filter = "all")
    {
        var filteredCategory = await _categoryService.GetAllFilteredCategories(searchStr, filter);  
        return Ok(filteredCategory);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        var category = await _categoryService.GetCategoryById(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> AddCategory([FromBody] CategoryDto categoryDto)
    {
       var category = await _categoryService.AddCategory(categoryDto);
        return Ok(category);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditCategory([FromBody] CategoryDto categoryDto, int id)
    {
        var category = await _categoryService.EditCategory(id, categoryDto);
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var category = await _categoryService.DeleteCategory(id);
        return Ok(category);
    }
}
