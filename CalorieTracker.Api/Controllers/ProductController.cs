using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using CalorieTracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IManufacturerService _manufacturerService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IManufacturerService manufacturerService, IMapper mapper)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _manufacturerService = manufacturerService ?? throw new ArgumentNullException(nameof(manufacturerService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery] string? searchStr, [FromQuery] string filter = "all")
    {
        var products = await _productService.GetAllFilteredProducts(searchStr, filter);
        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpPut("{productId}/{manufacturerId}/setManufacturer")]
    public async Task<ActionResult> SetProductManufacturer(int productId, int manufacturerId)
    {
        Product product = await _productService.GetProductById(productId);

        if (product == null)
        {
            return NotFound();
        }

        ManufacturerDto manufacturer = await _manufacturerService.GetManufacturerById(manufacturerId);

        if (manufacturer == null)
        {
            return NotFound();
        }

        product.ManufacturerId = manufacturerId;

        await _productService.EditProduct(_mapper.Map<ProductDto>(product), productId);

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPut("{manufacturerName}/{productId}/setManufacturerByName")]
    public async Task<ActionResult> SetProductManufacturerByName(string manufacturerName, int productId)
    {
        Product product = await _productService.GetProductById(productId);

        if (product == null)
        {
            return NotFound();
        }

        ManufacturerDto manufacturer = await _manufacturerService.GetManufacturerByName(manufacturerName);

        if (manufacturer == null)
        {
            ManufacturerDto newManufacturerDto = new ManufacturerDto
            {
                Name = manufacturerName
            };

            await _manufacturerService.AddManufacturer(newManufacturerDto);

            manufacturer = await _manufacturerService.GetManufacturerByName(manufacturerName);

            if (manufacturer == null)
            {
                return NotFound();
            }
        }

        product.ManufacturerId = manufacturer.Id;

        await _productService.EditProduct(_mapper.Map<ProductDto>(product), productId);

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPut("{productId}/removeManufacturer")]
    public async Task<ActionResult> RemoveProductManufacturer(int productId)
    {
        Product product = await _productService.GetProductById(productId);

        if (product == null)
        {
            return NotFound();
        }

        product.ManufacturerId = null;

        await _productService.EditProduct(_mapper.Map<ProductDto>(product), productId);

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteFood(int id)
    {
        Product product = await _productService.GetProductById(id);

        await _productService.DeleteProduct(id);

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditFood([FromBody] ProductDto productDto, int id)
    {
        Product product = await _productService.GetProductById(id);

        product.Name = productDto.Name;
        product.CaloriePer100g = productDto.CaloriePer100g;
        product.ProteinPer100g = productDto.ProteinPer100g;
        product.FatPer100g = productDto.FatPer100g;
        product.CarbohydratePer100g = productDto.CarbohydratePer100g;

        await _productService.EditProduct(productDto, id);

        return Ok(_mapper.Map<ProductDto>(product));
    }
}




