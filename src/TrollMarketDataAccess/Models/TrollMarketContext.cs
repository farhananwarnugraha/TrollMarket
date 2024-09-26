using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrollMarketDataAccess.Models
{
    public partial class TrollMarketContext : DbContext
    {
        public TrollMarketContext()
        {
        }

        public TrollMarketContext(DbContextOptions<TrollMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                 optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TrollMarket; Trusted_Connection=True;User=sa;Password=indocyber; TrustServerCertificate=True");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__OrderPro__C38F30096FC10BAF");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.Property(e => e.UsernameBuyer)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("FK_IdProduct_Products");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK_ShiperOrder_Shipper");

                entity.HasOne(d => d.UsernameBuyerNavigation)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.UsernameBuyer)
                    .HasConstraintName("FK_UsernameBuyer_Account");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CatgoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedProduct).HasDefaultValueSql("((0))");

                entity.Property(e => e.DescriptionProduct)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Discontiue).HasDefaultValueSql("((0))");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SellerUsername)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.SellerUsernameNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SellerUsername)
                    .HasConstraintName("FK_SellerUsername_Account");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shipper");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsService).HasDefaultValueSql("((0))");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ShipperName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__536C85E51799D880");

                entity.HasIndex(e => e.Username, "UQ_Username")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
