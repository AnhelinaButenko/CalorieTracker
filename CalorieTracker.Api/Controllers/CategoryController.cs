using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository)); 
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get([FromQuery] string? searchStr, [FromQuery] string filter = "all")
    {
        List<Category> filteredCategory = await _categoryRepository.GetAllFiltered(searchStr, filter);  

        return Ok(_mapper.Map<IEnumerable<CategoryDto>>(filteredCategory));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        Category category = await _categoryRepository.GetById(id);

        List<int> categoriesIds = category.Products.Select(p => p.Id).ToList();

        CategoryDto categoryDto = new()
        {
            Id = category.Id,
            Name =  category.Name,
            ProductsId = categoriesIds
        };

        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<ActionResult> AddCategory([FromBody] CategoryDto categoryDto)
    {
        Category category = _mapper.Map<Category>(categoryDto);

        if (categoryDto.ProductsId != null)
        {
            foreach (var productId in categoryDto.ProductsId)
            {
                Product product = await _productRepository.GetById(productId);

                if (product == null)
                {
                    return BadRequest("Invalid product ID.");
                }

                category.Products.Add(product);
            }
        }

        await _categoryRepository.Add(category);

        return Ok(_mapper.Map<ManufacturerDto>(category));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditCategory([FromBody] CategoryDto categoryDto, int id)
    {
        Category category = await _categoryRepository.GetById(id);

        category.Name = categoryDto.Name;

        if (categoryDto.ProductsId != null)
        {
            foreach (var productId in categoryDto.ProductsId)
            {
                Product product = await _productRepository.GetById(productId);

                if (product == null)
                {
                    return BadRequest("Invalid product ID.");
                }

                category.Products.Add(product);
            }
        }

        await _categoryRepository.Update(id, category);

        return Ok(_mapper.Map<CategoryDto>(category));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        Category category = await _categoryRepository.GetById(id);

        await _categoryRepository.Remove(category);

        return Ok(_mapper.Map<CategoryDto>(category));
    }

}
