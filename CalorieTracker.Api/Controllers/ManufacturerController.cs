using CalorieTracker.Api.Models;
using CalorieTracker.Service;
using Microsoft.AspNetCore.Mvc;


namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class ManufacturerController : ControllerBase
{
    private readonly IManufacturerService _manufacturerService;

    public ManufacturerController(IManufacturerService manufacturerService)
    {
        _manufacturerService = manufacturerService ?? throw new ArgumentNullException(nameof(manufacturerService));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<ManufacturerDto>>> Get([FromQuery] string? searchStr, [FromQuery] string filter = "all")
    {
        var manufacturers = await _manufacturerService.GetAllFilteredManufacturers(searchStr, filter);
        return Ok(manufacturers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        var manufacturer = await _manufacturerService.GetManufacturerById(id);
        return Ok(manufacturer);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetByName(string name)
    {
        var manufacturer = await _manufacturerService.GetManufacturerByName(name);
        return Ok(manufacturer);
    }

    [HttpPost]
    public async Task<ActionResult> AddManufacturer([FromBody] ManufacturerDto manufacturerDto)
    {
        var manufacturer = await _manufacturerService.AddManufacturer(manufacturerDto);
        return Ok(manufacturer);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditManufacturer([FromBody] ManufacturerDto manufacturerDto, int id)
    {
        var manufacturer = await _manufacturerService.EditManufacturer(id, manufacturerDto);
        return Ok(manufacturer);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteManufacturer(int id)
    {
        var manufacturer = await _manufacturerService.DeleteManufacturer(id);
        return Ok(manufacturer);
    }
}
