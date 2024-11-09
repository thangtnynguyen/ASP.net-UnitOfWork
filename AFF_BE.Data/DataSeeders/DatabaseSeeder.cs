using AFF_BE.Core.Data.Identity;
using AFF_BE.Data.DataSeeders.Identity;
using Microsoft.EntityFrameworkCore;

namespace AFF_BE.Data.DataSeeders
{
    public static class DatabaseSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(RoleSeeder.Data());
            modelBuilder.Entity<User>().HasData(UserSeeder.Data());
            modelBuilder.Entity<UserRole>().HasData(UserRoleSeeder.Data());
        }
    }
}
