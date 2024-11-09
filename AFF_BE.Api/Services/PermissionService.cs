using AFF_BE.Api.Services.Interfaces;
using AFF_BE.Core.Constants;
using AFF_BE.Core.Constants.System;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Exceptions;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Identity.Permission;
using AFF_BE.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AFF_BE.Api.Services
{
    public class PermissionService: IPermissionService
    {
        private readonly AffContext _dbContext;
        private readonly IMapper _mapper;


        public PermissionService(AffContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        private List<PermissionDto> GetChildren(int parentId)
        {
            var children = _dbContext.Permissions
                .Where(p => p.ParentPermissionId == parentId)
                .ToList();

            var childDtos = new List<PermissionDto>();

            foreach (var childPermission in children)
            {
                var childDto = _mapper.Map<PermissionDto>(childPermission);
                childDto.Childrens = GetChildren(childPermission.Id);
                childDtos.Add(childDto);
            }

            return childDtos;
        }


        public async Task<PagingResult<PermissionDto>> GetPaging(GetPermissionRequest request)
        {
            try
            {
                var permissions = await GetRecursive(null);

                var total = permissions.Count;

                if (request.PageIndex == null) request.PageIndex = 1;
                if (request.PageSize == null) request.PageSize = total;

                int totalPages = (int)Math.Ceiling((double)total / request.PageSize);

                if (string.IsNullOrEmpty(request.SortBy) || request.SortBy == SortByConstant.Desc)
                {
                    permissions = request.OrderBy switch
                    {
                        OrderByConstant.Id or _ => permissions.OrderByDescending(p => p.Id).ToList(),
                    };
                }
                else if (request.SortBy == SortByConstant.Asc)
                {
                    permissions = request.OrderBy switch
                    {
                        OrderByConstant.Id or _ => permissions.OrderBy(p => p.Id).ToList(),
                    };
                }

                var items = permissions
                    .Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var result = new PagingResult<PermissionDto>(items, request.PageIndex, request.PageSize, request.SortBy, request.OrderBy, total);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }

        private async Task<List<PermissionDto>> GetRecursive(int? parentPermissionId)
        {
            var children = _dbContext.Permissions
                 .Where(p => p.ParentPermissionId == parentPermissionId)
                 .ToList();

            var childDtos = new List<PermissionDto>();

            foreach (var childPermission in children)
            {
                var childDto = _mapper.Map<PermissionDto>(childPermission);
                childDto.Childrens = GetChildren(childPermission.Id);
                childDtos.Add(childDto);
            }

            return childDtos;
        }


        public async Task<List<PermissionDto>> GetByRoleId(int roleId)
        {
            try
            {
                var rolePermissions = await _dbContext.RolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .Include(rp => rp.Permission)
                    .ToListAsync();

                var permissions = rolePermissions.Select(rp => rp.Permission);

                var result = await GetRecursive(null, permissions);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCodeConstant.InternalServerError, ex);
            }
        }

        private async Task<List<PermissionDto>> GetRecursive(int? parentPermissionId, IEnumerable<Permission> allPermissions)
        {
            var children = allPermissions
                .Where(p => p.ParentPermissionId == parentPermissionId)
                .ToList();

            var childDtos = new List<PermissionDto>();

            foreach (var childPermission in children)
            {
                var childDto = _mapper.Map<PermissionDto>(childPermission);
                childDto.Childrens = await GetRecursive(childPermission.Id, allPermissions);
                childDtos.Add(childDto);
            }

            return childDtos;
        }


        public async Task<PermissionDto> Create(CreatePermissionRequest request)
        {
            var permission = _mapper.Map<Permission>(request);
            permission.CreatedAt = DateTime.Now;

            await _dbContext.Permissions.AddAsync(permission);
            await _dbContext.SaveChangesAsync();

            var permissionDto = _mapper.Map<PermissionDto>(permission);

            return permissionDto;

        }


    }
}
