using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Store
{
   public class Store:BaseEntity
    {
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string SellerName { get; set; }
        public string Plaque { get; set; }
        public short vahed { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public long User_ID { get; set; }

        [ForeignKey(nameof(User_ID))]
        public User.User user { get; set; }
        public ICollection<Invoice>  invoices{ get; set; }

    }
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(p => p.StoreName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.StoreAddress).IsRequired().HasMaxLength(500);
            builder.Property(p => p.SellerName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Plaque).HasMaxLength(10);
            builder.Property(p => p.vahed).HasMaxLength(3);
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Phone).HasMaxLength(11).IsRequired();
            builder.Property(p => p.User_ID).IsRequired();
        }
    }
}
