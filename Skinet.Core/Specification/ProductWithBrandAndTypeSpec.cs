using Skinet.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Specification
{
	public class ProductWithBrandAndTypeSpec : BaseSpecification<Product>
	{




        public ProductWithBrandAndTypeSpec(ProductParmSpec parmSpec ) :
            base
            (
                P =>(!parmSpec.BrandId.HasValue || P.ProductBrandId == parmSpec.BrandId) && 
                (!parmSpec.TypeId.HasValue || P.ProductTypeId == parmSpec.TypeId) &&
                (string.IsNullOrEmpty(parmSpec.Search) || P.Name.ToLower().Contains(parmSpec.Search))
            )
        {

            

            if (!string.IsNullOrEmpty(parmSpec.Sort))
            {
                switch (parmSpec.Sort)
                {
                    case "priceAsc":
                        AddOerderBy( O => O.Price);
                            break;
                    case "priceDesc":
                        AddOrderByDesc( O => O.Price);
                        break;
                    default:
                        AddOerderBy( O => O.Name);
                            break;
                
                }
            }


            AddPagination(parmSpec.PigeSize * (parmSpec.PigeIndex - 1) , parmSpec.PigeSize);
            InCludes.Add(P => P.ProductBrand);
            InCludes.Add(P => P.ProductType);
        }

        public ProductWithBrandAndTypeSpec(int id):base(P => P.Id == id)
        {
			InCludes.Add(P => P.ProductBrand);
			InCludes.Add(P => P.ProductType);

		}

    }
}
