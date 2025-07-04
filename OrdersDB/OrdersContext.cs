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

    public virtual DbSet<Orderline> Orderlines { get; set; }

    public virtual DbSet<ProductView> ProductViews { get; set; }

    public virtual DbSet<UsersView> UsersViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=orders;user=root;password=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("address");

            entity.HasIndex(e => e.CreatedBy, "UserCreated");

            entity.HasIndex(e => e.UpdatedBy, "UserUpdated");

            entity.Property(e => e.AddressId).HasMaxLength(20);
            entity.Property(e => e.Apartment)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.City).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime).HasMaxLength(6);
            entity.Property(e => e.House).HasMaxLength(20);
            entity.Property(e => e.Region).HasMaxLength(20);
            entity.Property(e => e.Street).HasMaxLength(20);
            entity.Property(e => e.UpdatedBy).HasMaxLength(20);
            entity.Property(e => e.UpdatedDateTime).HasMaxLength(6);
            entity.Property(e => e.Zip).HasMaxLength(20);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.AddressFrom, "AddressFrom");

            entity.HasIndex(e => e.AddressTo, "AddressTo");

            entity.HasIndex(e => e.CreatedBy, "OrderCreatedUser");

            entity.HasIndex(e => e.UpdatedBy, "OrderUpdatedUser");

            entity.Property(e => e.OrderId).HasMaxLength(20);
            entity.Property(e => e.AddressFrom).HasMaxLength(20);
            entity.Property(e => e.AddressTo).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime).HasMaxLength(6);
            entity.Property(e => e.UpdatedBy).HasMaxLength(20);
            entity.Property(e => e.UpdatedDateTime).HasMaxLength(6);

            entity.HasOne(d => d.AddressFromNavigation).WithMany(p => p.OrderAddressFromNavigations)
                .HasForeignKey(d => d.AddressFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AddressFrom");

            entity.HasOne(d => d.AddressToNavigation).WithMany(p => p.OrderAddressToNavigations)
                .HasForeignKey(d => d.AddressTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AddressTo");
        });

        modelBuilder.Entity<Orderline>(entity =>
        {
            entity.HasKey(e => e.OrderLineId).HasName("PRIMARY");

            entity.ToTable("orderlines");

            entity.HasIndex(e => e.OrderId, "OrderId");

            entity.HasIndex(e => e.CreatedBy, "OrderLineCreated");

            entity.HasIndex(e => e.ProductId, "ProductId");

            entity.Property(e => e.OrderLineId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasDefaultValueSql("'current_timestamp(6)'");
            entity.Property(e => e.OrderId).HasMaxLength(20);
            entity.Property(e => e.ProductId).HasMaxLength(11);
            entity.Property(e => e.Quantity).HasColumnType("int(11)");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderlines)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("OrderId");
        });

        modelBuilder.Entity<ProductView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("product_view");

            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Ean).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.ProductId).HasMaxLength(20);
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.UpdatedBy).HasMaxLength(20);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
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
