using Skinet.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Orders_Aggregate
{
    public class Order : BaseEntity
    {

        public Order()
        {
            
        }
        public Order(string buyerEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            this.deliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OderDate { get; set; } = DateTimeOffset.Now;

        public OrderAddress ShippingAddress { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public DeliveryMethod deliveryMethod { get; set; }



        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; } // => quntity * price


        public string PaymentIntendId { get; set; } = string.Empty;
        public decimal GetTotal()
        {
            return SubTotal * deliveryMethod.Cost;
        }

    }
}
