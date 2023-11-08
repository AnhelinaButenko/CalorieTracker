namespace CalorieTracker.Domains
{
    public class Product : BaseNamedEntity
    {
        public double CaloriePer100g { get; set; }

        public double ProteinPer100g { get; set; }

        public double FatPer100g { get; set; }

        public double CarbohydratePer100g { get; set; }
    }
}
