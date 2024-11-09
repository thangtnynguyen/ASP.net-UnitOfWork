using AFF_BE.Core.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Data.DataSeeders.Identity
{
    public class RoleSeeder
    {
        public static List<Role> Data()
        {
            var roles = new List<Role>()
            {
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "admin",
                    Description = "Admin Role"
                },
                new Role
                {
                    Id = 2,
                    Name = "Master",
                    NormalizedName = "master",
                    Description = "Master Role"
                },
                new Role
                {
                    Id = 3,
                    Name = "Employee",
                    NormalizedName = "employee",
                    Description = "Employee Role"
                },
                new Role
                {
                    Id = 4,
                    Name = "Customer",
                    NormalizedName = "customer",
                    Description = "Customer Role"
                },
            };

            return roles;
        }
    }
}
