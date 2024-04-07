using CalorieTracker.Api.Models;

namespace CalorieTracker.Service;

public interface IFileService
{
    Task WorkWithFileAsync(List<ImportProductDto> products);
}
