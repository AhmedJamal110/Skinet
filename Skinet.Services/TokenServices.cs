using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Skinet.Core.Identity;
using Skinet.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Services
{
	public class TokenServices : ITokenServices
	{
		private readonly IConfiguration _configuration;
		private readonly SymmetricSecurityKey _key;

		public TokenServices(IConfiguration configuration )
        {
			_configuration = configuration;

			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:key"]));
		}

        public async Task<string> CreateTokenAsync(AppUser user)
		{

			var Claims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName , user.DisplayName),

				new Claim(ClaimTypes.Email , user.Email)
			};

			var token = new JwtSecurityToken(

				claims: Claims,
				issuer: _configuration["Token:ValidIssuer"],
				audience: _configuration["Token:ValidAudiance"],
				signingCredentials : new SigningCredentials(_key, SecurityAlgorithms.HmacSha256),
				expires : DateTime.Now.AddDays(double.Parse(_configuration["Token:ExpirationTime"]) )
			);


			return  new JwtSecurityTokenHandler().WriteToken(token);



		}
	}
}
