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

namespace CalorieTracker.Api.Controllers;

[Route("api/files")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _hosting;
    private readonly CalorieTrackerDbContext _dbContext;

    public FilesController(IWebHostEnvironment hosting,
        CalorieTrackerDbContext dbContext)
    {
        _hosting = hosting
             ?? throw new System.ArgumentException(nameof(hosting));
        _dbContext = dbContext 
            ?? throw new System.ArgumentException(nameof(dbContext));
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
        }

        _dbContext.SaveChanges();

        return Ok();
    }
}


public class ImportProductDto
{
    public string ProductName { get; set; }
    
    public CaloryInfo CaloryInfo { get; set; }

    public string ManufacturerName { get; set; }
}

public class CaloryInfo
{
    public double CaloriePer100g { get; set; }

    public double ProteinPer100g { get; set;}

    public double FatPer100g { get; set;}

    public double CarbohydratePer100g { get; set;}
}