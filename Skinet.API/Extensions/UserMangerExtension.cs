using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Identity;
using System.Security.Claims;

namespace Skinet.API.Extensions
{
	public static class UserMangerExtension
	{
		public static async Task<AppUser> FindUserIncludeAddress( this UserManager<AppUser> userManager , ClaimsPrincipal User)
		{

			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(E => E.Email == email);

			return user;



		}


	}
}
