﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using se4458_api.Model;

#nullable disable

namespace se4458api.Migrations
{
    [DbContext(typeof(SelimContext))]
    [Migration("20240114235143_AddUserModel")]
    partial class AddUserModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("se4458_api.Model.Medicine", b =>
                {
                    b.Property<int>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MedicineID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicineId"));

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("MedicineId")
                        .HasName("PK__Medicine__4F2128F00160DBD0");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("se4458_api.Model.Pharmacy", b =>
                {
                    b.Property<int>("PharmacyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PharmacyID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PharmacyId"));

                    b.Property<string>("AuthenticationCredentials")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PharmacyId")
                        .HasName("PK__Pharmaci__BD9D2A8E34FD62B4");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("se4458_api.Model.Prescription", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PrescriptionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrescriptionId"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<string>("PatientTc")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("PatientTC");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int")
                        .HasColumnName("PharmacyID");

                    b.Property<decimal?>("TotalCost")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("PrescriptionId")
                        .HasName("PK__Prescrip__40130812BB6C589F");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("se4458_api.Model.PrescriptionDetail", b =>
                {
                    b.Property<int>("PrescriptionDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PrescriptionDetailID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrescriptionDetailId"));

                    b.Property<int?>("MedicineId")
                        .HasColumnType("int")
                        .HasColumnName("MedicineID");

                    b.Property<int?>("PrescriptionId")
                        .HasColumnType("int")
                        .HasColumnName("PrescriptionID");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PrescriptionDetailId")
                        .HasName("PK__Prescrip__6DB7668A477678D2");

                    b.HasIndex("MedicineId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("PrescriptionDetails");
                });

            modelBuilder.Entity("se4458_api.Model.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TransactionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int")
                        .HasColumnName("PharmacyID");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("TransactionId")
                        .HasName("PK__Transact__55433A4B5759782D");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("se4458_api.Model.Prescription", b =>
                {
                    b.HasOne("se4458_api.Model.Pharmacy", "Pharmacy")
                        .WithMany("Prescriptions")
                        .HasForeignKey("PharmacyId")
                        .HasConstraintName("FK__Prescript__Pharm__2CF2ADDF");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("se4458_api.Model.PrescriptionDetail", b =>
                {
                    b.HasOne("se4458_api.Model.Medicine", "Medicine")
                        .WithMany("PrescriptionDetails")
                        .HasForeignKey("MedicineId")
                        .HasConstraintName("FK__Prescript__Medic__30C33EC3");

                    b.HasOne("se4458_api.Model.Prescription", "Prescription")
                        .WithMany("PrescriptionDetails")
                        .HasForeignKey("PrescriptionId")
                        .HasConstraintName("FK__Prescript__Presc__2FCF1A8A");

                    b.Navigation("Medicine");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("se4458_api.Model.Transaction", b =>
                {
                    b.HasOne("se4458_api.Model.Pharmacy", "Pharmacy")
                        .WithMany("Transactions")
                        .HasForeignKey("PharmacyId")
                        .HasConstraintName("FK__Transacti__Pharm__339FAB6E");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("se4458_api.Model.Medicine", b =>
                {
                    b.Navigation("PrescriptionDetails");
                });

            modelBuilder.Entity("se4458_api.Model.Pharmacy", b =>
                {
                    b.Navigation("Prescriptions");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("se4458_api.Model.Prescription", b =>
                {
                    b.Navigation("PrescriptionDetails");
                });
#pragma warning restore 612, 618
        }
    }
}