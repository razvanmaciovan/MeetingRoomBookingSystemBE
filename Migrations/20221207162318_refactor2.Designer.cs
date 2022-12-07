﻿// <auto-generated />
using System;
using Locus.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Locus.Migrations
{
    [DbContext(typeof(EntitiesDbContext))]
    [Migration("20221207162318_refactor2")]
    partial class refactor2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Locus.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LayoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LayoutId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Locus.Models.Layout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<int?>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Layouts");
                });

            modelBuilder.Entity("Locus.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DayOfReservation")
                        .HasColumnType("Date");

                    b.Property<string>("EndInterval")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("StartInterval")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Locus.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("LayoutId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LayoutId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Locus.Models.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FloorCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("Locus.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int?>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Locus.Models.Image", b =>
                {
                    b.HasOne("Locus.Models.Layout", "Layout")
                        .WithMany("Images")
                        .HasForeignKey("LayoutId");

                    b.Navigation("Layout");
                });

            modelBuilder.Entity("Locus.Models.Layout", b =>
                {
                    b.HasOne("Locus.Models.Tenant", "Tenant")
                        .WithMany("Layouts")
                        .HasForeignKey("TenantId");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Locus.Models.Reservation", b =>
                {
                    b.HasOne("Locus.Models.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Locus.Models.Room", b =>
                {
                    b.HasOne("Locus.Models.Layout", "Layout")
                        .WithMany("Rooms")
                        .HasForeignKey("LayoutId");

                    b.Navigation("Layout");
                });

            modelBuilder.Entity("Locus.Models.User", b =>
                {
                    b.HasOne("Locus.Models.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Locus.Models.Layout", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Locus.Models.Room", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Locus.Models.Tenant", b =>
                {
                    b.Navigation("Layouts");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
