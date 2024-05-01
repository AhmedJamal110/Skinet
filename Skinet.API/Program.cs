using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.API.Errors;
using Skinet.API.Extensions;
using Skinet.API.Helper;
using Skinet.API.Middelware;
using Skinet.Core.Identity;
using Skinet.Core.Repository;
using Skinet.Infrastructure.Data;
using Skinet.Infrastructure.Data.Context;
using Skinet.Infrastructure.Data.Helper;
using Skinet.Infrastructure.Data.Identity;

namespace Skinet.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.ApplicationServices(builder.Configuration);
			builder.Services.IdentityServices(builder.Configuration);














			
			var app = builder.Build();
	   	 using	var scope = app.Services.CreateScope();
			var serviceProvider = scope.ServiceProvider;
			var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
			try
			{
				var dbcontext = serviceProvider.GetRequiredService<WarehouseDbContext>();
				await dbcontext.Database.MigrateAsync();
				await WarehouseDbContextSeed.seedDataAsync(dbcontext);

				var identityDbContext = serviceProvider.GetRequiredService<AppIdentityDbContext>();
				await identityDbContext.Database.MigrateAsync();

				var userManger = serviceProvider.GetRequiredService<UserManager<AppUser>>();
				await AppIdentityDbContextSeeding.IdentitySeedingData(userManger);


			}
			catch (Exception ex)
			{
				var loger = loggerFactory.CreateLogger<Program>();
				loger.LogError(ex, "An error happend");
				
			}

			app.UseMiddleware<ExceptionMiddelware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();
			
			app.UseStaticFiles();

			app.UseCors("CorsPolicy");
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
