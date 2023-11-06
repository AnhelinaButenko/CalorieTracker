namespace CalorieTracker.Domains;

public abstract class BaseNamedEntity
{
    public int Id { get; set; }

    public string Name { get; set; }
}
