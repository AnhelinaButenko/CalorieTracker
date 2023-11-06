namespace CalorieTracker.Domains
{
    public class DailyFoodDairy : ReportEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public List<BreakfastProduct> BreakfastProducts { get; set; } = new List<BreakfastProduct>();

        public List<LunchProduct> LunchProducts { get; set; } = new List<LunchProduct>();

        public List<DinnerProduct> DinnerProducts { get; set; } = new List<DinnerProduct>();
    }
}
