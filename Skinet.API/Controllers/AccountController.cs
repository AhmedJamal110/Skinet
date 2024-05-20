using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.API.DTO;
using Skinet.API.Errors;
using Skinet.API.Extensions;
using Skinet.Core.Identity;
using Skinet.Core.Services;
using System.Security.Claims;

namespace Skinet.API.Controllers
{

	public class AccountController : BaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenServices _token;
		private readonly IMapper _mapper;

		public AccountController( UserManager<AppUser> userManager  , SignInManager<AppUser> signInManager ,
			ITokenServices token , IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_token = token;
			_mapper = mapper;
		}


        [HttpPost("login")]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]

		public async Task<ActionResult<UserDto>> Login( LoginDto model )
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null)
				return Unauthorized( new ApiResponse(StatusCodes.Status401Unauthorized));

			var email = await _signInManager.CheckPasswordSignInAsync(user, model.Password , false);

			if(!email.Succeeded)
				return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));

			var obj = new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _token.CreateTokenAsync(user) ,
			};

			return Ok(obj);
		}




		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			if (CheckEmailExit(model.Email).Result.Value) return BadRequest(new ApiValidationResponse()
			{
				Errors = new string[] { "Email Is Already Exist" }
			});

			var user = new AppUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				UserName = model.Email.Split("@")[0],
			};

			var result = await _userManager.CreateAsync(user , model.Password);

			if (!result.Succeeded)
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));

			var obj = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _token.CreateTokenAsync(user)
			};

			return Ok(obj);
		}



		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
		    var user = await _userManager.FindByEmailAsync(email);

			var obj = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _token.CreateTokenAsync(user)
			};
			return Ok(obj);

		}


		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("address")]
		public async Task<ActionResult<AddressDto>> GetAddress()
		{
				
			var user = await _userManager.FindUserIncludeAddress(User);

			var addressMapped = _mapper.Map<Address, AddressDto>(user.Address);

			return Ok(addressMapped);
		}
		
		
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPut("address")]		
		public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto dto)
		{

			var user = await _userManager.FindUserIncludeAddress(User);
			
			var MappedAdress = _mapper.Map<AddressDto, Address>(dto);

			MappedAdress.Id = user.Address.Id;
			user.Address = MappedAdress; 

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));

			return Ok(dto);

		}


		[HttpGet("emailexit")]
		public async Task<ActionResult<bool>> CheckEmailExit(string email)
		{
			return await _userManager.FindByEmailAsync(email) is not null ;
			//return await _userManager.Users.AnyAsync(U => U.Email == email.ToLower());

		
		}


		
	
	}
}
