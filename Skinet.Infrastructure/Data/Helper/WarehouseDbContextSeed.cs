using Skinet.API.Entities;
using Skinet.Core.Entities;
using Skinet.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data.Helper
{
	public static class WarehouseDbContextSeed
	{
		public static async Task seedDataAsync(WarehouseDbContext context)
		{
			if (!context.ProductBrands.Any())
			{
				var brandData = File.ReadAllText("../Skinet.Infrastructure/Data/DataSeeding/brands.json");
				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
				if (Brands?.Count > 0)
				{
					foreach (var brand in Brands)
					{
						await context.Set<ProductBrand>().AddAsync(brand);
					}
					await context.SaveChangesAsync();
				}
			}


			if (!context.ProductTypes.Any())
			{
				var typesData = File.ReadAllText("../Skinet.Infrastructure/Data/DataSeeding/types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
				if (types?.Count > 0)
				{
					foreach (var type in types)
					{
						await context.Set<ProductType>().AddAsync(type);
					}
					await context.SaveChangesAsync();
				}

			}


			if (!context.Products.Any())
			{
				var productData = File.ReadAllText("../Skinet.Infrastructure/Data/DataSeeding/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productData);
				if (products?.Count > 0)
				{
					foreach (var product in products)
					{
						await context.Set<Product>().AddAsync(product);
					}
					await context.SaveChangesAsync();
				}
			}



		}



	}
}
