﻿using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository repository,
        IManufacturerRepository manufacturerRepository,
        IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
       _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [Route("cleanup")]
    public async Task<ActionResult> CleanUp()
    {
        List<Product> products = await _repository.GetAll();

        products = products
            .GroupBy(x => x.Name.ToLower())
            .SelectMany(products => products).ToList();

        List<Product> productsUnique = products
            .GroupBy(x => x.Name.ToLower())
            .Select(x => x.First())
            .Distinct().ToList();

        List<int> productsId = products.Select(x => x.Id).ToList();

        List<int> productsUniqueId = productsUnique.Select(x => x.Id).ToList();

        List<int> nonUniqueIds = productsId.Except(productsUniqueId).ToList();

        foreach (var product in products)
        {
            foreach (var id in nonUniqueIds)
            {
                if (product.Id == id)
                {
                    await _repository.Remove(product);
                }              
            }
        }
        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Get([FromQuery] string? serchStr)
    {
        List<Product> products = await _repository.GetAll();

        if (!string.IsNullOrEmpty(serchStr))
        {
            var filteredResult = products.Where(x => x.Name.Contains(serchStr));

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(filteredResult)
                .OrderByDescending(x => x.CaloriePer100g));
        }

        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products)
            .OrderByDescending(x => x.CaloriePer100g));
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

    [HttpPut("{productId}/{manufacturerId}/setManufacturer")]
    public async Task<ActionResult> SetProductManufacturer(int productId, int manufacturerId)
    {
        Product product = await _repository.GetById(productId);

        if (product == null)
        {
            return NotFound();
        }

        Manufacturer manufacturer = await _manufacturerRepository.GetById(manufacturerId);

        if (manufacturer == null)
        {
            return NotFound();
        }

        product.ManufacturerId = manufacturerId;

        await _repository.Update(product.Id, product);

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPut("{productId}/removeManufacturer")]
    public async Task<ActionResult> RemoveProductManufacturer(int productId)
    {
        Product product = await _repository.GetById(productId);

        if (product == null)
        {
            return NotFound();
        }

        product.ManufacturerId = null;

        await _repository.Update(product.Id, product);

        return Ok(_mapper.Map<ProductDto>(product));
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



