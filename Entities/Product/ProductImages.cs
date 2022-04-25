using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Product
{
   public class ProductImages:BaseEntity
    {
        public string ImageUrl { get; set; }
        public long Product_ID { get; set; }

        [ForeignKey(nameof(Product_ID))]
        public Product Product { get; set; }
    }

    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImages>
    {
        public void Configure(EntityTypeBuilder<ProductImages> builder)
        {
            builder.Property(p => p.ImageUrl).IsRequired().HasMaxLength(1000);
            builder.Property(p => p.Product_ID).IsRequired();
            builder.HasOne(h => h.Product).WithMany(w => w.ProductImages).HasForeignKey(h => h.Product_ID);
        }
    }
}
