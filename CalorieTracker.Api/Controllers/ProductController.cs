using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository repository,
        IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Get([FromQuery] string serchStr)
    {
        List<Product> products = await _repository.GetAll();

        if (!string.IsNullOrEmpty(serchStr))
        {
            var filteredResult = products.Where(x => x.Name.Contains(serchStr));

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(filteredResult));
        }

        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        Product product = await _repository.GetById(id);

        return Ok(_mapper.Map<ProductDto>(product));
    }

    //
    [HttpPost]
    public async Task<ActionResult> AddFood([FromBody] ProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);

        await _repository.Add(product);

        ProductDto productDTO = _mapper.Map<ProductDto>(product);

        return Ok(_mapper.Map<ProductDto>(productDTO));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteFood(int id)
    {
        Product product = await _repository.GetById(id);

        await _repository.Remove(product);

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditFood([FromBody] ProductDto productDto, int id)
    {
        Product product = await _repository.GetById(id);

        product.Name = productDto.Name;
        product.CaloriePer100g = productDto.CaloriePer100g;
        product.ProteinPer100g = productDto.ProteinPer100g;
        product.FatPer100g = productDto.FatPer100g;
        product.CarbohydratePer100g = productDto.CarbohydratePer100g;

        await _repository.Update(id, product);

        return Ok(_mapper.Map<ProductDto>(product));
    }
}



