using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Identity.User;

namespace AFF_BE.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(EntityIdentityRequest<int> request);

        Task<UserDto> GetUserInfo(HttpContext httpContext);

        Task<UserDto> GetUserInfoAsync();

        Task<List<string>> GetRoleByUserAsync(User user);

        Task<List<string>> GetPermissionByUserAsync(User user);

        Task<User> EditUserInfo(EditUserInfoRequest request);

        Task<UserDto> AssignUserToRolesAsync(AssignUserToRoleRequest request);

        Task<ConfirmEmailResult> VerifyEmailWithOtp(string? email, string otp);

        Task<User> Create(CreateUserRequest request);



    }
}
