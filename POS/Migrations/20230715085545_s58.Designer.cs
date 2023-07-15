﻿// <auto-generated />
using System;
using Hotel_POS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hotel_POS.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230715085545_s58")]
    partial class s58
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Hotel_POS.Model.Bill", b =>
                {
                    b.Property<string>("billId")
                        .HasColumnType("TEXT");

                    b.Property<string>("billData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("billDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("discount")
                        .HasColumnType("REAL");

                    b.Property<double>("grandTotal")
                        .HasColumnType("REAL");

                    b.Property<double>("subTotal")
                        .HasColumnType("REAL");

                    b.HasKey("billId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Hotel_POS.Model.Item", b =>
                {
                    b.Property<int>("itemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("itemCategory")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("itemName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("itemPhoto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("itemPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("itemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Hotel_POS.Model.TestModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ds")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("TM");
                });

            modelBuilder.Entity("Hotel_POS.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CashierId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
