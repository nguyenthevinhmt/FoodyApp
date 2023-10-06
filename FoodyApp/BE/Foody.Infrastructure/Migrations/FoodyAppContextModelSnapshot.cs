﻿// <auto-generated />
using System;
using Foody.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Foody.Infrastructure.Migrations
{
    [DbContext(typeof(FoodyAppContext))]
    partial class FoodyAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Foody.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryImageUrl = "no-image.png",
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(8989),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 813, DateTimeKind.Local).AddTicks(1261),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Các món cơm",
                            IsDeleted = false,
                            Name = "Cơm",
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CategoryImageUrl = "no-image.png",
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(8992),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 813, DateTimeKind.Local).AddTicks(1266),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Các món ăn nhanh",
                            IsDeleted = false,
                            Name = "Đồ ăn nhanh",
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CategoryImageUrl = "no-image.png",
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(8994),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 813, DateTimeKind.Local).AddTicks(1269),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Các đồ uống",
                            IsDeleted = false,
                            Name = "Đồ uống",
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CategoryImageUrl = "no-image.png",
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(8995),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 813, DateTimeKind.Local).AddTicks(1271),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Các món bún",
                            IsDeleted = false,
                            Name = "Bún",
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CategoryImageUrl = "no-image.png",
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(8997),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 813, DateTimeKind.Local).AddTicks(1274),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Các món mì",
                            IsDeleted = false,
                            Name = "Mì",
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Foody.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<int>("ProductCartId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Foody.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Foody.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("ActualPrice")
                        .HasColumnType("float");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsActived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Foody.Domain.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Foody.Domain.Entities.ProductPromotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PromotionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PromotionId");

                    b.ToTable("ProductPromotions");
                });

            modelBuilder.Entity("Foody.Domain.Entities.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<double>("DiscountPercent")
                        .HasColumnType("float");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PromotionCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Promotion", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(7034),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 812, DateTimeKind.Local).AddTicks(8690),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Không giảm giá",
                            DiscountPercent = 0.0,
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Không giảm giá",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(7040),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 812, DateTimeKind.Local).AddTicks(8705),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Giảm giá 5%",
                            DiscountPercent = 5.0,
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Giảm giá 5%",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10,
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(7041),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 812, DateTimeKind.Local).AddTicks(8707),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Giảm giá 10%",
                            DiscountPercent = 10.0,
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Giảm giá 10%",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 20,
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(7042),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 812, DateTimeKind.Local).AddTicks(8709),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Giảm giá 20%",
                            DiscountPercent = 20.0,
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Giảm giá 20%",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 25,
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(7043),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 812, DateTimeKind.Local).AddTicks(8710),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Giảm giá 25%",
                            DiscountPercent = 25.0,
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Giảm giá 25%",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 50,
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 51, DateTimeKind.Local).AddTicks(7044),
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 812, DateTimeKind.Local).AddTicks(8711),
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            CreatedBy = "",
                            Description = "Giảm giá 50%",
                            DiscountPercent = 50.0,
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Giảm giá 50%",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Foody.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 43, DateTimeKind.Local).AddTicks(8650),
                            CreatedBy = "",
                            Email = "Admin@gmail.com",
                            IsDeleted = false,
                            Password = "nuqNqnLqFPc7+on6MVYCOQXijEaVS1mX4tV/qkhvcO3qyHr8",
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 792, DateTimeKind.Local).AddTicks(5557),
                            CreatedBy = "",
                            Email = "Admin@gmail.com",
                            IsDeleted = false,
                            Password = "QcoAcWh++/5xdunI4q3dTIqIha67b4S872huG4XL/Wif0/K7",
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 1
                        },
                        new
                        {
                            Id = 2,
<<<<<<< HEAD
                            CreatedAt = new DateTime(2023, 9, 29, 16, 42, 5, 46, DateTimeKind.Local).AddTicks(9329),
                            CreatedBy = "",
                            Email = "Customer@gmail.com",
                            IsDeleted = false,
                            Password = "GKvE255f4ogqoydh5ipLFSfRu9pRY/51S1nwO1gFSGFWF1KY",
=======
                            CreatedAt = new DateTime(2023, 9, 30, 15, 8, 1, 802, DateTimeKind.Local).AddTicks(2596),
                            CreatedBy = "",
                            Email = "Customer@gmail.com",
                            IsDeleted = false,
                            Password = "6tvZ/I1SBMzh+8UKFKUWBGyDY4XsaILy/7NF0kphvB6iRCkz",
>>>>>>> dea53bea39d2f420446064117c5444b2e3ad71d4
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdateBy = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserType = 2
                        });
                });

            modelBuilder.Entity("Foody.Domain.Entities.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetailAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Ward")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("Foody.Domain.Entities.Order", b =>
                {
                    b.HasOne("Foody.Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Foody.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("Foody.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Foody.Domain.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Foody.Domain.Entities.Product", b =>
                {
                    b.HasOne("Foody.Domain.Entities.Category", "Categories")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Foody.Domain.Entities.ProductImage", b =>
                {
                    b.HasOne("Foody.Domain.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Foody.Domain.Entities.ProductPromotion", b =>
                {
                    b.HasOne("Foody.Domain.Entities.Product", "Product")
                        .WithMany("ProductPromotion")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Foody.Domain.Entities.Promotion", "Promotion")
                        .WithMany("ProductPromotions")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("Foody.Domain.Entities.UserAddress", b =>
                {
                    b.HasOne("Foody.Domain.Entities.User", "User")
                        .WithMany("UserAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Foody.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Foody.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Foody.Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductPromotion");
                });

            modelBuilder.Entity("Foody.Domain.Entities.Promotion", b =>
                {
                    b.Navigation("ProductPromotions");
                });

            modelBuilder.Entity("Foody.Domain.Entities.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("UserAddresses");
                });
#pragma warning restore 612, 618
        }
    }
}
