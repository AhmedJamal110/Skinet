using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Core.Orders_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data.Config
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(O => O.Status)
                    .HasConversion(S => S.ToString(), S => (OrderStatus)Enum.Parse(typeof(OrderStatus), S));


            builder.OwnsOne(O => O.ShippingAddress, SAddress => SAddress.WithOwner());

            builder.Property(S => S.SubTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
