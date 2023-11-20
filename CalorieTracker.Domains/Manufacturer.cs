namespace CalorieTracker.Domains;
public class Manufacturer : BaseNamedEntity
{
    public List<Product> Products { get; set; } = new List<Product>();

    public int ProductId { get; set; }
    
    public Product Product { get; set; }
}
