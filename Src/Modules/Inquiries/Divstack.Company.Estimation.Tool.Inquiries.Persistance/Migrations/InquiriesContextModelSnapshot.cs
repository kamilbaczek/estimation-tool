﻿// <auto-generated />
using System;
using Divstack.Company.Estimation.Tool.Inquiries.Persistance.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Divstack.Company.Estimation.Tool.Inquiries.Persistance.Migrations
{
    [DbContext(typeof(InquiriesContext))]
    partial class InquiriesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("Divstack.Company.Estimation.Tool.Inquiries.Domain.Inquiries.Inquiry", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Inquiries");
                });

            modelBuilder.Entity("Divstack.Company.Estimation.Tool.Inquiries.Domain.Inquiries.Inquiry", b =>
                {
                    b.OwnsOne("Divstack.Company.Estimation.Tool.Inquiries.Domain.Clients.Client", "Client", b1 =>
                        {
                            b1.Property<Guid>("InquiryId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("InquiryId");

                            b1.ToTable("Inquiries");

                            b1.WithOwner()
                                .HasForeignKey("InquiryId");

                            b1.OwnsOne("Divstack.Company.Estimation.Tool.Inquiries.Domain.Clients.ClientCompany", "Company", b2 =>
                                {
                                    b2.Property<Guid>("ClientInquiryId")
                                        .HasColumnType("char(36)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("longtext");

                                    b2.Property<string>("Size")
                                        .IsRequired()
                                        .HasColumnType("longtext");

                                    b2.HasKey("ClientInquiryId");

                                    b2.ToTable("Inquiries");

                                    b2.WithOwner()
                                        .HasForeignKey("ClientInquiryId");
                                });

                            b1.OwnsOne("Divstack.Company.Estimation.Tool.Shared.DDD.ValueObjects.Emails.Email", "Email", b2 =>
                                {
                                    b2.Property<Guid>("ClientInquiryId")
                                        .HasColumnType("char(36)");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(255)
                                        .HasColumnType("varchar(255)");

                                    b2.HasKey("ClientInquiryId");

                                    b2.ToTable("Inquiries");

                                    b2.WithOwner()
                                        .HasForeignKey("ClientInquiryId");
                                });

                            b1.Navigation("Company");

                            b1.Navigation("Email");
                        });

                    b.OwnsMany("Divstack.Company.Estimation.Tool.Inquiries.Domain.Inquiries.Item.InquiryItem", "InquiryItems", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("InquiryId")
                                .HasColumnType("char(36)");

                            b1.HasKey("Id");

                            b1.HasIndex("InquiryId");

                            b1.ToTable("InquiryItemsServices");

                            b1.WithOwner("Inquiry")
                                .HasForeignKey("InquiryId");

                            b1.OwnsOne("Divstack.Company.Estimation.Tool.Inquiries.Domain.Inquiries.Item.Services.Service", "Service", b2 =>
                                {
                                    b2.Property<Guid>("InquiryItemId")
                                        .HasColumnType("char(36)");

                                    b2.Property<Guid?>("ServiceId")
                                        .HasColumnType("char(36)");

                                    b2.HasKey("InquiryItemId");

                                    b2.ToTable("InquiryItemsServices");

                                    b2.WithOwner()
                                        .HasForeignKey("InquiryItemId");

                                    b2.OwnsMany("Divstack.Company.Estimation.Tool.Inquiries.Domain.Inquiries.Item.Services.Attributes.Attribute", "Attributes", b3 =>
                                        {
                                            b3.Property<Guid>("Id")
                                                .HasColumnType("char(36)");

                                            b3.Property<Guid>("ServiceInquiryItemId")
                                                .HasColumnType("char(36)");

                                            b3.Property<Guid?>("ValueId")
                                                .HasColumnType("char(36)");

                                            b3.HasKey("Id");

                                            b3.HasIndex("ServiceInquiryItemId");

                                            b3.ToTable("InquiryItemsServicesAttributes");

                                            b3.WithOwner("Service")
                                                .HasForeignKey("ServiceInquiryItemId");

                                            b3.Navigation("Service");
                                        });

                                    b2.Navigation("Attributes");
                                });

                            b1.Navigation("Inquiry");

                            b1.Navigation("Service");
                        });

                    b.Navigation("Client");

                    b.Navigation("InquiryItems");
                });
#pragma warning restore 612, 618
        }
    }
}
