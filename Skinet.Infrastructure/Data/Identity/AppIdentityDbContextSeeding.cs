using Microsoft.AspNetCore.Identity;
using Skinet.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data.Identity
{
	public  class AppIdentityDbContextSeeding
	{
		public static async  Task IdentitySeedingData( UserManager<AppUser> userManager	)
		{
			if (!userManager.Users.Any())
			{
				var user = new AppUser()
				{
					DisplayName = "Ahmed Gamal",
					Email = "ahmed@gamal.com",
					PhoneNumber = "01234567890",
					UserName = "Ahmed",
					Address = new Address
					{
						FirstName = "Ahmed",
						LastName = "Gamal",
						Street = "Beni Suif",
						City = "Cairo",
						Country = "Egypt",
						ZipCode = "zipcod"
					}
				};

			   await userManager.CreateAsync( user, "Pa$$w0rd");
			}

		}


	}
}
