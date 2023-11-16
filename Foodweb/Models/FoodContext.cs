using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Foodweb.Models;

public partial class FoodContext : DbContext
{
    public FoodContext()
    {
    }

    public FoodContext(DbContextOptions<FoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=food");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(250);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Account_Roles");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId).HasName("PK_ProductCategory");

            entity.ToTable("Category");

            entity.Property(e => e.CateId).HasColumnName("CateID");
            entity.Property(e => e.CateName).HasMaxLength(250);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetaDesc).HasMaxLength(250);
            entity.Property(e => e.Metakeyword).HasMaxLength(250);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CusId);

            entity.Property(e => e.CusId).HasColumnName("CusID");
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.FullName).HasMaxLength(250);
            entity.Property(e => e.LocaId).HasColumnName("LocaID");
            entity.Property(e => e.Phone).HasMaxLength(12);

            entity.HasOne(d => d.Loca).WithMany(p => p.Customers)
                .HasForeignKey(d => d.LocaId)
                .HasConstraintName("FK_Customers_Locations");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocaId);

            entity.Property(e => e.LocaId).HasColumnName("LocaID");
            entity.Property(e => e.LocaName).HasMaxLength(250);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CusId).HasColumnName("CusID");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.ShipDate).HasColumnType("datetime");

            entity.HasOne(d => d.Cus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK_Orders_Customers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId)
                .ValueGeneratedOnAdd()
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProId).HasColumnName("ProID");
            entity.Property(e => e.ProName).HasMaxLength(250);

            entity.HasOne(d => d.OrderDetailNavigation).WithOne(p => p.OrderDetail)
                .HasForeignKey<OrderDetail>(d => d.OrderDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Orders");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.ToTable("Page");

            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MetaDesc).HasMaxLength(250);
            entity.Property(e => e.MetaKey).HasMaxLength(250);
            entity.Property(e => e.PageName).HasMaxLength(250);
            entity.Property(e => e.Thumb).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(250);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CateId).HasColumnName("CateID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MetaDesc).HasMaxLength(250);
            entity.Property(e => e.MetaKey).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(250);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProId);

            entity.ToTable("Product");

            entity.Property(e => e.ProId)
                .ValueGeneratedNever()
                .HasColumnName("ProID");
            entity.Property(e => e.CateId).HasColumnName("CateID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MetaDesc).HasMaxLength(250);
            entity.Property(e => e.MetaKey).HasMaxLength(250);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProName).HasMaxLength(250);
            entity.Property(e => e.ShortDesc).HasMaxLength(250);
            entity.Property(e => e.Thumb).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Cate).WithMany(p => p.Products)
                .HasForeignKey(d => d.CateId)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(250);
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.ShipId);

            entity.Property(e => e.ShipId).HasColumnName("ShipID");
            entity.Property(e => e.Company).HasMaxLength(250);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.ShipDate).HasColumnType("datetime");
            entity.Property(e => e.ShipName).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
