using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.DTO;
using Skinet.API.Errors;
using Skinet.Core.Orders_Aggregate;
using Skinet.Core.Repository;
using System.Security.Claims;

namespace Skinet.API.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrderController( IOrderServices orderServices , IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrders(OrderDto model)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var addressMapped = _mapper.Map<OrderAddressDto, OrderAddress>(model.shippingAddress);

           var order = await  _orderServices.CreateOrderAsync(BuyerEmail, model.BasketId, model.DeliverMethodId, addressMapped);
            if (order is null)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));


            return Ok(order); 
        }


        [HttpGet("getorders")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOedersForUser()
        {
           var buyerEmail =  User.FindFirstValue(ClaimTypes.Email);

           var orders =  await _orderServices.GetOrdersForUserAsync(buyerEmail);

            return Ok(orders);


        }

        [ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderForUser(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);


           var order =   await _orderServices.GetOrderForUserByIdAsync(BuyerEmail, id);

            if (order is null)
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            return order;
        
        }


        [HttpGet("deliverymethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var dliveryMethod = await _orderServices.GetDeliveryMethodAsync();

            return Ok(dliveryMethod);
        }


    }
}
