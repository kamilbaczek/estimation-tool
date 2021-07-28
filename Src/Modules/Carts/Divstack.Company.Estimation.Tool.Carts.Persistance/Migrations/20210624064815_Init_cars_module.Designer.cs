﻿// <auto-generated />
using System;
using Divstack.Company.Estimation.Tool.Carts.Persistance.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Divstack.Company.Estimation.Tool.Carts.Persistance.Migrations
{
    [DbContext(typeof(CartsContext))]
    [Migration("20210624064815_Init_cars_module")]
    partial class Init_cars_module
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Divstack.Company.Estimation.Tool.Carts.Domain.Carts.Cart", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("datetime");

                    b.Property<int>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Divstack.Company.Estimation.Tool.Carts.Domain.Carts.Cart", b =>
                {
                    b.OwnsMany("Divstack.Company.Estimation.Tool.Carts.Domain.Carts.CartItem", "Items", b1 =>
                        {
                            b1.Property<byte[]>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("varbinary(16)");

                            b1.Property<byte[]>("CartId")
                                .IsRequired()
                                .HasColumnType("varbinary(16)");

                            b1.Property<byte[]>("ProductId")
                                .IsRequired()
                                .HasColumnType("varbinary(16)");

                            b1.HasKey("Id");

                            b1.HasIndex("CartId");

                            b1.ToTable("Items");

                            b1.WithOwner()
                                .HasForeignKey("CartId");
                        });

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}