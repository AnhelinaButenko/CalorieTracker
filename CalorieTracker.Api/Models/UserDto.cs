using CalorieTracker.Domains;

namespace CalorieTracker.Api.Models;

public class UserDto
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string? Email { get; set; }

    public double CurrentWeight { get; set; }

    public double DesiredWeight { get; set; }

    public double Height { get; set; }

    public int Age { get; set; }

    public Gender Gender { get; set; }

    public ActivityLevel ActivityLevel { get; set; }
}
