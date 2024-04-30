using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Orders_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember( Value ="Pending")]
        Pending ,
        [EnumMember(Value ="PaymentRecieve")]
        PaymentRecieve,

        [EnumMember(Value ="PaymentFail")]
        PaymentFail,



    }
}
