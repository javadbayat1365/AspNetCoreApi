using Common.Enums;
using Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.User
{
   public class User :IdentityUser<long>,IEntity
    {
        [Required]
        [MaxLength(100)]
        public string Fullname{ get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public DateTimeOffset LastLoginDate { get; set; }
        public DateTime RegisterDate { get; set; }
        //public long Role_ID { get; set; }
        public ICollection<Store.Store> stores { get; set; }
        public ICollection<Invoice>  invoices{ get; set; }
        public bool IsActive { get; set; }
        //public virtual Role Role { get; set; }
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PasswordHash).HasMaxLength(500).IsRequired();
            //builder.HasOne(h => h.Role).WithMany(w => w.Users).HasForeignKey(h => h.Role_ID);
        }
    }
}
