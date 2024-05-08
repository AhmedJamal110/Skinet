using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Services
{
    public interface IPaymentServices
    {

        Task<CustomerBasket> CreateOrUpdatePaymentIntend( string basketID);
    
    
    }
}
