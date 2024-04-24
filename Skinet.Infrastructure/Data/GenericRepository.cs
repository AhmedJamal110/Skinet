using Microsoft.EntityFrameworkCore;
using Skinet.API.Entities;
using Skinet.Core.Repository;
using Skinet.Core.Specification;
using Skinet.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly WarehouseDbContext _context;

		public GenericRepository(WarehouseDbContext context )
        {
			_context = context;
		}
        public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();

		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecByAsync(ISpecification<T> Spec)
		{
			return await ApplySpecification(Spec).ToListAsync();	
		}

		public async Task<T> GetByIDAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T> GetByIdWithSpec(ISpecification<T> Spec)
		{

			return await ApplySpecification(Spec).FirstOrDefaultAsync();
		}

		public async Task<int> GetCountWithSpecByAsync(ISpecification<T> Spec)
		{

			return await ApplySpecification(Spec).CountAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
		{

			return SpcificationEvaluter<T>.GetQuery(_context.Set<T>().AsQueryable(), Spec);
		
		
		}
	
	
	}
}
