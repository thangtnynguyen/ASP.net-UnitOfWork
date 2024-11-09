using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using AFF_BE.Core.Constants.System;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Data.Content;
using System.Reflection.Emit;
using AFF_BE.Core.Models.Payment;
using AFF_BE.Data.DataSeeders;
using AFF_BE.Core.Data.Tree;
using AFF_BE.Core.Data.Commission;
using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.Data.Address;

namespace AFF_BE.Data
{
    public class AffContext : IdentityDbContext<User, Role, int>
    {
        public AffContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Banner> Banners { get; set; } = null!;

        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;

        public virtual DbSet<Product> Products { get; set; } = null!;

        public virtual DbSet<ProductVariant> ProductVariants { get; set; } = null!;

        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;

        public virtual DbSet<Brand> Brands { get; set; } = null!;

        public virtual DbSet<News> News { get; set; } = null !;

        //tree
        public DbSet<TreeNode> TreeNodes { get; set; }

        public DbSet<TreePosition> TreePositions { get; set; }

        //order
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<PaymentAccount> PaymentAccounts { get; set; } = null!;

        //comision
        public DbSet<DirectCommission> DirectCommissions { get; set; } = null!;
        public DbSet<IndirectCommission> IndirectCommissions { get; set; } = null!;

        //address
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;




        //identity

        public virtual DbSet<Permission>? Permissions { get; set; }
        public virtual DbSet<RolePermission>? RolePermissions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //identity

            builder.Entity<User>().ToTable("Users");

            builder.Entity<Role>().ToTable("Roles");

            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims")
                .HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens")
               .HasKey(x => new { x.UserId });


            //tree

            builder.Entity<TreeNode>()
           .HasOne(node => node.LeftChild)
           .WithMany()
           .HasForeignKey(node => node.LeftChildId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TreeNode>()
                .HasOne(node => node.RightChild)
                .WithMany()
                .HasForeignKey(node => node.RightChildId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<IndirectCommission>()
                .HasOne(ic => ic.UserRecive)
                .WithMany(u => u.IndirectCommissions)  // Đảm bảo User có một ICollection<IndirectCommission>
                .HasForeignKey(ic => ic.UserReciveId);

            #region ignore
            //builder.Entity<TreeNode>()
            //.HasOne(u => u.LeftChild)
            //.WithOne()
            //.HasForeignKey<TreeNode>(u => u.LeftChildId);

            //builder.Entity<TreeNode>()
            //    .HasOne(u => u.RightChild)
            //    .WithOne()
            //    .HasForeignKey<TreeNode>(u => u.RightChildId);

            //builder.Entity<TreeNode>()
            //    .HasOne(u => u.Parent)
            //    .WithMany()
            //    .HasForeignKey(u => u.ParentId);
            #endregion


            //seeder 
            builder.Seed(); 
            
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added);

            foreach (var entityEntry in entries)
            {
                var dateCreatedProp = entityEntry.Entity.GetType().GetProperty(SystemConstant.DateCreatedField);
                if (entityEntry.State == EntityState.Added
                    && dateCreatedProp != null)
                {
                    dateCreatedProp.SetValue(entityEntry.Entity, DateTime.Now);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}