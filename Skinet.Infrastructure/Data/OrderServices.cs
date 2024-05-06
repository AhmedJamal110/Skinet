using Skinet.API.Entities;
using Skinet.Core.Orders_Aggregate;
using Skinet.Core.Repository;
using Skinet.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderServices(IUnitOfWork unitOfWork , IBasketRepository basketRepository )
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodId, OrderAddress address)
        {
            // 1- basket 

              var basket = await _basketRepository.GetBasketAsync(BasketId);

            // 2- get items at basket form product
            List<OrderItem> orderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                   var product = await _unitOfWork.Repository<Product>().GetByIDAsync(item.Id);

                    var productItemOrder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrder, item.Quntity, product.Price);
                    
                      orderItems.Add(orderItem);
                
                }

            }

            // 3- subtotal

            var subTotal = orderItems.Sum(O => O.Price * O.Quntity);

            // 4- get deliveryMethod

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIDAsync(deliveryMethodId);

            // 5- create order

            var order = new Order(BuyerEmail, address, deliveryMethod, orderItems, subTotal);
            await _unitOfWork.Repository<Order>().Add(order);

            // 6- save to database
            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return null;
           await  _basketRepository.DeleteBasketAsync(BasketId);

            return order;
        }

       

        public Task<Order> GetOrderForUserByIdAsync(string BuyerEmail, int orderId)
        {

            var Spec = new OrderSpecification(BuyerEmail, orderId);
          var order =   _unitOfWork.Repository<Order>().GetByIdWithSpec(Spec);
            return order;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var Spec = new OrderSpecification(BuyerEmail);

          var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecByAsync(Spec);

            return orders;

        }





        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {

            var dliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

            return dliveryMethod;

        }
    }
}
