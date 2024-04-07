using CalorieTracker.Api.Seeder;
using CalorieTracker.Data;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using CalorieTracker.Service;
using Microsoft.AspNetCore.StaticFiles;

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
        builder.Services.AddSwaggerGen(); //настраивают Swagger/ OpenAPI дл€ документировани€ API

        builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

        builder.Services.AddDbContext<CalorieTrackerDbContext>(o => o.EnableSensitiveDataLogging());

        builder.Services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<CalorieTrackerDbContext>(); // настраивает Identity Framework с пользовательским классом User,
                                                                  // классом ролей Role и использованием CalorieTrackerDbContext дл€ хранени€ данных

        builder.Services.AddScoped<IDataSeeder, DataSeeder>(); // добавл€ет DataSeeder как Scoped сервис дл€ заполнени€ начальных данных
                                                               // —ервис создаетс€ один раз на каждый HTTP-запрос, и весь запрос использует один и тот же экземпл€р сервиса.
                                                               // Ёто полезно дл€ сервисов с состо€нием, которые должны сохран€ть данные в пределах одного запроса


        builder.Services.AddTransient<IFileService, FileService>(); // добавл€ет FileService как transient сервис дл€ обработки файлов
                                                                    // ƒл€ каждого запроса создаетс€ новый экземпл€р сервиса.
                                                                    // Ёто подходит, если сервис не хранит состо€ние между вызовами и можно
                                                                    // безопасно создавать новый экземпл€р дл€ каждого запроса.

        builder.Services.AddScoped<IProductRepository, ProductRepository>(); // добавл€ет ProductRepository как Scoped сервис дл€ работы с продуктами в базе данных

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

        builder.Services.AddScoped<IDailyForDayRepository, DailyForDayRepository>();

        builder.Services.AddScoped<IBreakfastProductRepository, BreakfastProductRepository>();

        builder.Services.AddScoped<ILunchProductRepository, LunchProductRepository>();

        builder.Services.AddScoped<IDinnerProductRepository, DinnerProductRepository>();

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddScoped<IManufacturerService, ManufacturerService>();

        builder.Services.AddScoped<ICategoryService, CategoryService>();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}