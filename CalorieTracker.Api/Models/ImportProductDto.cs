namespace CalorieTracker.Api.Models;

public class ImportProductDto
{
    public string ProductName { get; set; }

    public CaloryInfoDto CaloryInfo { get; set; }

    public string? ManufacturerName { get; set; }
}
