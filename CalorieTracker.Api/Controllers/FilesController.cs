using CalorieTracker.Api.Models;
using CalorieTracker.Data;
using CalorieTracker.Data.Configuration;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;

namespace CalorieTracker.Api.Controllers;

[Route("api/files")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _hosting;
    private readonly CalorieTrackerDbContext _dbContext;
    private readonly IProductRepository _productRepository;
    private readonly IManufacturerRepository _manufacturerRepository;

    public FilesController(IWebHostEnvironment hosting,
        CalorieTrackerDbContext dbContext, IProductRepository productRepository,
        IManufacturerRepository manufacturerRepository)
    {
        _hosting = hosting
             ?? throw new System.ArgumentException(nameof(hosting));
        _dbContext = dbContext
            ?? throw new System.ArgumentException(nameof(dbContext));
        _productRepository = productRepository
            ?? throw new ArgumentNullException(nameof(productRepository));
        _manufacturerRepository = manufacturerRepository
            ?? throw new ArgumentNullException(nameof(manufacturerRepository));
    }

    [HttpPost]
    public async Task<IActionResult> AddFile(IFormFile uploadedFile)
    {
        if (uploadedFile != null)
        {
            string fileContent = null;

            using (StreamReader reader = new StreamReader(uploadedFile.OpenReadStream()))
            {
                fileContent = reader.ReadToEnd();
            }

            List<ImportProductDto> result = JsonConvert.DeserializeObject<List<ImportProductDto>>(fileContent);

            foreach (ImportProductDto product in result)
            {
                Product productByName = await _productRepository.GetByName(product.ProductName);

                Manufacturer? manufacturerByName = null;

                if (!string.IsNullOrWhiteSpace(product.ManufacturerName))
                {
                    manufacturerByName = await _manufacturerRepository.GetByName(product.ManufacturerName);

                    if (manufacturerByName == null)
                    {
                        manufacturerByName = new()
                        {
                            Name = product.ManufacturerName,
                        };

                        await _manufacturerRepository.Add(manufacturerByName);
                    }
                }

                if (productByName != null)
                {
                    productByName.Name = product.ProductName;
                    productByName.CaloriePer100g = product.CaloryInfo.CaloriePer100g;
                    productByName.ProteinPer100g = product.CaloryInfo.ProteinPer100g;
                    productByName.FatPer100g = product.CaloryInfo.FatPer100g;
                    productByName.CarbohydratePer100g = product.CaloryInfo.CarbohydratePer100g;

                    if (product.ManufacturerName != null)
                    {
                        productByName.Name = product.ManufacturerName;
                    }

                    await _productRepository.Update(productByName.Id, productByName);
                }
                else
                {
                    Product newProduct = new()
                    {
                        Name = product.ProductName,
                        CaloriePer100g = product.CaloryInfo.CaloriePer100g,
                        ProteinPer100g = product.CaloryInfo.ProteinPer100g,
                        FatPer100g = product.CaloryInfo.FatPer100g,
                        CarbohydratePer100g = product.CaloryInfo.CarbohydratePer100g,
                    };

                    if (product.ManufacturerName != null)
                    {
                        newProduct.ManufacturerId = manufacturerByName?.Id;
                    }

                    await _productRepository.Add(newProduct);
                }
            }
        }

        return Ok();
    }
}

public class ImportProductDto
{
    public string ProductName { get; set; }

    public CaloryInfo CaloryInfo { get; set; }

    public string? ManufacturerName { get; set; }
}

public class CaloryInfo
{
    public double CaloriePer100g { get; set; }

    public double ProteinPer100g { get; set; }

    public double FatPer100g { get; set; }

    public double CarbohydratePer100g { get; set; }
}