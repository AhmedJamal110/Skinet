using Skinet.Core.Orders_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Repository
{
    public interface IOrderServices
    {

         Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodId, OrderAddress address);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail);


        Task<Order> GetOrderForUserByIdAsync(string BuyerEmail, int orderId);


    }
}
