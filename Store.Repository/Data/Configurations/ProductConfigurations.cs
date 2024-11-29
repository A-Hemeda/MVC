using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(200).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired(true);
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(P => P.Brand).WithMany().HasForeignKey(P => P.BrandId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(P => P.Type).WithMany().HasForeignKey(P => P.TypeId).OnDelete(DeleteBehavior.SetNull);
            builder.Property(P => P.BrandId).IsRequired(false);
            builder.Property(P => P.TypeId).IsRequired(false);


        }
    }
}
