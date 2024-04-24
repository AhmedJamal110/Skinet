using Microsoft.EntityFrameworkCore;
using Skinet.API.Entities;
using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data.Context
{
	public class WarehouseDbContext : DbContext
 	{

        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {

            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand>  ProductBrands { get; set; }

    }
}
