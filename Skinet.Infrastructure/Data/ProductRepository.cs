using Microsoft.EntityFrameworkCore;
using Skinet.API.Entities;
using Skinet.Core.Repository;
using Skinet.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data
{


	public class ProductRepository : IProductRepository
	{
		private readonly WarehouseDbContext _context;

		public ProductRepository(WarehouseDbContext context )
        {
				_context = context;
		}
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
		{
			return await _context.Products.ToListAsync();

		}

		public async Task<Product> GetProductByIDAsync(int id )
		{
			return await _context.Products.FindAsync(id);
		}
	}
}
