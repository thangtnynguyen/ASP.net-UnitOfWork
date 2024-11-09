using AFF_BE.Core.Data.Identity;

namespace AFF_BE.Data.DataSeeders.Identity
{
    public class UserRoleSeeder
    {
        public static List<UserRole> Data()
        {
            var userRoles = new List<UserRole>()
            {
                new UserRole
                {
                     UserId = 1,
                     RoleId = 1
                },
                new UserRole
                {
                     UserId = 1,
                     RoleId = 2
                }
            };

            return userRoles;
        }
    }
}
