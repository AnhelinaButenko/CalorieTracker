using CalorieTracker.Api.Models;

namespace CalorieTracker.Api.Services;

public interface IFileService
{
    Task WorkWithFileAsync(List<ImportProductDto> products);
}
