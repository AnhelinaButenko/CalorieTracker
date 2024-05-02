using CalorieTracker.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace CalorieTracker.Api.Models;

public class UserDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [EmailAddress]
    [MaxLength(150)]
    public string? Email { get; set; }

    [Range(1, 300)]
    public double CurrentWeight { get; set; }

    [Range(1, 300)]
    public double DesiredWeight { get; set; }

    [Range(1, 250)]
    public double Height { get; set; }

    [Range(1, 150)]
    public int Age { get; set; }

    public Gender Gender { get; set; }

    public ActivityLevel ActivityLevel { get; set; }

    public double? RecommendedCalories { get; set; }

    public double? RecommendedProtein { get; set; }

    public double? RecommendedFat { get; set; }

    public double? RecommendedCarbs { get; set; }
}