﻿namespace CalorieTracker.Domains;

public class DailyForDay : BaseNamedEntity
{
    public List<BreakfastProduct> BreakfastProducts { get; set; } = new List<BreakfastProduct>();

    public List<LunchProduct> LunchProducts { get; set; } = new List<LunchProduct>();

    public List<DinnerProduct> DinnerProducts { get; set; } = new List<DinnerProduct>();

    public  DateTime Date { get; set; }

    public User User { get; set; }
}
