namespace CalorieTracker.Api.Models;

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double CurrentWeight { get; set; }

    public double DesiredWeight { get; set; }
}
