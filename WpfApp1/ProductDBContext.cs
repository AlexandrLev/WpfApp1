using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WpfApp1
{
    public partial class ProductDBContext : DbContext
    {
        public ProductDBContext()
        {
        }

        public ProductDBContext(DbContextOptions<ProductDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreAccounting> StoreAccountings { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseAccounting> WarehouseAccountings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=desktop-vqnjpeo\\sqlexpress;Database=ProductDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_Products_ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Rnumber).HasColumnName("RNumber");

                entity.Property(e => e.Unit).HasMaxLength(10);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasIndex(e => e.StoreId, "IX_Stores_ID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Sname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("SName");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<StoreAccounting>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.StoreId });

                entity.ToTable("StoreAccounting");

                entity.HasIndex(e => e.ProductId, "IX_StoreAccounting_ProductID");

                entity.HasIndex(e => e.StoreId, "IX_StoreAccounting_StoreID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Cost).HasDefaultValueSql("((0))");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StoreAccountings)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_StoreAccounting_Products");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreAccountings)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_StoreAccounting_Stores");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.WhousesId);

                entity.HasIndex(e => e.WhousesId, "IX_Warehouses_ID");

                entity.Property(e => e.WhousesId).HasColumnName("WHousesID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.NameStorekeeper).HasMaxLength(40);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Wnumber).HasColumnName("WNumber");
            });

            modelBuilder.Entity<WarehouseAccounting>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.WhousesId });

                entity.ToTable("WarehouseAccounting");

                entity.HasIndex(e => e.ProductId, "IX_WarehouseAccounting_ProductID");

                entity.HasIndex(e => e.WhousesId, "IX_WarehouseAccounting_WHousesID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.WhousesId).HasColumnName("WHousesID");

                entity.Property(e => e.Cost).HasDefaultValueSql("((0))");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseAccountings)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_WarehouseAccounting_Products");

                entity.HasOne(d => d.Whouses)
                    .WithMany(p => p.WarehouseAccountings)
                    .HasForeignKey(d => d.WhousesId)
                    .HasConstraintName("FK_WarehouseAccounting_Warehouses");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
