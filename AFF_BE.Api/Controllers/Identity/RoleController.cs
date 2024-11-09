using AFF_BE.Api.Attributes;
using AFF_BE.Api.Hubs;
using AFF_BE.Api.Services;
using AFF_BE.Api.Services.Interfaces;
using AFF_BE.Core.Constants.Identity;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Identity.Role;
using AFF_BE.Core.Models.Identity.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AFF_BE.Api.Controllers.Identity
{
    [Route("api/role")]
    [ApiController]
    //[HasPermission(PermissionConstant.ManageRole)]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _roleService;
        private readonly IHubContext<RefreshTokenHub> _hubContext;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IHubContext<RefreshTokenHub> hubContext, IMapper mapper)
        {
            _roleService = roleService;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        [HttpGet("paging")]
        //[HasPermission(PermissionConstant.ManageRoleView)]
        public async Task<ApiResult<PagingResult<RoleDto>>> Get([FromQuery] GetRoleRequest request)
        {
            var result = await _roleService.GetPaging(request);

            return new ApiResult<PagingResult<RoleDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh vai trò thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-id")]
        //[HasPermission(PermissionConstant.ManageRoleView)]
        public async Task<ApiResult<RoleDto>> GetById([FromQuery] EntityIdentityRequest<int> request)
        {
            var result = await _roleService.GetById(request);

            return new ApiResult<RoleDto>()
            {
                Status = true,
                Message = "Lấy thông tin  vai trò thành công!",
                Data = result
            };
        }

        [HttpGet("user-have-role")]
        //[HasPermission(PermissionConstant.ManageRoleView)]
        public async Task<ApiResult<List<UserDto>>> GetUserInRole([FromQuery] EntityIdentityRequest<int> request)
        {
            var result = await _roleService.GetUsersInRoleByIdAsync(request.Id);
            var userInRoleDto = _mapper.Map<List<UserDto>>(result);

            return new ApiResult<List<UserDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh vai trò thành công!",
                Data = userInRoleDto
            };
        }

        [HttpPost("create")]
        //[HasPermission(PermissionConstant.ManageRoleCreate)]
        public async Task<ApiResult<RoleDto>> Create([FromBody] CreateRoleRequest request)
        {
            var result = await _roleService.Create(request);

            return new ApiResult<RoleDto>()
            {
                Status = true,
                Message = "Thêm vai trò thành công!",
                Data = result
            };
        }

        [HttpPut("edit")]
        //[HasPermission(PermissionConstant.ManageRoleEdit)]
        public async Task<ApiResult<RoleDto>> Edit([FromBody] EditRoleRequest request)
        {
            var result = await _roleService.Edit(request);

            await _hubContext.Clients.All.SendAsync("RefreshTokenByRole", result);

            return new ApiResult<RoleDto>()
            {
                Status = true,
                Message = "Cập nhập thành công!",
                Data = result
            };
        }

        [HttpPut("delete")]
        //[HasPermission(PermissionConstant.ManageRoleDelete)]
        public async Task<ApiResult<RoleDto>> Delete([FromBody] EntityIdentityRequest<int> request)
        {
            var result = await _roleService.Delete(request.Id);

            return new ApiResult<RoleDto>()
            {
                Status = true,
                Message = "Đã xóa!",
                Data = result
            };
        }

        [HttpPut("delete-multiple")]
        //[HasPermission(PermissionConstant.ManageRoleDelete)]
        public async Task<ApiResult<List<RoleDto>>> DeleteMultiple([FromBody] ListEntityIdentityRequest<int?> request)
        {
            var result = await _roleService.DeleteMultiple(request.Ids);

            return new ApiResult<List<RoleDto>>()
            {
                Status = true,
                Message = "Đã xóa các role",
                Data = result
            };
        }


    }
}
