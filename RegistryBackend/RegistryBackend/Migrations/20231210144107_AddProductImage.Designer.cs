﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistryBackend;

#nullable disable

namespace RegistryBackend.Migrations
{
    [DbContext(typeof(RegistryDb))]
    [Migration("20231210144107_AddProductImage")]
    partial class AddProductImage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("RegistryBackend.Model.Departement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departements");
                });

            modelBuilder.Entity("RegistryBackend.Model.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UUID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("RegistryBackend.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartementId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int?>("SaleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DepartementId");

                    b.HasIndex("SaleId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RegistryBackend.Model.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndingTime")
                        .HasColumnType("TEXT");

                    b.Property<double>("PercentageOff")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("StartingTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("RegistryBackend.Model.Product", b =>
                {
                    b.HasOne("RegistryBackend.Model.Departement", "Departement")
                        .WithMany()
                        .HasForeignKey("DepartementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RegistryBackend.Model.Sale", "Sale")
                        .WithMany()
                        .HasForeignKey("SaleId");

                    b.Navigation("Departement");

                    b.Navigation("Sale");
                });
#pragma warning restore 612, 618
        }
    }
}
