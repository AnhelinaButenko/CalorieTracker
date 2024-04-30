using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class MealProductController : ControllerBase
{
    private readonly IMealProductService _mealProductService;
    private readonly IMapper _mapper;

    public MealProductController(IMealProductService mealProductService, IMapper mapper)
    {
        _mealProductService = mealProductService ?? throw new ArgumentNullException(nameof(mealProductService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> GetAll()
    {
        var mealProducts = await _mealProductService.GetAllMealProduct();

        return Ok(_mapper.Map<IEnumerable<MealProductDto>>(mealProducts));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        var mealProduct = await _mealProductService.GetMealProductById(id);

        return Ok(_mapper.Map<MealProductDto>(mealProduct));
    }

    [HttpPost]
    public async Task<ActionResult> AddMealProduct([FromBody] MealProductDto mealProductDto)
    {
        var mealProduct = await _mealProductService.AddMealProduct(mealProductDto);

        return Ok(_mapper.Map<MealProductDto>(mealProduct));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMealProduct(int id)
    {
        var mealProduct = await _mealProductService.DeleteMealProduct(id);

        return Ok(_mapper.Map<MealProductDto>(mealProduct));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditMealProduct([FromBody] MealProductDto mealProductDto, int id)
    {
        var mealProduct = await _mealProductService.EditMealProduct(mealProductDto, id);

        return Ok(_mapper.Map<MealProductDto>(mealProduct));
    }
}