
using Microsoft.EntityFrameworkCore;
using Store.Core;
using Store.Core.AutoMapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Helper;
using Store.Repository;
using Store.Repository.Data;
using Store.Repository.Data.Contexts;
using Store.Repository.Repositories;
using Store.Service.Services.Products;
using Store.Repository.Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.Identity;


#region The Old One 
/*
namespace Store
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // DI
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile(builder.Configuration)));
            builder.Services.AddScoped<IBasketRepository,BasketRepository>();
            ///////// add sigleton multiplexer ......

            var app = builder.Build();


            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context  = services.GetRequiredService<StoreDbContext>();
            var loggerFactory  = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                // we can use here Console.WriteLine but we need to show the error not like an script but showing it like error:
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There are Prolblem during apply migrations !");

            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // use to allow static file in response : like images to show it in response body 
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
*/
#endregion


#region Last One
var builder = WebApplication.CreateBuilder(args);

// Register dependencies
builder.Services.AddDependencyCallInProgram(builder.Configuration); // Custom DI method

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add metadata for API
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Blend Store API",
        Version = "v1",
        Description = "The Blend Store API enables customers to register, manage their accounts, add products to their cart, and place orders." +
        " It supports CRUD operations on products, brands, and types, and facilitates secure payments through Stripe. " +
        "This API serves as the backend for an e-commerce platform, providing an interface for managing orders, products, and payments with Stripe gateway.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "HESHAM",  
            Url = new Uri("https://www.linkedin.com/in/ichatosha/") 
        }
    });
});
// Build the application
var app = builder.Build();

// Configure middleware pipeline
await app.ConfigureMiddlewareAsync(); 
// Run the application
app.Run();


#endregion