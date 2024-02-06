using CalorieTracker.Api.Models;
using CalorieTracker.Api.Services;
using CalorieTracker.Data;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService
            ?? throw new ArgumentNullException(nameof(fileService));
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

            List<ImportProductDto> products = JsonConvert.DeserializeObject<List<ImportProductDto>>(fileContent);

            await _fileService.WorkWithFileAsync(products);
        }

        return Ok();
    }
}
