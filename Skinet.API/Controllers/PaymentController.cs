using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.DTO;
using Skinet.API.Errors;
using Skinet.Core.Services;

namespace Skinet.API.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentServices _paymentServices;

        public PaymentController( IPaymentServices paymentServices
            )
        {
            _paymentServices = paymentServices;
        }



        [Authorize]    
        [HttpPost("{basketId}")]
        
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePayment(string basketId)
        {
            var basket = await _paymentServices.CreateOrUpdatePaymentIntend(basketId);

            if (basket is null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "there is an error in your basket"));

            return Ok(basket);
                    
        }

    }
}
