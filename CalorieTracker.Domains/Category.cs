using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.Domains;

public class Category : BaseNamedEntity
{
    public List<Product>? Products { get; set; } = new List<Product>();
}
 