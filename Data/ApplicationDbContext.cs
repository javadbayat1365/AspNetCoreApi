using Common.Utilities;
using Entities.Common;
using Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,long> // 
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source = .;Initial Catalog = MyApiDB;Integrated Security = true");
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var entitiesAssembly = typeof(IEntity).Assembly;
            //Added All Entities To DB Context
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly,entitiesAssembly);
            //Added All Fuent Api To DB Context
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            //Removed Cascade Relations
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            //change To Pluralizing Tables Name
            modelBuilder.AddPluralizingTableNameConvention();
        }

        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// این متد در هر عمل ذخیره و یا ویرایش در دیتابیس اعداد فارسی رو به انگلیسی و حروف عربی رو به فارسی تبدیل میکند
        /// </summary>
        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
    }
}
