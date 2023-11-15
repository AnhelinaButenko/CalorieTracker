using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult> Get()
    {
        List<Product> product = await _repository.GetAll();

        return Ok(_mapper.Map<IEnumerable<ProductDto>>(product));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        Product product = await _repository.GetById(id);

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult> AddFood([FromBody] ProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);

        await _repository.Add(product);

        ProductDto productDTO = _mapper.Map<ProductDto>(product);

        return Ok(_mapper.Map<ProductDto>(productDTO));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteFood([FromBody] ProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);

        await _repository.Remove(product);

        ProductDto productDTO = _mapper.Map<ProductDto>(product);

        return Ok(_mapper.Map<ProductDto>(productDTO));
    }
}



