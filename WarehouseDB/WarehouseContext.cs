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

            entity.ToTable("products");

            entity.HasIndex(e => e.ProductId, "Product_ID").IsUnique();

            entity.HasIndex(e => e.CreatedUserId, "User");

            entity.Property(e => e.ProductId)
                .HasMaxLength(6)
                .HasColumnName("Product_ID");
            entity.Property(e => e.CreatedUserId)
                .HasMaxLength(20)
                .HasColumnName("Created_User_ID");
            entity.Property(e => e.ProductCreatedDate)
                .HasMaxLength(6)
                .HasColumnName("Product_Created_Date");
            entity.Property(e => e.ProductEan)
                .HasMaxLength(11)
                .HasColumnName("Product_EAN");
            entity.Property(e => e.ProductName)
                .HasMaxLength(10)
                .HasColumnName("Product_Name");
            entity.Property(e => e.ProductPrice).HasColumnName("Product_Price");
            entity.Property(e => e.ProductQuantity).HasColumnName("Product_Quantity");
            entity.Property(e => e.ProductType)
                .HasMaxLength(20)
                .HasColumnName("Product_Type");
            entity.Property(e => e.ProductWeight).HasColumnName("Product_Weight");
        });

        modelBuilder.Entity<ProductPriceHistory>(entity =>
        {
            entity.HasKey(e => e.ProductPriceId).HasName("PRIMARY");

            entity.ToTable("product_price_history");

            entity.HasIndex(e => e.CreatedUserId, "Created User");

            entity.HasIndex(e => e.ProductId, "Product Id Price");

            entity.HasIndex(e => e.ProductPriceId, "Product_Price_ID");

            entity.HasIndex(e => e.ProductPriceId, "Product_Price_ID_2").IsUnique();

            entity.HasIndex(e => e.ProductPriceId, "Product_Price_ID_3");

            entity.Property(e => e.ProductPriceId)
                .HasColumnType("int(11)")
                .HasColumnName("Product_Price_ID");
            entity.Property(e => e.ChangeTime)
                .HasColumnType("datetime")
                .HasColumnName("Change_Time");
            entity.Property(e => e.CreatedUserId)
                .HasMaxLength(20)
                .HasColumnName("Created_User_ID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(6)
                .HasColumnName("Product_ID");
            entity.Property(e => e.ProductPrice).HasColumnName("Product_Price");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPriceHistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Product Id Price");
        });

        modelBuilder.Entity<ProductQuantityHistory>(entity =>
        {
            entity.HasKey(e => e.ProductQuantityId).HasName("PRIMARY");

            entity.ToTable("product_quantity_history");

            entity.HasIndex(e => e.CreatedUserId, "Created User Id");

            entity.HasIndex(e => e.ProductId, "Product Id Quantity");

            entity.HasIndex(e => e.ProductQuantityId, "Product_Quantity_ID").IsUnique();

            entity.Property(e => e.ProductQuantityId)
                .HasColumnType("int(10)")
                .HasColumnName("Product_Quantity_ID");
            entity.Property(e => e.ChangeTime)
                .HasMaxLength(6)
                .HasColumnName("Change_Time");
            entity.Property(e => e.CreatedUserId)
                .HasMaxLength(20)
                .HasColumnName("Created_User_ID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(6)
                .HasColumnName("Product_ID");
            entity.Property(e => e.ProductQuantity).HasColumnName("Product_Quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductQuantityHistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Product Id Quantity");
        });

        modelBuilder.Entity<UsersView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("users_view");

            entity.Property(e => e.CreatedDate)
                .HasMaxLength(6)
                .HasColumnName("Created_Date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("User_ID");
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
