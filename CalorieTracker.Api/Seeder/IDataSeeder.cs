namespace CalorieTracker.Api.Seeder;

public interface IDataSeeder
{
    Task Seed(bool recreateDb = false);
}
