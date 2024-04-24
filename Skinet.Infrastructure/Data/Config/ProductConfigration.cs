using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.API.Entities;
using Skinet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data.Config
{
	public class ProductConfigration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{

			builder.HasOne(P => P.ProductBrand)
				   .WithMany()
				   .HasForeignKey(P => P.ProductBrandId);

			builder.HasOne(P => P.ProductType)
				.WithMany()
				.HasForeignKey(P => P.ProductTypeId);


			
			

			builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
			builder.Property(P => P.Description).IsRequired();
			builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
			builder.Property(P => P.PictureUrl).IsRequired();

			
			
	
		
			
		}
	}
}
