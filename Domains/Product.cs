namespace Domains
{
    public class Product : BaseEntity
    {
        public int CaloriePer100g { get; set; }
        public int ProteinPer100g { get; set; }
        public int FatPer100g { get; set; }
        public int CarbohydratePer100g { get; set; }
    }
}
