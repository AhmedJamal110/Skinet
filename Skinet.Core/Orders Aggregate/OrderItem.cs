using Skinet.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Orders_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder productItem, int quntity, decimal price)
        {
            ProductItem = productItem;
            Quntity = quntity;
            Price = price;
        }

        public ProductItemOrder ProductItem { get; set; }

        public int Quntity { get; set; }
        public decimal Price { get; set; }


    }
}
