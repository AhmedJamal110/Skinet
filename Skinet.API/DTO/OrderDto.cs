using Skinet.Core.Orders_Aggregate;

namespace Skinet.API.DTO
{
    public class OrderDto
    {

        public string BasketId { get; set; }
        public int DeliverMethodId { get; set; }
        public OrderAddressDto shippingAddress { get; set; }

    }
}
