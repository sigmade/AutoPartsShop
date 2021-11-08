﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sigmade.Domain;

namespace Sigmade.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211108142542_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChildSubChild", b =>
                {
                    b.Property<int>("ChildsId")
                        .HasColumnType("int");

                    b.Property<int>("SubChildsId")
                        .HasColumnType("int");

                    b.HasKey("ChildsId", "SubChildsId");

                    b.HasIndex("SubChildsId");

                    b.ToTable("ChildSubChild");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MainId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MainId");

                    b.ToTable("Childs");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.Main", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mains");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.OrderHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("UserContragentId")
                        .HasColumnType("int");

                    b.Property<string>("VendorCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserContragentId");

                    b.ToTable("OrderHistories");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.SearchHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserContragentId")
                        .HasColumnType("int");

                    b.Property<string>("UserIpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserContragentId");

                    b.ToTable("SearchHistories");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.SubChild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubChild");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.UserContragent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserContragents");
                });

            modelBuilder.Entity("ChildSubChild", b =>
                {
                    b.HasOne("Sigmade.Domain.Models.Child", null)
                        .WithMany()
                        .HasForeignKey("ChildsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sigmade.Domain.Models.SubChild", null)
                        .WithMany()
                        .HasForeignKey("SubChildsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sigmade.Domain.Models.Child", b =>
                {
                    b.HasOne("Sigmade.Domain.Models.Main", "Main")
                        .WithMany("Childs")
                        .HasForeignKey("MainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Main");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.OrderHistory", b =>
                {
                    b.HasOne("Sigmade.Domain.Models.UserContragent", "UserContragent")
                        .WithMany("Orders")
                        .HasForeignKey("UserContragentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserContragent");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.SearchHistory", b =>
                {
                    b.HasOne("Sigmade.Domain.Models.UserContragent", "UserContragent")
                        .WithMany()
                        .HasForeignKey("UserContragentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserContragent");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.UserContragent", b =>
                {
                    b.HasOne("Sigmade.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.Main", b =>
                {
                    b.Navigation("Childs");
                });

            modelBuilder.Entity("Sigmade.Domain.Models.UserContragent", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}