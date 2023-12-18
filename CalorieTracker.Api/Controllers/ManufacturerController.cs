using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class ManufacturerController : ControllerBase
{
    private readonly IManufacturerRepository _repository;
    private readonly IMapper _mapper;

    public ManufacturerController(
        IManufacturerRepository repository,
        IMapper mapper)
    {      
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Get([FromQuery] string? serchStr)
    {
        List<Manufacturer> manufacturers = await _repository.GetAll();

        if (!string.IsNullOrEmpty(serchStr))
        {
            var filteredResult = manufacturers.Where(x => x.Name.Contains(serchStr));

            return Ok(_mapper.Map<IEnumerable<ManufacturerDto>>(filteredResult)
                .OrderByDescending(x => x.Name));
        }

        return Ok(_mapper.Map<IEnumerable<ManufacturerDto>>(manufacturers)
            .OrderByDescending(x => x.Name));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        Manufacturer manufacturer = await _repository.GetById(id);

        return Ok(_mapper.Map<ManufacturerDto>(manufacturer));
    }

    [HttpPost]
    public async Task<ActionResult> AddManufacturer([FromBody] ManufacturerDto manufacturerDto)
    {
        Manufacturer manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);

        await _repository.Add(manufacturer);

        return Ok(_mapper.Map<Manufacturer>(manufacturerDto));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditManufacturer([FromBody] ManufacturerDto manufacturerDto, int id)
    {
        Manufacturer manufacturer = await _repository.GetById(id);

        manufacturer.Name = manufacturerDto.Name;

        await _repository.Update(id, manufacturer);

        return Ok(_mapper.Map<ManufacturerDto>(manufacturer));
    }

    [HttpPut("{productId}/{manufacturerId}/setManufacturer")]
    public async Task<ActionResult> SetProductManufacturer(int productId, int manufacturerId)
    {
        Manufacturer manufacturer = await _repository.GetById(manufacturerId);

        if (manufacturer == null)
        {
            return NotFound();
        }

        manufacturer.Id = manufacturerId;

        await _repository.Update(manufacturer.Id, manufacturer);

        return Ok(_mapper.Map<ManufacturerDto>(manufacturer));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteManufacturer(int id)
    {
        Manufacturer manufacturer = await _repository.GetById(id);

        await _repository.Remove(manufacturer);

        return Ok(_mapper.Map<ManufacturerDto>(manufacturer));
    }
}
