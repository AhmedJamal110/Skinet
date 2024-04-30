using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Orders_Aggregate
{
    public class ProductItemOrder
    {
        public ProductItemOrder()
        {
            
        }
        public ProductItemOrder(int productId, string productName, string productPictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPictureUrl = productPictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPictureUrl { get; set; }

    }
}
