using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace warehouse_management.UsersDB;

public partial class UsersContext : DbContext
{
    public UsersContext()
    {
    }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessFunction> AccessFunctions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersAccess> UsersAccesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=users;user=root;password=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessFunction>(entity =>
        {
            entity.HasKey(e => e.AccessId).HasName("PRIMARY");

            entity.ToTable("access_function");

            entity.Property(e => e.AccessId).HasMaxLength(20);
            entity.Property(e => e.AccessName).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasMaxLength(20);
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasDefaultValueSql("'current_timestamp(6)'");
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<UsersAccess>(entity =>
        {
            entity.HasKey(e => e.UserAccessId).HasName("PRIMARY");

            entity.ToTable("users_access");

            entity.HasIndex(e => e.AccessId, "accessId");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.UserAccessId).HasColumnType("int(11)");
            entity.Property(e => e.AccessId).HasMaxLength(20);
            entity.Property(e => e.UserId).HasMaxLength(20);

            entity.HasOne(d => d.Access).WithMany(p => p.UsersAccesses)
                .HasForeignKey(d => d.AccessId)
                .HasConstraintName("accessId");

            entity.HasOne(d => d.User).WithMany(p => p.UsersAccesses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("userId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
