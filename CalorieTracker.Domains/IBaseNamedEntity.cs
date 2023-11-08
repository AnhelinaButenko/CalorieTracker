namespace CalorieTracker.Domains;

public interface IBaseNamedEntity : IBaseEntity
{
    public string Name { get; set; }
}

public class BaseNamedEntity : BaseEntity, IBaseNamedEntity
{
    public string Name { get; set; }
}