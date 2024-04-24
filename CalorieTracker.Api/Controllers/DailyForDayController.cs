using CalorieTracker.Api.Models;
using CalorieTracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class DailyForDayController : ControllerBase
{
    private readonly IDailyForDayService _dailyForDayService;

    public DailyForDayController(IDailyForDayService dailyForDayService)
    {
        _dailyForDayService = dailyForDayService ?? throw new ArgumentNullException(nameof(dailyForDayService));
    }

    [HttpGet("{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DailyForDayUserDto>> GetDailyForDayDtoForCertainUser([FromRoute] int userId, [FromQuery] DateTime date)
    {
        var result = await _dailyForDayService.GetDailyForDayDtoForCertainUser(userId, date);
        return Ok(result);
    }

    [HttpDelete("removeBreakfastProduct/{userId}/{breakfastProductId}/{productId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DailyForDayUserDto>> RemoveProductFromBreakfastProductForCertainUser([FromRoute] int userId, [FromRoute] int breakfastProductId, [FromRoute] int productId, [FromQuery] DateTime date)
    {
        var result = await _dailyForDayService.RemoveProductFromBreakfastProductForCertainUser(userId, breakfastProductId, productId, date);
        return Ok(result);
    }

    [HttpDelete("removeLunchProduct/{userId}/{lunchProductId}/{productId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DailyForDayUserDto>> RemoveProductFromLunchProductForCertainUser([FromRoute] int userId, [FromRoute] int lunchProductId, [FromRoute] int productId, [FromQuery] DateTime date)
    {
        var result = await _dailyForDayService.RemoveProductFromLunchProductForCertainUser(userId, lunchProductId, productId, date);
        return Ok(result);
    }

    [HttpDelete("removeDinnerProduct/{userId}/{dinnerProductId}/{productId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DailyForDayUserDto>> RemoveProductFromDinnerProductForCertainUser([FromRoute] int userId, [FromRoute] int dinnerProductId, [FromRoute] int productId, [FromQuery] DateTime date)
    {
        var result = await _dailyForDayService.RemoveProductFromDinnerProductForCertainUser(userId, dinnerProductId, productId, date);
        return Ok(result);
    }
}