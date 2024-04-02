namespace CalorieTracker.Api.Models;

public class CategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<int>? ProductsId { get; set; }

    public List<ProductDto>? Products { get; set; }
}
