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

            entity.ToTable("access_functions");

            entity.HasIndex(e => e.AccessId, "Access_ID").IsUnique();

            entity.Property(e => e.AccessId)
                .HasMaxLength(10)
                .HasColumnName("Access_ID");
            entity.Property(e => e.AccessName)
                .HasMaxLength(20)
                .HasColumnName("Access_Name");
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasColumnName("Created_Date_Time");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserId, "User_ID").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("User_ID");
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasColumnName("Created_Date_Time");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<UsersAccess>(entity =>
        {
            entity.HasKey(e => e.UserAccessId).HasName("PRIMARY");

            entity.ToTable("users_access");

            entity.HasIndex(e => e.AccessId, "Access");

            entity.HasIndex(e => e.UserId, "User");

            entity.HasIndex(e => e.UserAccessId, "User_Access_ID").IsUnique();

            entity.Property(e => e.UserAccessId)
                .HasColumnType("int(11)")
                .HasColumnName("User_Access_ID");
            entity.Property(e => e.AccessId)
                .HasMaxLength(20)
                .HasColumnName("Access_ID");
            entity.Property(e => e.CreatedDateTime)
                .HasMaxLength(6)
                .HasColumnName("Created_Date_Time");
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("User_ID");

            entity.HasOne(d => d.Access).WithMany(p => p.UsersAccesses)
                .HasForeignKey(d => d.AccessId)
                .HasConstraintName("Access");

            entity.HasOne(d => d.User).WithMany(p => p.UsersAccesses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
