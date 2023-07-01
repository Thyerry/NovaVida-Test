﻿// <auto-generated />
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(BaseContext))]
    partial class BaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("Entity.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("IMAGE_URL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("NAME");

                    b.Property<double>("Price")
                        .HasColumnType("REAL")
                        .HasColumnName("PRICE");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("URL");

                    b.HasKey("Id");

                    b.ToTable("T_PRODUCT");
                });
#pragma warning restore 612, 618
        }
    }
}