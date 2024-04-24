using Microsoft.EntityFrameworkCore;
using Skinet.API.Entities;
using Skinet.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data
{
	public static class SpcificationEvaluter<T> where T : BaseEntity
	{

		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecification<T> Spec)
		{
			var query = inputQuery;

			if (Spec.Criteria is not null)
				 query =  query.Where(Spec.Criteria);

			if(Spec.OrderBy is not null)
			{
				query = query.OrderBy(Spec.OrderBy);
			}
		
			if(Spec.OrderByDesc is not null)
			{
				query = query.OrderByDescending(Spec.OrderByDesc);
			}

			if (Spec.IsPaginationEnabled)
			{
				query = query.Skip(Spec.Skip).Take(Spec.Take);
			}
			
		  query = Spec.InCludes.Aggregate(query, (currentQuery, includeExpress) => currentQuery.Include(includeExpress));


			return query;
		}

	}
}
