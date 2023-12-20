namespace CalorieTracker.Api.Models;

public class ManufacturerDto
{
    public int Id { get; set; }

    public List<int>? ProductsId { get; set; }

    public string Name { get; set; }
}
