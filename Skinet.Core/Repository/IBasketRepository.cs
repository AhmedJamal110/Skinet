using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Repository
{
	public interface IBasketRepository
	{

		Task<CustomerBasket?> GetBasketAsync(string basketid);

		Task<CustomerBasket?> UpdateBsketAsync(CustomerBasket basket);


		Task<bool> DeleteBasketAsync(string basketId);
    }
}
