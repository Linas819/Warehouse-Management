using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace warehouse_management.WarehouseDB;

public partial class WarehouseContext : DbContext
{
    public WarehouseContext()
    {
    }

    public WarehouseContext(DbContextOptions<WarehouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPriceHistory> ProductPriceHistories { get; set; }

    public virtual DbSet<ProductQuantityHistory> ProductQuantityHistories { get; set; }

    public virtual DbSet<UsersView> UsersViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=warehouse;user=root;password=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.CreatedBy, "ProductCreated");

            entity.HasIndex(e => e.UpdatedBy, "ProductUpdated");

            entity.Property(e => e.ProductId).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Ean).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.UpdatedBy).HasMaxLength(20);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductPriceHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_price_history");

            entity.HasIndex(e => e.ProductId, "PriceProductId");

            entity.HasIndex(e => e.CreatedBy, "ProductPriceCreated");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasDefaultValueSql("'current_timestamp(6)'");
            entity.Property(e => e.ProductId).HasMaxLength(20);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPriceHistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("PriceProductId");
        });

        modelBuilder.Entity<ProductQuantityHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_quantity_history");

            entity.HasIndex(e => e.CreatedBy, "ProductQuantityCreated");

            entity.HasIndex(e => e.ProductId, "QauntityProduct");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime).HasMaxLength(6);
            entity.Property(e => e.ProductId).HasMaxLength(20);
            entity.Property(e => e.Quantity).HasColumnType("int(11)");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductQuantityHistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("QauntityProduct");
        });

        modelBuilder.Entity<UsersView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("users_view");

            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasDefaultValueSql("'current_timestamp(6)'");
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
