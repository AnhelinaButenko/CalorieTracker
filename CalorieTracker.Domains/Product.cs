namespace CalorieTracker.Domains;

public class Product : BaseNamedEntity
{
    public double CaloriePer100g { get; set; }

    public double ProteinPer100g { get; set; }

    public double FatPer100g { get; set; }

    public double CarbohydratePer100g { get; set; }

    public MealProduct MealProduct { get; set; }

    public int? ManufacturerId { get; set; }

    public Manufacturer Manufacturer { get; set; }

    public int? CategoryId { get; set; }

    public Category Category { get; set; }
}
