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
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ManufacturerController(
        IManufacturerRepository repository,
        IMapper mapper, IProductRepository productRepository)
    {      
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
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

        if (manufacturerDto.ProductsId != null)
        {
            foreach (var productId in manufacturerDto.ProductsId)
            {
                Product product = await _productRepository.GetById(productId);

                if (product == null)
                {
                    return BadRequest("Invalid product ID.");
                }

                manufacturer.Products.Add(product);
            }
        }

        await _repository.Add(manufacturer);

        return Ok(_mapper.Map<ManufacturerDto>(manufacturer));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditManufacturer([FromBody] ManufacturerDto manufacturerDto, int id)
    {
        Manufacturer manufacturer = await _repository.GetById(id);

        manufacturer.Name = manufacturerDto.Name;

        if (manufacturerDto.ProductsId != null)
        {
            foreach (var productId in manufacturerDto.ProductsId)
            {               
                Product product = await _productRepository.GetById(productId);

                if (product == null)
                {
                    return BadRequest("Invalid product ID.");
                }

                manufacturer.Products.Add(product);
            }
        }

        await _repository.Update(id, manufacturer);

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
