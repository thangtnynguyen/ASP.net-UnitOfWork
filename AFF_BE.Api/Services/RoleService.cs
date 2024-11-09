using AFF_BE.Api.Services.Interfaces;
using AFF_BE.Core.Constants;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Exceptions;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Identity.Role;
using AFF_BE.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AFF_BE.Api.Services
{
    public class RoleService:IRoleService
    {
        private readonly AffContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly PermissionService _permissionService;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;


        public RoleService(AffContext dbContext, IMapper mapper, IUserService userService, PermissionService permissionService, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _permissionService = permissionService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<PagingResult<RoleDto>> GetPaging(GetRoleRequest request)
        {
            try
            {
                var query = _dbContext.Roles.Where(x => x.DeletedAt == null).AsQueryable();

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(b => b.Name.ToLower().Contains(request.Name.ToLower()));
                }

                if (!string.IsNullOrEmpty(request.Description))
                {
                    query = query.Where(b => b.Description.ToLower().Contains(request.Description.ToLower()));
                }

                int total = await query.CountAsync();

                if (request.PageIndex == null) request.PageIndex = 1;
                if (request.PageSize == null) request.PageSize = total;

                int totalPages = (int)Math.Ceiling((double)total / request.PageSize);

                var roles = await query
                    .OrderByDescending(b => b.Id)
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Include(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
                    .ToListAsync();

                var roleDtos = _mapper.Map<List<RoleDto>>(roles);

                foreach (var roleDto in roleDtos)
                {
                    roleDto.Permissions = await _permissionService.GetByRoleId(roleDto.Id);
                }

                var result = new PagingResult<RoleDto>(roleDtos, request.PageIndex, request.PageSize, total);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }

        }

        public async Task<RoleDto> GetById(EntityIdentityRequest<int> request)
        {
            try
            {
                var query = _dbContext.Roles.Where(x => x.DeletedAt == null && x.Id == request.Id).AsQueryable();

                var role = await query
                    .OrderByDescending(b => b.Id)
                    .Include(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
                    .FirstOrDefaultAsync();

                var roleDto = _mapper.Map<RoleDto>(role);

                roleDto.Permissions = await _permissionService.GetByRoleId(roleDto.Id);


                return roleDto;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }

        }

        public async Task<PagingResult<RoleDto>> GetByUser(GetRoleByUserRequest request)
        {
            try
            {
                var query = _dbContext.Roles
                    .Where(x => x.DeletedAt == null && _dbContext.UserRoles.Any(ur => ur.UserId == request.UserId && ur.RoleId == x.Id))
                    .AsQueryable();


                int total = await query.CountAsync();

                if (request.PageIndex == null) request.PageIndex = 1;
                if (request.PageSize == null) request.PageSize = total;

                int totalPages = (int)Math.Ceiling((double)total / request.PageSize);

                var roles = await query
                    .OrderByDescending(b => b.Id)
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Include(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
                    .ToListAsync();

                var roleDtos = _mapper.Map<List<RoleDto>>(roles);

                foreach (var roleDto in roleDtos)
                {
                    roleDto.Permissions = await _permissionService.GetByRoleId(roleDto.Id);
                }

                var result = new PagingResult<RoleDto>(roleDtos, request.PageIndex, request.PageSize, total);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }

        public async Task<RoleDto> Create(CreateRoleRequest request)
        {
            try
            {
                request.NormalizedName = request.Name?.Replace(" ", "").ToLower();
                var role = _mapper.Map<Role>(request);

                var userCurrent = await _userService.GetUserInfoAsync();
                role.CreatedAt = DateTime.Now;
                role.CreatedBy = userCurrent?.Id;


                await _dbContext.Roles.AddAsync(role);
                await _dbContext.SaveChangesAsync();

                if (request.PermissionIds != null && request.PermissionIds.Any())
                {
                    foreach (var permissionId in request.PermissionIds)
                    {
                        var rolePermission = new RolePermission
                        {
                            RoleId = role.Id,
                            PermissionId = permissionId
                        };

                        await _dbContext.RolePermissions.AddAsync(rolePermission);
                    }

                    await _dbContext.SaveChangesAsync();
                }
                var roleDto = _mapper.Map<RoleDto>(role);

                return roleDto;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }
      
        public async Task<RoleDto> Edit(EditRoleRequest request)
        {
            try
            {
                request.NormalizedName = request.Name?.Replace(" ", "").ToLower();
                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == request.Id);


                if (role == null)
                {
                    throw new ApiException("Không tìm thấy quyền hợp lệ!", HttpStatusCodeConstant.InternalServerError);
                }

                _mapper.Map(request, role);

                var userCurrent = await _userService.GetUserInfoAsync();
                role.UpdatedAt = DateTime.Now;
                role.UpdatedBy = userCurrent?.Id;

                var existingRolePermissions = _dbContext.RolePermissions.Where(rp => rp.RoleId == role.Id);
                _dbContext.RolePermissions.RemoveRange(existingRolePermissions);

                if (request.PermissionIds != null && request.PermissionIds.Count > 0)
                {
                    foreach (var permissionId in request.PermissionIds)
                    {
                        var rolePermission = new RolePermission
                        {
                            RoleId = role.Id,
                            PermissionId = permissionId
                        };
                        await _dbContext.RolePermissions.AddAsync(rolePermission);
                    }
                }


                var usersInRole = await this.GetUsersInRoleByIdAsync(role.Id);

                if (usersInRole != null && usersInRole.Any())
                {

                    foreach (var user in usersInRole)
                    {
                        user.IsRefreshToken = true;

                        var result = await _userManager.UpdateAsync(user);

                        if (!result.Succeeded)
                        {
                            throw new Exception($"Failed to update user: {user.UserName}");
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();

                var roleDto = _mapper.Map<RoleDto>(role);

                return roleDto;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }

        public async Task<RoleDto> Delete(int id)
        {
            try
            {
                var role = await _dbContext.Roles.FindAsync(id);

                if (role == null)
                {
                    throw new ApiException("Không tìm quyền hợp lệ!", HttpStatusCodeConstant.InternalServerError);
                }

                var userCurrent = await _userService.GetUserInfoAsync();

                role.DeletedAt = DateTime.Now;
                role.CreatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                var roleDto = _mapper.Map<RoleDto>(role);

                return roleDto;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }

        public async Task<List<RoleDto>> DeleteMultiple(List<int?> ids)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var roles = await _dbContext.Roles
                                                   .Where(s => ids.Contains(s.Id) && s.DeletedAt == null)
                                                   .ToListAsync();

                    if (!roles.Any())
                    {
                        throw new ApiException("Không tìm thấy quyền nào hợp lệ để xoá.", HttpStatusCodeConstant.BadRequest);
                    }

                    foreach (var role in roles)
                    {
                        role.DeletedAt = DateTime.Now;
                    }

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var roleDtos = _mapper.Map<List<RoleDto>>(roles);

                    return roleDtos;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ApiException($"Lỗi khi xoá các quyền: {ex.Message}", HttpStatusCodeConstant.InternalServerError, ex);
                }
            }
        }

        public async Task<List<User>> GetUsersInRoleByIdAsync(int roleId)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            var userIdsInRole = await _dbContext.UserRoles
                                                .Where(ur => ur.RoleId == roleId)
                                                .Select(ur => ur.UserId)
                                                .ToListAsync();

            var usersInRole = await _userManager.Users
                                                .Where(u => userIdsInRole.Contains(u.Id))
                                                .ToListAsync();

            return usersInRole;
        }


    }
}
