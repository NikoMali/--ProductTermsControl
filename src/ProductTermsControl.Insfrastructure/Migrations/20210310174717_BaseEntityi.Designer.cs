﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductTermsControl.Insfrastructure.Helpers;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210310174717_BaseEntityi")]
    partial class BaseEntityi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.BranchProductStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsOutOfStock")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OutOfStockReason")
                        .HasColumnType("text");

                    b.Property<int>("ProductToBranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("ProductToBranchId");

                    b.ToTable("BranchProductStocks");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("IdentificationCode")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Companys");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.Magazine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("IdentificationCode")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Magazines");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.MagazineBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("IdentificationCode")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<int>("MagazineId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("MagazineId");

                    b.ToTable("MagazineBranches");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("IdentificationCode")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.ProductToBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<int>("DaysBeforeNotifiWarning")
                        .HasColumnType("int");

                    b.Property<int>("MagazineBranchId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<int>("ResponsiblePersonsGroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TermDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("MagazineBranchId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ResponsiblePersonsGroupId");

                    b.ToTable("ProductToBranches");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.ResponsiblePersonsForProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<int>("ResponsiblePersonsGroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResponsiblePersonsGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("ResponsiblePersonsForProducts");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.ResponsiblePersonsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("ResponsiblePersonsGroups");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(4000)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(4000)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.UserReference", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("MagazineBranchId")
                        .HasColumnType("int");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("MagazineBranchId");

                    b.HasIndex("PositionId");

                    b.ToTable("UserReferences");
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.BranchProductStock", b =>
                {
                    b.HasOne("ProductTermsControl.Domain.Entities.ProductToBranch", "ProductToBranch")
                        .WithMany("BranchProductStocks")
                        .HasForeignKey("ProductToBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.MagazineBranch", b =>
                {
                    b.HasOne("ProductTermsControl.Domain.Entities.Magazine", "Magazine")
                        .WithMany("MagazineBranchs")
                        .HasForeignKey("MagazineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.Product", b =>
                {
                    b.HasOne("ProductTermsControl.Domain.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.ProductToBranch", b =>
                {
                    b.HasOne("ProductTermsControl.Domain.Entities.MagazineBranch", "MagazineBranch")
                        .WithMany("ProductToBranchs")
                        .HasForeignKey("MagazineBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductTermsControl.Domain.Entities.Product", "Product")
                        .WithMany("ProductToBranchs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductTermsControl.Domain.Entities.ResponsiblePersonsGroup", "ResponsiblePersonsGroup")
                        .WithMany("ProductToBranchs")
                        .HasForeignKey("ResponsiblePersonsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.ResponsiblePersonsForProduct", b =>
                {
                    b.HasOne("ProductTermsControl.Domain.Entities.ResponsiblePersonsGroup", "ResponsiblePersonsGroup")
                        .WithMany()
                        .HasForeignKey("ResponsiblePersonsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductTermsControl.Domain.Entities.User", "User")
                        .WithMany("ResponsiblePersonsByProducts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductTermsControl.Domain.Entities.UserReference", b =>
                {
                    b.HasOne("ProductTermsControl.Domain.Entities.MagazineBranch", "MagazineBranchs")
                        .WithMany("UserReferences")
                        .HasForeignKey("MagazineBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductTermsControl.Domain.Entities.Position", "Positions")
                        .WithMany("UserReferences")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductTermsControl.Domain.Entities.User", "User")
                        .WithMany("UserReferences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
