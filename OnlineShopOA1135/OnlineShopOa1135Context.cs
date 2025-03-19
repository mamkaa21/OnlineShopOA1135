using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineShopOA1135.Model;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace OnlineShopOA1135;

    public partial class OnlineShopOa1135Context : DbContext
    {
        public OnlineShopOa1135Context()
        {
        }

        public OnlineShopOa1135Context(DbContextOptions<OnlineShopOa1135Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Good> Goods { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseMySql("server=192.168.200.13;database=OnlineShopOA1135;user=student;password=student", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Good>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Amount).HasColumnType("int(11)");
                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Category_Id");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Image).HasColumnType("mediumblob");
                entity.Property(e => e.Price).HasPrecision(10);
                entity.Property(e => e.Rating).HasColumnType("int(11)");
                entity.Property(e => e.Review).HasMaxLength(255);
                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.Login).HasMaxLength(255);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Role_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }


