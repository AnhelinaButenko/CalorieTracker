namespace CalorieTracker.Api.Models;

public class ProductDto
{
    public int Id { get; set; }

    public int? ManufacturerId { get; set; }

    public string Name { get; set; }

    public double CaloriePer100g { get; set; }

    public double ProteinPer100g { get; set; }

    public double FatPer100g { get; set; }

    public double CarbohydratePer100g { get; set; }
}
