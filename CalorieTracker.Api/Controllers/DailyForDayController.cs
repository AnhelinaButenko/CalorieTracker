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

    [HttpDelete("removeMealProduct/{userId}/{mealProductId}/{productId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<DailyForDayUserDto>> RemoveProductFromBreakfastProductForCertainUser([FromRoute] int userId, [FromRoute] int mealProductId, [FromRoute] int productId, [FromQuery] DateTime date)
    {
        var result = await _dailyForDayService.RemoveProductFromMealProductForCertainUser( userId, mealProductId, productId, date);
        return Ok(result);
    }
}