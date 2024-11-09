using AFF_BE.Core.Constants.System;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Exceptions;
using AFF_BE.Core.Helpers;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Content.Brand;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Data.Repositories
{
    public class BrandRepository : RepositoryBase<Brand, int>, IBrandRepository
    {
        private readonly IMapper _mapper;

        public BrandRepository(AffContext dbContext,IMapper mapper,  IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
            _mapper = mapper;
        }

        public async Task<PagingResult<BrandDto>> GetBrands(PagingRequest request)
        {
           
            var query = _dbContext.Brands.AsQueryable();

            int total = await query.CountAsync();

            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = total;


            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else if (string.IsNullOrEmpty(request.OrderBy))
            {
                if (request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }
            else if (string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else
            {
                if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }

            query = query
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize);
            var data = await _mapper.ProjectTo<BrandDto>(query).ToListAsync();

            var result = new PagingResult<BrandDto>(data, request.PageIndex, request.PageSize, request.SortBy, request.OrderBy, total);

            return result;

        }

        public async Task CreateAsync(CreateBrandRequest bra)
        {

            bool isNameExist = await IsBrandNameExistAsync(bra.Name);

            if (isNameExist)
            {

                throw new Exception("Brand name already exist");
            }
            else
            {

                var newBrand = new Brand
                {
                    Code = bra.Code ?? StringHelper.GenerateRandomCode(8),
                    Name = bra.Name,
                    Description = bra.Description,
                    IsDeleted = bra.IsDeleted ?? false,
                    Version = bra.Version ?? 1,
                    CreatedAt = bra.CreatedAt ?? DateTime.Now
                };


                await CreateAsync(newBrand);
            }
        }
        public async Task<bool> IsBrandNameExistAsync(string name)
        {

            var existingBrand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Name == name);

            return existingBrand != null;
        }
        public async Task<bool> IsBrandNameExistAsyncUpdate(string name, int id)
        {

            var existingBrand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Name == name && b.Id != id);

            return existingBrand != null;
        }


        public async Task UpdateAsync(UpdateBrandRequest bra)
        {
            var existingBrand = await GetByIdAsync(bra.Id);
            if (existingBrand == null)
            {
                throw new NoDataException("The brand does not exist");
            }

            if (bra.Name != null && bra.Name != existingBrand.Name)
            {
                existingBrand.Name = bra.Name;
            }

            if (bra.Description != null)
            {
                existingBrand.Description = bra.Description;
            }
            if (bra.IsDeleted != null)
            {
                existingBrand.IsDeleted = bra.IsDeleted;
            }

            await UpdateAsync(existingBrand);
        }
        public async Task<BrandDto> GetOne(int id)
        {
            var brand = await _dbContext.Brands.FindAsync(id);

            if (brand == null)
            {

                return null;
            }

            return new BrandDto
            {
                Id = brand.Id,
                Code = brand.Code,
                Name = brand.Name,
                Description = brand.Description,
                IsDeleted = brand.IsDeleted,
                Version = brand.Version,
            };
        }
        public async Task UpdateAsyncStastus(int id, bool isDeleted)
        {
            var existingBrand = await GetByIdAsync(id);
            if (existingBrand == null)
            {
                throw new NoDataException("The brand does not exist");
            }

            existingBrand.IsDeleted = isDeleted;

            await UpdateAsync(existingBrand);
        }
        public async Task<List<BrandDto>> GetAllBrands()
        {
            var result = _dbContext.Brands
                .Where(obj => obj.IsDeleted == false)
                .Select(brand => new BrandDto
                {
                    Id = brand.Id,
                    Name = brand.Name,
                })
                .ToList();

            return result;
        }

        public async Task<PagingResult<BrandDto>> SearchBrands(PagingRequest request, string? keySearch)
        {
          
            var query = _dbContext.Brands.AsQueryable();

            if (!string.IsNullOrEmpty(keySearch))
            {
                query = query.Where(b => b.Name.Contains(keySearch) ||b.Description.Contains(keySearch));
            }


            int total = await query.CountAsync();

            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = total;


            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.OrderBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else if (string.IsNullOrEmpty(request.OrderBy))
            {
                if (request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }
            else if (string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else
            {
                if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }

            query = query
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize);


            var data = await _mapper.ProjectTo<BrandDto>(query).ToListAsync();




            var result = new PagingResult<BrandDto>(data, request.PageIndex, request.PageSize, request.SortBy, request.OrderBy, total);

            return result;

        }
    

        public string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string signChars = "ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ";
            string unsignChars = "aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYY";

            StringBuilder result = new StringBuilder();

            foreach (char c in text)
            {
                int index = signChars.IndexOf(c);
                if (index != -1)
                {
                    result.Append(unsignChars[index]);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString().Replace(" ", "-");
        }

    }
}
