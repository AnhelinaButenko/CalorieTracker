using System.ComponentModel.DataAnnotations.Schema;

namespace CalorieTracker.Domains;
public class Manufacturer : BaseNamedEntity
{
    public List<Product>? Products { get; set; } = new List<Product>();
}
