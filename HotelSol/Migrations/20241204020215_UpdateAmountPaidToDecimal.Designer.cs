﻿// <auto-generated />
using System;
using HotelSol.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelSol.Migrations
{
    [DbContext(typeof(HotelSolDbContext))]
    [Migration("20241204020215_UpdateAmountPaidToDecimal")]
    partial class UpdateAmountPaidToDecimal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelSol.Data.Models.Guests", b =>
                {
                    b.Property<int>("GuestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GuestID");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("HotelSol.Data.Models.Payments", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationID")
                        .HasColumnType("int");

                    b.HasKey("PaymentID");

                    b.HasIndex("ReservationID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("HotelSol.Data.Models.ReservationRoom", b =>
                {
                    b.Property<int>("ReservationRoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ReservationID")
                        .HasColumnType("int");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("ReservationRoomID");

                    b.HasIndex("ReservationID");

                    b.HasIndex("RoomID");

                    b.ToTable("ReservationRooms");
                });

            modelBuilder.Entity("HotelSol.Data.Models.Reservations", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GuestID")
                        .HasColumnType("int");

                    b.HasKey("ReservationID");

                    b.HasIndex("GuestID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("HotelSol.Data.Models.Rooms", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PricePerNight")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<string>("RoomType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelSol.Data.Models.Payments", b =>
                {
                    b.HasOne("HotelSol.Data.Models.Reservations", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("HotelSol.Data.Models.ReservationRoom", b =>
                {
                    b.HasOne("HotelSol.Data.Models.Reservations", "Reservations")
                        .WithMany("ReservationRooms")
                        .HasForeignKey("ReservationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelSol.Data.Models.Rooms", "Rooms")
                        .WithMany("ReservationRooms")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservations");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelSol.Data.Models.Reservations", b =>
                {
                    b.HasOne("HotelSol.Data.Models.Guests", "Guests")
                        .WithMany()
                        .HasForeignKey("GuestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guests");
                });

            modelBuilder.Entity("HotelSol.Data.Models.Reservations", b =>
                {
                    b.Navigation("ReservationRooms");
                });

            modelBuilder.Entity("HotelSol.Data.Models.Rooms", b =>
                {
                    b.Navigation("ReservationRooms");
                });
#pragma warning restore 612, 618
        }
    }
}
