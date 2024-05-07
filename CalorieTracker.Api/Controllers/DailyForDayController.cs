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

    [HttpDelete("removeMealProduct/{userId}/{mealProductId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DailyForDayUserDto>> RemoveProductFromMealProductForCertainUser([FromRoute] int userId, [FromRoute] int mealProductId, [FromQuery] DateTime date)
    {
        var result = await _dailyForDayService.RemoveProductFromMealProductForCertainUser( userId, mealProductId, date);
        return Ok(result);
    }

    [HttpPut("updateMealProduct/{userId}/{mealProductId}")]
    public async Task<ActionResult> UpdateMealProductForCertainUser([FromRoute] int userId, [FromRoute] int mealProductId, [FromBody] MealProductDto updatedMealProduct, [FromQuery] DateTime date)
    {
        DailyForDay dailyForDay = await _dailyForDayService.EditProductFromMealProductForCertainUser(updatedMealProduct, userId, mealProductId, date);
        var result = _mapper.Map<DailyForDayUserDto>(dailyForDay);
        return Ok(result);
    }
}