﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190825084626_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Common.BaseInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<long?>("ParentBaseInfo_ID");

                    b.HasKey("Id");

                    b.HasIndex("ParentBaseInfo_ID");

                    b.ToTable("BaseInfos");
                });

            modelBuilder.Entity("Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Mobile");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<DateTime>("RegisterDate");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Entities.Invoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<bool>("IsActive");

                    b.Property<long>("ProductCompany_ID");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<int>("Status");

                    b.Property<long>("Store_ID");

                    b.Property<long>("User_ID");

                    b.HasKey("Id");

                    b.HasIndex("ProductCompany_ID");

                    b.HasIndex("Store_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Entities.Location.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BaseInfo_LocationType_ID");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<long?>("ParentLocation_ID");

                    b.HasKey("Id");

                    b.HasIndex("BaseInfo_LocationType_ID");

                    b.HasIndex("ParentLocation_ID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Entities.Product.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BaseInfo_Brand_ID");

                    b.Property<long>("BaseInfo_Category_ID");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<DateTime>("RegisterDate");

                    b.HasKey("Id");

                    b.HasIndex("BaseInfo_Brand_ID");

                    b.HasIndex("BaseInfo_Category_ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entities.Product.ProductCompany", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("Commission");

                    b.Property<long>("Company_ID");

                    b.Property<decimal>("CustomerPriceUnit");

                    b.Property<decimal>("CustomerTotalPrice");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<long>("Product_ID");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<decimal>("StorePriceUnit");

                    b.Property<decimal>("StoreTotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("Company_ID");

                    b.HasIndex("Product_ID");

                    b.ToTable("ProductCompanies");
                });

            modelBuilder.Entity("Entities.Product.ProductImages", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<bool>("IsActive");

                    b.Property<long>("Product_ID");

                    b.HasKey("Id");

                    b.HasIndex("Product_ID");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Entities.Store.Store", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Lat");

                    b.Property<string>("Lng");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Plaque")
                        .HasMaxLength(10);

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("StoreAddress")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<long>("User_ID");

                    b.Property<short>("vahed")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.HasIndex("User_ID");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Entities.User.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Entities.User.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("Age");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Gender");

                    b.Property<bool>("IsActive");

                    b.Property<DateTimeOffset>("LastLoginDate");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<long?>("RoleId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<long>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Entities.Common.BaseInfo", b =>
                {
                    b.HasOne("Entities.Common.BaseInfo", "ParentBaseInfo")
                        .WithMany("ChildBaseInfos")
                        .HasForeignKey("ParentBaseInfo_ID");
                });

            modelBuilder.Entity("Entities.Invoice", b =>
                {
                    b.HasOne("Entities.Product.ProductCompany", "ProductCompany")
                        .WithMany("Invoices")
                        .HasForeignKey("ProductCompany_ID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Entities.Store.Store", "Store")
                        .WithMany("invoices")
                        .HasForeignKey("Store_ID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Entities.User.User", "User")
                        .WithMany("invoices")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Entities.Location.Location", b =>
                {
                    b.HasOne("Entities.Common.BaseInfo", "baseInfo_LocationType")
                        .WithMany("Locations")
                        .HasForeignKey("BaseInfo_LocationType_ID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Entities.Location.Location", "Parentlocation")
                        .WithMany("ChildLocations")
                        .HasForeignKey("ParentLocation_ID");
                });

            modelBuilder.Entity("Entities.Product.Product", b =>
                {
                    b.HasOne("Entities.Common.BaseInfo", "BrandBaseInfo")
                        .WithMany()
                        .HasForeignKey("BaseInfo_Brand_ID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Entities.Common.BaseInfo", "CategoryBaseInfo")
                        .WithMany()
                        .HasForeignKey("BaseInfo_Category_ID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Entities.Product.ProductCompany", b =>
                {
                    b.HasOne("Entities.Company", "Company")
                        .WithMany("ProductCompanies")
                        .HasForeignKey("Company_ID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Entities.Product.Product", "Product")
                        .WithMany("ProductCompanies")
                        .HasForeignKey("Product_ID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Entities.Product.ProductImages", b =>
                {
                    b.HasOne("Entities.Product.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("Product_ID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Entities.Store.Store", b =>
                {
                    b.HasOne("Entities.User.User", "user")
                        .WithMany("stores")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Entities.User.User", b =>
                {
                    b.HasOne("Entities.User.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Entities.User.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Entities.User.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Entities.User.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Entities.User.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Entities.User.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Entities.User.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
