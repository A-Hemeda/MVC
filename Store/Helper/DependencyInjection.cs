using Microsoft.EntityFrameworkCore;
using Store.Core.AutoMapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Core;
using Store.Repository.Data.Contexts;
using Store.Repository.Repositories;
using Store.Repository;
using Store.Service.Services.Products;
using StackExchange.Redis;
using Store.Core.AutoMapping.Basket;
using Store.Repository.Identity.Contexts;
using Store.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Store.Repository.Identity;
using Store.Service.Services.Caches;
using Store.Service.Services.Tokens;
using Store.Service.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Imaging;
using System.Text;
using Store.Core.AutoMapping.Orders;
using Store.Service.Services.Orders;
using Store.Service.Services.Baskets;
using Store.Service.Services.Payment;
using Store.Service.Services.Coupons;
namespace Store.Helper
{
    //  Before Build :  
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyCallInProgram (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContexts(configuration);
            services.AddServices();
            services.AddRepositories();
            services.AddUnitOfWork();
            services.AddAutoMapperProfile(configuration);
            services.AddSwagger(); 
            services.AddRedisService(configuration);
            services.AddIdentityServices();
            services.AddAuthenticationService(configuration);
            
            return services;
        }

        private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            
            services.AddDbContext<StoreIdentityDBContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("IdentityConnection");
                options.UseSqlServer(connectionString);
            });
             
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICouponService, CouponService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>)); // Add this line for generic repository registration
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        
        // sigleton >> 
        private static IServiceCollection AddRedisService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) => 
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            return services;
        }

        private static IServiceCollection AddAutoMapperProfile(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile(configuration)));
            return services;

        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        // allow DI for all identity built in services : 
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDBContext>();


            return services;
        }

        // allow DI for authentication : 
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
            return services;
        } 


    }
}
