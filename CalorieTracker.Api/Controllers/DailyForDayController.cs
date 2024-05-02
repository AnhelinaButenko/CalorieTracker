using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Domains;
using CalorieTracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class DailyForDayController : ControllerBase
{
    private readonly IDailyForDayService _dailyForDayService;
    private readonly IMapper _mapper;

    public DailyForDayController(IDailyForDayService dailyForDayService, IMapper mapper)
    {
        _dailyForDayService = dailyForDayService ?? throw new ArgumentNullException(nameof(dailyForDayService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DailyForDayUserDto>> GetDailyForDayDtoForCertainUser([FromRoute] int userId, [FromQuery] DateTime date)
    {
        var result = await _dailyForDayService.GetDailyForDayDtoForCertainUser(userId, date);
        return Ok(result);
    }

    [HttpDelete("removeMealProduct/{userId}/{mealProductId}/{productId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DailyForDayUserDto>> RemoveProductFromMealProductForCertainUser([FromRoute] int userId, [FromRoute] int mealProductId, [FromRoute] int productId, [FromQuery] DateTime date)
    {
        var result = await _dailyForDayService.RemoveProductFromMealProductForCertainUser( userId, mealProductId, productId, date);
        return Ok(result);
    }

    [HttpPut("updateMealProduct/{userId}/{mealProductId}/{productId}")]
    public async Task<ActionResult> EditProductFromMealProductForCertainUser([FromBody] MealProductDto mealProductDto, [FromRoute] int userId, [FromRoute] int mealProductId, [FromRoute] int productId, [FromQuery] DateTime date)
    {
        MealProduct mealProduct = await _dailyForDayService.EditProductFromMealProductForCertainUser(mealProductDto, userId, mealProductId, productId, date);

        mealProduct.Name = mealProductDto.Name;

        //mealProduct.CaloriePer100g = mealProductDto.CaloriePer100g;

        //await _productService.EditProduct(productDto, id);

        return Ok(_mapper.Map<MealProductDto>(mealProduct));
    }
}