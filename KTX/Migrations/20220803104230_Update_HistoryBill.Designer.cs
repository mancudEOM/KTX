﻿// <auto-generated />
using System;
using KTX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KTX.Migrations
{
    [DbContext(typeof(KtxDbContext))]
    [Migration("20220803104230_Update_HistoryBill")]
    partial class Update_HistoryBill
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KTX.Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("ElectricityIndexE")
                        .HasColumnType("real");

                    b.Property<float>("ElectricityIndexS")
                        .HasColumnType("real");

                    b.Property<int?>("HistoryBillId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<float>("WaterIndexE")
                        .HasColumnType("real");

                    b.Property<float>("WaterIndexS")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("HistoryBillId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("KTX.Models.HistoryBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("HistoryBills");
                });

            modelBuilder.Entity("KTX.Models.HistoryRent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("HistoryRents");
                });

            modelBuilder.Entity("KTX.Models.RelativeUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Job")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RelativeUsers");
                });

            modelBuilder.Entity("KTX.Models.Rent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DueDateRent")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HistoryRentId")
                        .HasColumnType("int");

                    b.Property<string>("Semeter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDateRent")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HistoryRentId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("KTX.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Block")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("KTX.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KTX.Models.Bill", b =>
                {
                    b.HasOne("KTX.Models.HistoryBill", null)
                        .WithMany("Bills")
                        .HasForeignKey("HistoryBillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KTX.Models.HistoryBill", b =>
                {
                    b.HasOne("KTX.Models.Room", "Room")
                        .WithOne("HistoryBill")
                        .HasForeignKey("KTX.Models.HistoryBill", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("KTX.Models.HistoryRent", b =>
                {
                    b.HasOne("KTX.Models.User", "User")
                        .WithOne("HistoryRent")
                        .HasForeignKey("KTX.Models.HistoryRent", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KTX.Models.RelativeUser", b =>
                {
                    b.HasOne("KTX.Models.User", null)
                        .WithMany("RelativeUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KTX.Models.Rent", b =>
                {
                    b.HasOne("KTX.Models.HistoryRent", null)
                        .WithMany("Rents")
                        .HasForeignKey("HistoryRentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KTX.Models.User", b =>
                {
                    b.HasOne("KTX.Models.Room", null)
                        .WithMany("Users")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KTX.Models.HistoryBill", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("KTX.Models.HistoryRent", b =>
                {
                    b.Navigation("Rents");
                });

            modelBuilder.Entity("KTX.Models.Room", b =>
                {
                    b.Navigation("HistoryBill");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("KTX.Models.User", b =>
                {
                    b.Navigation("HistoryRent");

                    b.Navigation("RelativeUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
