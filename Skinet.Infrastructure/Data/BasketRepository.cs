using Skinet.Core.Entities;
using Skinet.Core.Repository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer redis)
        {
			_database = redis.GetDatabase();
		}
        public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);

		}

		public async Task<CustomerBasket?> GetBasketAsync(string basketid)
		{
			var basket = await _database.StringGetAsync(basketid);

			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}



		public async Task<CustomerBasket?> UpdateBsketAsync(CustomerBasket basket)
		{
			var jsonFormatting = JsonSerializer.Serialize(basket);
			var CreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonFormatting, TimeSpan.FromDays(10));

			if (!CreatedOrUpdated)
				return null;

			return await GetBasketAsync(basket.Id);


		}
	}
}
