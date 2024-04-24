using Skinet.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Services
{
	public interface ITokenServices
	{

		Task<string> CreateTokenAsync(AppUser user);

	}
}
