using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Agri_Energy_Connect.Models;

public partial class AgriEnergyConnectContext : DbContext
{
    public AgriEnergyConnectContext()
    {
    }

    public AgriEnergyConnectContext(DbContextOptions<AgriEnergyConnectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Farm> Farms { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    public virtual DbSet<FarmerCategory> FarmerCategories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=labVMH8OX\\SQLEXPRESS;Database=AgriEnergyConnect;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83FEBBC508F");

            entity.ToTable("employee");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.AdminAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("admin_address");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__farm__3213E83F73723764");

            entity.ToTable("farm");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FarmAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("farm_address");
            entity.Property(e => e.FarmDescription)
                .HasColumnType("text")
                .HasColumnName("farm_description");
            entity.Property(e => e.FarmName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("farm_name");
            entity.Property(e => e.FarmerId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("farmer_id");

            entity.HasOne(d => d.Farmer).WithMany(p => p.Farms)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__farm__farmer_id__5FB337D6");
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__farmer__3213E83F6F23523D");

            entity.ToTable("farmer");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FarmerAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("farmer_address");
            entity.Property(e => e.FarmerCategoryId).HasColumnName("farmer_category_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.FarmerCategory).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.FarmerCategoryId)
                .HasConstraintName("FK__farmer__farmer_c__5CD6CB2B");

           
        });

        modelBuilder.Entity<FarmerCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__farmer_c__3213E83F7A375CE7");

            entity.ToTable("farmer_category");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category_name");
            entity.Property(e => e.Details)
                .HasColumnType("text")
                .HasColumnName("details");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83F6BAC01D5");

            entity.ToTable("product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Details)
                .HasColumnType("text")
                .HasColumnName("details");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.FarmerId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("farmer_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductCategoryId).HasColumnName("product_category_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductionDate).HasColumnName("production_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("unit");

            entity.HasOne(d => d.Farmer).WithMany(p => p.Products)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__farmer___6383C8BA");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__product__6477ECF3");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3213E83F360DD718");

            entity.ToTable("product_category");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category_name");
            entity.Property(e => e.Details)
                .HasColumnType("text")
                .HasColumnName("details");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
