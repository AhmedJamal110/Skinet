using Skinet.Core.Orders_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specification
{
    public class OrderSpecification : BaseSpecification<Order>
    {

        
        public OrderSpecification(string email):base( O => O.BuyerEmail == email)
        {

            InCludes.Add(O => O.deliveryMethod);
            InCludes.Add(O => O.Items);


            AddOrderByDesc(O => O.OderDate);


        }


        public OrderSpecification(string email , int id):base
            (
               O =>(O.BuyerEmail == email && O.Id == id)  
                
            )
        {
            InCludes.Add(O => O.deliveryMethod);
            InCludes.Add(O => O.Items);


            AddOrderByDesc(O => O.OderDate);
        }



    }
}
