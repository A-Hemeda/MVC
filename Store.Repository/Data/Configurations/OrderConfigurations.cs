using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<OrderO>
    {
        public void Configure(EntityTypeBuilder<OrderO> builder)
        {
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2))");

            builder.Property(O => O.Status)
                .HasConversion(OStatus => OStatus.ToString(), OStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), OStatus));

            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());

            builder.HasOne(O => O.DeliveryMethod).WithMany().HasForeignKey(O => O.DeliveryMethodId);



        }
    }
}
