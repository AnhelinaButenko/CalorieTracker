using CalorieTracker.Api.Seeder;
using CalorieTracker.Data;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CalorieTracker.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

        builder.Services.AddDbContext<CalorieTrackerDbContext>();

        builder.Services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<CalorieTrackerDbContext>();

        builder.Services.AddScoped<IDataSeeder, DataSeeder>();

       // SeedData(builder.Services);

        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        var app = builder.Build();


        app.UseCors("CorsPolicy");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void SeedData(IServiceCollection services)
    {
        using IServiceScope serviceScope = services.BuildServiceProvider().CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;

        IDataSeeder seedService = provider.GetRequiredService<IDataSeeder>();
        seedService.Seed().GetAwaiter().GetResult();
    }
}