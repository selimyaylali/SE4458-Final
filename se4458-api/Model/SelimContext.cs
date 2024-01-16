using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace se4458_api.Model;

public partial class SelimContext : DbContext
{
    public SelimContext()
    {
    }

    public SelimContext(DbContextOptions<SelimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:syaylalimidterm.database.windows.net,1433;Initial Catalog=selim;Persist Security Info=False;User ID=20070006072@stu.yasar.edu.tr;Password=Rzr53abb;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F2128F00160DBD0");

            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.PharmacyId).HasName("PK__Pharmaci__BD9D2A8E34FD62B4");

            entity.Property(e => e.PharmacyId).HasColumnName("PharmacyID");
            entity.Property(e => e.AuthenticationCredentials).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__40130812BB6C589F");

            entity.Property(e => e.PrescriptionId).HasColumnName("PrescriptionID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

            entity.Property(e => e.PatientTc)
                .HasMaxLength(11)
                .HasColumnName("PatientTC");

            entity.Property(e => e.PharmacyId).HasColumnName("PharmacyID");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(10, 2)");
            entity.HasOne(d => d.Pharmacy).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PharmacyId)
                .HasConstraintName("FK__Prescript__Pharm__2CF2ADDF");
        });


        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B5759782D");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.PharmacyId).HasColumnName("PharmacyID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Pharmacy).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PharmacyId)
                .HasConstraintName("FK__Transacti__Pharm__339FAB6E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
