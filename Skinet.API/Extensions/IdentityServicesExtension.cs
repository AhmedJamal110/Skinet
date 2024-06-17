using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Skinet.Core.Identity;
using Skinet.Infrastructure.Data.Identity;
using System.Text;

namespace Skinet.API.Extensions
{
	public static class IdentityServicesExtension
	{
		public static IServiceCollection  IdentityServices(this IServiceCollection services , IConfiguration configuration )
		{

			services.AddDbContext<AppIdentityDbContext>(opt =>
			{
				opt.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

			});

			services.AddIdentity<AppUser, IdentityRole>()
				    .AddEntityFrameworkStores<AppIdentityDbContext>( );

			services.AddAuthentication( opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(option =>
				{
					option.TokenValidationParameters = new TokenValidationParameters
					{

						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]) ),
						ValidateIssuer = true,
						ValidIssuer = configuration["Token:ValidIssuer"],
						ValidateAudience =true,
						ValidAudience = configuration["Token:ValidAudiance"],
						ValidateLifetime = true,



					};


				});
					

			return services;
		}



	}
}
