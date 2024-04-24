using Skinet.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specification
{
	public class ProductWithFiltrationForCount : BaseSpecification<Product>
	{

        public ProductWithFiltrationForCount(ProductParmSpec parmSpec) : base(
          P => (!parmSpec.BrandId.HasValue || P.ProductBrandId == parmSpec.BrandId) &&
          (!parmSpec.TypeId.HasValue || P.ProductTypeId == parmSpec.TypeId) && 
          (string.IsNullOrEmpty(parmSpec.Search) || P.Name.ToLower().Contains(parmSpec.Search))
          )
        {
            
        }

    }
}
