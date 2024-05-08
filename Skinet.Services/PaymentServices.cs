using Microsoft.Extensions.Configuration;
using Skinet.API.Entities;
using Skinet.Core.Entities;
using Skinet.Core.Orders_Aggregate;
using Skinet.Core.Repository;
using Skinet.Core.Services;
using Stripe;


namespace Skinet.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration _config;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentServices( IConfiguration config , IBasketRepository basketRepository , IUnitOfWork unitOfWork) 
        {
            _config = config;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntend(string basketID)
        {
            StripeConfiguration.ApiKey = _config["StripeSetting:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketID);


            if (basket is null)
                return null;

            var shippingCost = 0M;

            // to update shippingCost
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIDAsync(basket.DeliveryMethodId.Value);
                shippingCost = deliveryMethod.Cost;
                basket.ShippingCost = deliveryMethod.Cost;
            }


            // to uodate Price
            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<API.Entities.Product>().GetByIDAsync(item.Id);

                    if (item.price != product.Price)
                        item.price = product.Price;

                }


            }


            PaymentIntent paymentIntent;
            var service = new PaymentIntentService();

            // 1- check paymentIntend 
            // 2- amount = price * quntity + shippingCost
            
            if (string.IsNullOrEmpty(basket.PaymentIntend))
            {
                var opt = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(I => (I.price * 100) * I.Quntity) + (long)shippingCost * 100,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await service.CreateAsync(opt);
                basket.PaymentIntend = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var opt = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(I => (I.price * 100) * I.Quntity) + (long)shippingCost * 100,


                };

                await service.UpdateAsync(basket.PaymentIntend, opt);
            }

            // update basket
            await _basketRepository.UpdateBsketAsync(basket);
            return basket;
        
                
         }
    }
}
