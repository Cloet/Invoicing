﻿// <auto-generated />
using System;
using Invoicing.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Invoicing.EntityFramework.Migrations
{
    [DbContext(typeof(InvoicingDbContext))]
    partial class InvoicingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Invoicing.Domain.Model.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ArticleCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("double precision");

                    b.Property<int?>("VATId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VATId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<bool>("MainMunicipality")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Postal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Address_Id")
                        .HasColumnType("integer");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EMail")
                        .HasColumnType("text");

                    b.Property<string>("Mobile")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telephone")
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Address_Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressCityId")
                        .HasColumnType("integer");

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressStreet")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("InvoiceNumber")
                        .HasColumnType("integer");

                    b.Property<double>("TotalExcludingVAT")
                        .HasColumnType("double precision");

                    b.Property<double>("TotalIncludingVAT")
                        .HasColumnType("double precision");

                    b.Property<double>("TotalVAT")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("AddressCityId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.InvoiceLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ArticleCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ArticleDescription")
                        .HasColumnType("text");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("integer");

                    b.Property<int>("InvoiceNumber")
                        .HasColumnType("integer");

                    b.Property<float>("LineNumber")
                        .HasColumnType("real");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("double precision");

                    b.Property<string>("VATCode")
                        .HasColumnType("text");

                    b.Property<double>("VATPercentage")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceLine");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("HashedPassword")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.VAT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Percentage")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("VAT");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Address", b =>
                {
                    b.HasOne("Invoicing.Domain.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Article", b =>
                {
                    b.HasOne("Invoicing.Domain.Model.VAT", "VAT")
                        .WithMany()
                        .HasForeignKey("VATId");

                    b.Navigation("VAT");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.City", b =>
                {
                    b.HasOne("Invoicing.Domain.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Customer", b =>
                {
                    b.HasOne("Invoicing.Domain.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("Address_Id");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Invoice", b =>
                {
                    b.HasOne("Invoicing.Domain.Model.City", "AddressCity")
                        .WithMany()
                        .HasForeignKey("AddressCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressCity");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.InvoiceLine", b =>
                {
                    b.HasOne("Invoicing.Domain.Model.Invoice", null)
                        .WithMany("InvoiceLines")
                        .HasForeignKey("InvoiceId");
                });

            modelBuilder.Entity("Invoicing.Domain.Model.Invoice", b =>
                {
                    b.Navigation("InvoiceLines");
                });
#pragma warning restore 612, 618
        }
    }
}
