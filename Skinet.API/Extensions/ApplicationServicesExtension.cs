using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.API.Errors;
using Skinet.API.Helper;
using Skinet.Core.Repository;
using Skinet.Core.Services;
using Skinet.Infrastructure.Data;
using Skinet.Infrastructure.Data.Context;
using Skinet.Infrastructure.Data.Identity;
using Skinet.Services;
using StackExchange.Redis;

namespace Skinet.API.Extensions
{
	public static class ApplicationServicesExtension
	{

		public static IServiceCollection ApplicationServices( this IServiceCollection services , IConfiguration configuration)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			
			services.AddDbContext<WarehouseDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(typeof(MappingProfile));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IOrderServices, OrderServices>();
			services.AddScoped<IPaymentServices, PaymentServices>();
			services.AddSingleton<IResponseCacheServices, ResponseCacheServices>();
			
			services.AddSingleton<IConnectionMultiplexer>(option =>
			{
				var connection = configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);	
			});

			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			services.AddScoped<ITokenServices, TokenServices>();
		


			services.Configure<ApiBehaviorOptions>(Option =>
			{
				Option.InvalidModelStateResponseFactory = (actionResilt) =>
				{
					var errors = actionResilt.ModelState.Where(e => e.Value.Errors.Count > 0)
														 .SelectMany(e => e.Value.Errors)
														 .Select(e => e.ErrorMessage).ToArray();


					var validationResponse = new ApiValidationResponse() { Errors = errors };


					return new BadRequestObjectResult(validationResponse);
				};
			});


			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
				});
			});


			return services;
		}

	}
}
