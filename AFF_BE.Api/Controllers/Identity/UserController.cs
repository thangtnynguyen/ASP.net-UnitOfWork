using AFF_BE.Api.Attributes;
using AFF_BE.Api.Extension;
using AFF_BE.Api.Hubs;
using AFF_BE.Api.Services;
using AFF_BE.Api.Services.Interfaces;
using AFF_BE.Core.Constants.Identity;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Commission;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Identity.Role;
using AFF_BE.Core.Models.Identity.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AFF_BE.Api.Controllers.Identity
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IHubContext<RefreshTokenHub> _hubContext;
        private readonly IUnitOfWork _unitOfWork;



        public UserController(UserManager<User> userManager,IUnitOfWork unitOfWork, IUserService userService, IMapper mapper, IRoleService roleService, IHubContext<RefreshTokenHub> hubContext)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        [HttpPost("get-commission")]
        public async Task<ApiResult<GetCommissionByUserDto>> GetCommissionByUser()
        {
            var userId = User.GetUserId();
            var userInfo = await _unitOfWork.Commissions.GetCommissionByUserId(userId);
            return new ApiResult<GetCommissionByUserDto>()
            {
                Status = true,
                Message = "Lấy danh sách người dùng thành công",
                Data = userInfo
            };
        }

        [HttpGet("paging")]
        //[HasPermission(PermissionConstant.ManageUserView)]
        public async Task<ApiResult<PagingResult<UserDto>>> GetPaging([FromQuery] GetUserRequest request)
        {
            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.Contains(request.PhoneNumber));
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(x => x.Email.Contains(request.Email));
            }
            if (!string.IsNullOrEmpty(request.Address))
            {
                query = query.Where(x => x.Address.Contains(request.Address));
            }
            if (request.IsDeleted.HasValue)
            {
                query = query.Where(x => x.IsDeleted == request.IsDeleted);
            }

            var totalRow = await query.CountAsync();


            query = query.Skip((request.PageIndex - 1) * request.PageSize)
               .Take(request.PageSize);

            var users = await _mapper.ProjectTo<UserDto>(query).ToListAsync();


            return new ApiResult<PagingResult<UserDto>>()
            {
                Status = true,
                Message = "Lấy danh sách người dùng thành công",
                Data = new PagingResult<UserDto>(users, request.PageIndex, request.PageSize, totalRow)
            };


        }

        [HttpGet("paging-info")]
        public async Task<ApiResult<PagingResult<UserDto>>> GetPagingInfo([FromQuery] GetUserRequest request)
        {
            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.Contains(request.PhoneNumber));
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(x => x.Email.Contains(request.Email));
            }
            if (!string.IsNullOrEmpty(request.Address))
            {
                query = query.Where(x => x.Address.Contains(request.Address));
            }
            if (request.IsDeleted.HasValue)
            {
                query = query.Where(x => x.IsDeleted == request.IsDeleted);
            }

            var totalRow = await query.CountAsync();


            query = query.Skip((request.PageIndex - 1) * request.PageSize)
               .Take(request.PageSize);

            var users = await _mapper.ProjectTo<UserDto>(query).ToListAsync();


            return new ApiResult<PagingResult<UserDto>>()
            {
                Status = true,
                Message = "Lấy danh sách người dùng thành công",
                Data = new PagingResult<UserDto>(users, request.PageIndex, request.PageSize, totalRow)
            };
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<UserDto>> GetById([FromQuery] GetRoleByUserRequest request)
        {
            var id = new EntityIdentityRequest<int>()
            {
                Id=request.UserId,
            };
            var user = await _userService.GetById(id);
            var roles = await _roleService.GetByUser(request);
            if (user == null)
            {

                return new ApiResult<UserDto>
                {
                    Status = false,
                    Message = "Không có user có id như vậy",
                    Data = null
                };
            }
            user.Roles = roles.Items;

            return new ApiResult<UserDto>
            {
                Status = true,
                Message = "Thành công",
                Data = user
            };

        }

        [HttpGet("user-info")]
        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            var user = await _userService.GetUserInfo(HttpContext);

            if (user == null)
            {
                return new ApiResult<UserDto>()
                {
                    Status = false,
                    Message = "Không tìm thấy thông tin người dùng hợp lệ!",
                    Data = null
                };
            }

            return new ApiResult<UserDto>()
            {
                Status = true,
                Message = "Lấy thông tin người dùng thành công!",
                Data = user
            };
        }

        [HttpGet("user-info-async")]
        public async Task<ApiResult<UserDto>> GetCurrentUserAsync()
        {
            var user = await _userService.GetUserInfoAsync();

            if (user == null)
            {
                return new ApiResult<UserDto>()
                {
                    Status = false,
                    Message = "Không tìm thấy thông tin người dùng hợp lệ!",
                    Data = null
                };
            }

            return new ApiResult<UserDto>()
            {
                Status = true,
                Message = "Lấy thông tin người dùng thành công!",
                Data = user
            };
        }

        [HttpPost("create")]
        //[HasPermission(PermissionConstant.ManageUserCreate)]
        public async Task<ApiResult<UserDto>> Create([FromBody] CreateUserRequest request)
        {
            var user = await _userService.Create(request);

            if (user == null)
            {
                return new ApiResult<UserDto>()
                {
                    Status = true,
                    Message = "Tạo người dùng thất bại!",
                    Data = null
                };
            }
            var userDto = _mapper.Map<UserDto>(user);

            return new ApiResult<UserDto>()
            {
                Status = true,
                Message = "Tạo người dùng thành công!",
                Data = userDto
            };
        }

        [HttpPost("set-password")]
        public async Task<ApiResult<bool>> SetPassword([FromBody] SetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = "Tài khoản không tồn tại trong hệ thống",
                    Data = false
                };
            }

            var success = await _userService.VerifyEmailWithOtp(request.Email, request.Otp);
            if (success.Success == false)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = "Mã OTP không hợp lệ hoặc đã hết hạn",
                    Data = false
                };
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = string.Join("<br>", result.Errors.Select(x => x.Description)),
                    Data = false
                };
            }

            return new ApiResult<bool>
            {
                Status = true,
                Message = "Tạo mới mật khẩu thành công",
                Data = true
            };
        }

        [HttpPut("edit-user-info")]
        public async Task<ApiResult<UserDto>> EditUserInfo([FromBody] EditUserInfoRequest request)
        {
            var result = await _userService.EditUserInfo(request);
            var userDto = _mapper.Map<UserDto>(result);

            return new ApiResult<UserDto>()
            {
                Status = true,
                Message = "Cập nhật thông tin người dùng thành công!",
                Data = userDto
            };
        }

        [HttpPut("assign-user-to-roles")]
        //[HasPermission(PermissionConstant.ManageUserEdit)]
        public async Task<ApiResult<UserDto>> AssignUserToRolesAsync([FromBody] AssignUserToRoleRequest request)
        {
            var user = await _userService.AssignUserToRolesAsync(request);

            await _hubContext.Clients.All.SendAsync("RefreshToken", user);

            return new ApiResult<UserDto>()
            {
                Status = true,
                Message = "Thành công",
                Data = user
            };
        }

        


    }
}
