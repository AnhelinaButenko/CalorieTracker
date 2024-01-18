using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
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
    public async Task<ActionResult<IEnumerable<ManufacturerDto>>> Get([FromQuery] string? searchStr, [FromQuery] string filter = "all")
    {
        List<Manufacturer> manufacturersList = await _repository.GetAll();

        IQueryable<Manufacturer> manufacturersQuery = manufacturersList.AsQueryable();
        IEnumerable<Manufacturer> manufacturers;

        if (filter == "withProducts")
        {
            manufacturers = manufacturersQuery.Include(m => m.Products).Where(m => m.Products.Any()).ToList();
        }
        else if (filter == "withoutProducts")
        {
            manufacturers = manufacturersQuery.Include(m => m.Products).Where(m => m.Products == null || !m.Products.Any()).ToList();
        }
        else
        {
            manufacturers = manufacturersQuery.ToList();
        }

        if (!string.IsNullOrEmpty(searchStr))
        {
            manufacturers = manufacturers.Where(m => m.Name.Contains(searchStr, StringComparison.OrdinalIgnoreCase));
        }

        List<ManufacturerDto> manufacturerDtos = manufacturers
                 .Select(manufacturer => new ManufacturerDto
                 {
                     Id = manufacturer.Id,
                     Name = manufacturer.Name,
                     Products = manufacturer.Products?.Select(p => new ProductDto
                     {
                         Id = p.Id,
                         Name = p.Name,
                         CaloriePer100g = p.CaloriePer100g,
                         ProteinPer100g = p.ProteinPer100g,
                         FatPer100g = p.FatPer100g,
                         CarbohydratePer100g = p.CarbohydratePer100g
                     }).ToList() ?? new List<ProductDto>()
                 })
                 .OrderByDescending(x => x.Name)
                 .ToList();

        return Ok(manufacturerDtos);
    }

    //public async Task<ActionResult> Get([FromQuery] string? serchStr)
    //{
    //    List<Manufacturer> manufacturers = await _repository.GetAll();

    //    if (!string.IsNullOrEmpty(serchStr))
    //    {
    //        var filteredResult = manufacturers.Where(x => x.Name.Contains(serchStr));

    //        return Ok(_mapper.Map<IEnumerable<ManufacturerDto>>(filteredResult)
    //            .OrderByDescending(x => x.Name));
    //    }

    //    return Ok(_mapper.Map<IEnumerable<ManufacturerDto>>(manufacturers)
    //        .OrderByDescending(x => x.Name));
    //}

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        Manufacturer manufacturer = await _repository.GetById(id);

        List<int> productsIds = manufacturer.Products.Select(p => p.Id).ToList();

        ManufacturerDto manufacturerDto = new()
        {
            Id = manufacturer.Id,
            Name = manufacturer.Name,
            ProductsId = productsIds
        };

        return Ok(manufacturerDto);
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

        //List<Product> productsId = manufacturer.Products.Where(p => manufacturerDto.ProductsId.Contains(p.Id)).ToList();

        //foreach (var productId in productsId)
        //{
        //    manufacturer.Products.Add(productId);
        //}


        //var productsToRemove = manufacturer.Products.Where(p => !manufacturerDto.ProductsId.Contains(p.Id)).ToList();

        //foreach (var productToRemove in productsToRemove)
        //{
        //    manufacturer.Products.Remove(productToRemove);
        //}

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
