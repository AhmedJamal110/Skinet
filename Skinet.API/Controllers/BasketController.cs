using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.DTO;
using Skinet.API.Errors;
using Skinet.Core.Entities;
using Skinet.Core.Repository;

namespace Skinet.API.Controllers
{
	public class BasketController : BaseController
	{
		private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController( IBasketRepository basketRepository , IMapper mapper)
        {
			_basketRepository = basketRepository;
            _mapper = mapper;
        }

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string basketId)
		{
			var basket = await _basketRepository.GetBasketAsync(basketId);
			return basket is null ? new CustomerBasket(basketId) : basket;
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
		{
			var customerMapped = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

			var CreatedOrUpdated =await _basketRepository.UpdateBsketAsync(customerMapped);

			if (CreatedOrUpdated is null)
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));

			return Ok(CreatedOrUpdated);
		}


		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket( string basketId)
		{
			return await _basketRepository.DeleteBasketAsync(basketId);
		}

    }
}
