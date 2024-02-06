using CalorieTracker.Api.Seeder;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly IDataSeeder _seeder;

    public SeedController(IDataSeeder seeder)
    {
        _seeder = seeder;
    }

    [HttpPost]
    public async Task<IActionResult> Seed([FromQuery] bool recreateDb = false)
    {
         await _seeder.Seed(recreateDb);  

         return Ok();
    }
}
