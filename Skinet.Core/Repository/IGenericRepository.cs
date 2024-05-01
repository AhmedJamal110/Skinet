using Skinet.API.Entities;
using Skinet.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Repository
{
	public interface IGenericRepository<T> where T : BaseEntity
	{

		Task<IReadOnlyList<T>> GetAllAsync();
		 
		Task<T> GetByIDAsync(int id);

		Task<IReadOnlyList<T>> GetAllWithSpecByAsync(ISpecification<T> Spec);

		Task<T> GetByIdWithSpec(ISpecification<T> Spec);

		Task<int> GetCountWithSpecByAsync(ISpecification<T> Spec);

		Task Add(T item);


		void Uodate(T item);

		void Delete(T item);

	}
}
