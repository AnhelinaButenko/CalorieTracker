namespace Domains
{
    public class DailyFoodDairy : ReportEntity
    {
        public List<BreakfastProducts> BreakfastProducts { get; set; } = new List<BreakfastProducts>();
        public List<LunchProducts> LunchProducts { get; set; } = new List<LunchProducts>();
        public List<DinnerProducts> DinnerProducts { get; set; } = new List<DinnerProducts>();
    }
}
