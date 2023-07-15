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
    [Migration("20230327102039_aa11sdf54")]
    partial class aa11sdf54
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Hotel_POS.Model.Bill", b =>
                {
                    b.Property<int>("billId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("billData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("billDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("grandTotal")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TM");
                });

            modelBuilder.Entity("Hotel_POS.Model.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CashierId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
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
