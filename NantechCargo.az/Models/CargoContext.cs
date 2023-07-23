using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class CargoContext : DbContext
    {
        public CargoContext()
        {
        }

        public CargoContext(DbContextOptions<CargoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branchs { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-KI2QPOK\\SQLEXPRESS;Database=Cargo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.BranchAddress).HasMaxLength(100);

                entity.Property(e => e.BranchName).HasMaxLength(50);

                entity.Property(e => e.BranchPhone).HasMaxLength(20);
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.Property(e => e.LevelName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationDate).HasColumnType("date");

                entity.Property(e => e.NotificationDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationIsRead)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.NotificationOrder)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationOrderId)
                    .HasConstraintName("FK__Notificat__Notif__3B40CD36");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.OrderBranch)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderBranchId)
                    .HasConstraintName("FK__Orders__OrderBra__3864608B");

                entity.HasOne(d => d.OrderClient)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderClientId)
                    .HasConstraintName("FK__Orders__OrderCli__4D5F7D71");

                entity.HasOne(d => d.OrderLevel)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderLevelId)
                    .HasConstraintName("FK_Orders_Levels");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductCargoAmount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ProductConfirm).HasMaxLength(30);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProductOrder)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductOrderId)
                    .HasConstraintName("FK_Products_Orders");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.Property(e => e.ShopLogo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShopName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShopUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserBirthDay).HasColumnType("date");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserFin)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UserFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserPhoto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .HasConstraintName("FK__Users__UserStatu__14270015");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
