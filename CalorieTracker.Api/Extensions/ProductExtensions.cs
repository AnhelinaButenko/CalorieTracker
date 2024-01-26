using CalorieTracker.Domains;

namespace CalorieTracker.Api.Extensions
{
    public static class ProductExtensions
    {

        public static string? MapManufacturerName(this Product x)
        {
            return x.Manufacturer?.Name;
        }
    }
}
