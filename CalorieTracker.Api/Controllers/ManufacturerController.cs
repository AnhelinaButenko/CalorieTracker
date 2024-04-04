using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using CalorieTracker.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    //private readonly IManufacturerRepository _repository;
    //private readonly IProductRepository _productRepository;
    //private readonly IMapper _mapper;

    //public ManufacturerController(
    //    IManufacturerRepository repository,
    //    IMapper mapper, IProductRepository productRepository)
    //{      
    //    _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    //    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    //    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    //}

    //[HttpGet]
    //[ProducesResponseType(200)]
    //[ProducesResponseType(400)]
    //public async Task<ActionResult<IEnumerable<ManufacturerDto>>> Get([FromQuery] string? searchStr, [FromQuery] string filter = "all")
    //{
    //    List<Manufacturer> filteredProducts = await _repository.GetAllFiltered (searchStr, filter);

    //    return Ok(_mapper.Map<IEnumerable<ManufacturerDto>>(filteredProducts));
    //}

    //[HttpGet("{id:int}")]
    //public async Task<ActionResult> GetId(int id)
    //{
    //    Manufacturer manufacturer = await _repository.GetById(id);

    //    List<int> productsIds = manufacturer.Products.Select(p => p.Id).ToList();

    //    ManufacturerDto manufacturerDto = new()
    //    {
    //        Id = manufacturer.Id,
    //        Name = manufacturer.Name,
    //        ProductsId = productsIds
    //    };

    //    return Ok(manufacturerDto);
    //}

    //[HttpPost]
    //public async Task<ActionResult> AddManufacturer([FromBody] ManufacturerDto manufacturerDto)
    //{
    //    Manufacturer manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);

    //    if (manufacturerDto.ProductsId != null)
    //    {
    //        foreach (var productId in manufacturerDto.ProductsId)
    //        {
    //            Product product = await _productRepository.GetById(productId);

    //            if (product == null)
    //            {
    //                return BadRequest("Invalid product ID.");
    //            }

    //            manufacturer.Products.Add(product);
    //        }
    //    }

    //    await _repository.Add(manufacturer);

    //    return Ok(_mapper.Map<ManufacturerDto>(manufacturer));
    //}

    //[HttpPut("{id:int}")]
    //public async Task<ActionResult> EditManufacturer([FromBody] ManufacturerDto manufacturerDto, int id)
    //{
    //    Manufacturer manufacturer = await _repository.GetById(id);

    //    manufacturer.Name = manufacturerDto.Name;

    //    if (manufacturerDto.ProductsId != null)
    //    {
    //        foreach (var productId in manufacturerDto.ProductsId)
    //        {
    //            Product product = await _productRepository.GetById(productId);

    //            if (product == null)
    //            {
    //                return BadRequest("Invalid product ID.");
    //            }

    //            manufacturer.Products.Add(product);
    //        }
    //    }

    //    await _repository.Update(id, manufacturer);

    //    return Ok(_mapper.Map<ManufacturerDto>(manufacturer));
    //}

    //[HttpDelete("{id:int}")]
    //public async Task<ActionResult> DeleteManufacturer(int id)
    //{
    //    Manufacturer manufacturer = await _repository.GetById(id);

    //    await _repository.Remove(manufacturer);

    //    return Ok(_mapper.Map<ManufacturerDto>(manufacturer));
    //}
}
