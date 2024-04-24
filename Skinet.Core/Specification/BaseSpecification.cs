using Skinet.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specification
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Expression<Func<T, object>>> InCludes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get ; set ; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Take { get; set; }
		public int Skip { get; set; }
		public bool IsPaginationEnabled { get; set; }

		public BaseSpecification()
        {

        }

        public BaseSpecification( Expression<Func<T , bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;


		}

		public void AddOerderBy(Expression<Func<T , object>> orderByExperssion)
		{
			OrderBy = orderByExperssion;
		}


		public void AddOrderByDesc(Expression<Func<T , object>> orderByDescExpression)
		{
			OrderByDesc = orderByDescExpression;
		}
	
	
		public void AddPagination(int skip , int take)
		{
			IsPaginationEnabled = true;
			Skip = skip;
			Take = take;
		
		}
	
	
	}
}
