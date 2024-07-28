using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace warehouse_management.OrdersDB;

public partial class OrdersContext : DbContext
{
    public OrdersContext()
    {
    }

    public OrdersContext(DbContextOptions<OrdersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProductLine> OrderProductLines { get; set; }

    public virtual DbSet<ProductsView> ProductsViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=orders;user=root;password=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("address");

            entity.HasIndex(e => e.UpdateUserId, "Updated User");

            entity.HasIndex(e => e.CreatedBy, "User");

            entity.Property(e => e.AddressId)
                .HasMaxLength(20)
                .HasColumnName("Address_ID");
            entity.Property(e => e.AddressApartment)
                .HasMaxLength(20)
                .HasColumnName("Address_Apartment");
            entity.Property(e => e.AddressCity)
                .HasMaxLength(20)
                .HasColumnName("Address_City");
            entity.Property(e => e.AddressCountry)
                .HasMaxLength(20)
                .HasColumnName("Address_Country");
            entity.Property(e => e.AddressHouse)
                .HasMaxLength(10)
                .HasColumnName("Address_House");
            entity.Property(e => e.AddressRegion)
                .HasMaxLength(20)
                .HasColumnName("Address_Region");
            entity.Property(e => e.AddressStreet)
                .HasMaxLength(20)
                .HasColumnName("Address_Street");
            entity.Property(e => e.AddressZipCode)
                .HasMaxLength(20)
                .HasColumnName("Address_ZIP_Code");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasColumnName("Created_Date_Time");
            entity.Property(e => e.UpdateDateTime)
                .HasMaxLength(6)
                .HasColumnName("Update_Date_Time");
            entity.Property(e => e.UpdateUserId)
                .HasMaxLength(20)
                .HasColumnName("Update_User_ID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.AddressFrom, "Address from");

            entity.HasIndex(e => e.AddressTo, "Address to");

            entity.HasIndex(e => e.CreatedBy, "Order user");

            entity.HasIndex(e => e.UpdatedUserId, "Update_id");

            entity.Property(e => e.OrderId)
                .HasMaxLength(20)
                .HasColumnName("Order_ID");
            entity.Property(e => e.AddressFrom)
                .HasMaxLength(20)
                .HasColumnName("Address_From");
            entity.Property(e => e.AddressTo)
                .HasMaxLength(20)
                .HasColumnName("Address_To");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasColumnName("Created_Date_Time");
            entity.Property(e => e.UpdateDateTime)
                .HasMaxLength(6)
                .HasColumnName("Update_Date_Time");
            entity.Property(e => e.UpdatedUserId)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("Updated_User_ID");

            entity.HasOne(d => d.AddressFromNavigation).WithMany(p => p.OrderAddressFromNavigations)
                .HasForeignKey(d => d.AddressFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Address from");

            entity.HasOne(d => d.AddressToNavigation).WithMany(p => p.OrderAddressToNavigations)
                .HasForeignKey(d => d.AddressTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Address to");
        });

        modelBuilder.Entity<OrderProductLine>(entity =>
        {
            entity.HasKey(e => e.OrderLineId).HasName("PRIMARY");

            entity.ToTable("order_product_line");

            entity.HasIndex(e => e.CreatedBy, "Created By");

            entity.HasIndex(e => e.OrderId, "Order");

            entity.HasIndex(e => e.ProductId, "Product");

            entity.Property(e => e.OrderLineId)
                .HasColumnType("int(11)")
                .HasColumnName("Order_Line_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasColumnName("Created_Date_Time");
            entity.Property(e => e.OrderId)
                .HasMaxLength(20)
                .HasColumnName("Order_ID");
            entity.Property(e => e.OrderProductQuantity).HasColumnName("Order_Product_Quantity");
            entity.Property(e => e.ProductId)
                .HasMaxLength(6)
                .HasColumnName("Product_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProductLines)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("Order");
        });

        modelBuilder.Entity<ProductsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("products_view");

            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasColumnName("Created_Date_Time");
            entity.Property(e => e.CreatedUserId)
                .HasMaxLength(20)
                .HasColumnName("Created_User_ID");
            entity.Property(e => e.ProductEan)
                .HasMaxLength(11)
                .HasColumnName("Product_EAN");
            entity.Property(e => e.ProductId)
                .HasMaxLength(6)
                .HasColumnName("Product_ID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(10)
                .HasColumnName("Product_Name");
            entity.Property(e => e.ProductPrice).HasColumnName("Product_Price");
            entity.Property(e => e.ProductQuantity).HasColumnName("Product_Quantity");
            entity.Property(e => e.ProductType)
                .HasMaxLength(20)
                .HasColumnName("Product_Type");
            entity.Property(e => e.ProductWeight).HasColumnName("Product_Weight");
            entity.Property(e => e.UpdateDateTime)
                .HasMaxLength(6)
                .HasColumnName("Update_Date_Time");
            entity.Property(e => e.UpdatedUserId)
                .HasMaxLength(20)
                .HasColumnName("Updated_User_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
