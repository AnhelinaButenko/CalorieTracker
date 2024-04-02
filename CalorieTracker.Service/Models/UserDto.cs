using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
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

    [Range(1, 150)]
    public Gender Gender { get; set; }

    [Range(1, 150)]
    public ActivityLevel ActivityLevel { get; set; }
}