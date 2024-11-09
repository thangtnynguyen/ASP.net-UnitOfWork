using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Exceptions;
using AFF_BE.Core.Helpers;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Brand;
using AFF_BE.Core.Models.Content.ProductCategory;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Data.Repositories
{
    public class ProductCategoryRepostory : RepositoryBase<ProductCategory, int>, IProductCategoryRepository
    {
        private readonly IMapper _mapper;

        public ProductCategoryRepostory(AffContext dbContext, IMapper mapper, IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
            _mapper = mapper;
        }
        private async Task<ProductCategory> GetParentAsync(int parentId)
        {
            ProductCategory? parent = await GetByIdAsync(parentId);
            if (parent == null)
            {
                return null;
            }
            else if (parent.IsDeleted == true)
            {
                throw new ArgumentException("This parent has been deleted");
            }
            else if (parent.Level == 5)
            {
                throw new ArgumentException("This parent's level is already equal to 5, which cannot be set as a parent");
            }
            return parent;
        }
        private List<ProductCategoryAndChildDto> BuildTreeProductCategory(List<ProductCategory> categories)
        {
            List<ProductCategoryAndChildDto> result = new List<ProductCategoryAndChildDto>();
            Dictionary<int, ProductCategoryAndChildDto> hashParent = new Dictionary<int, ProductCategoryAndChildDto>();

            for (int i = 0; i < categories.Count;)
            {
                if (categories[0].Level == 1)
                {
                    ProductCategoryAndChildDto productCategoryAndChild = _mapper.Map<ProductCategoryAndChildDto>(categories[0]);
                    hashParent.Add(categories[0].Id, productCategoryAndChild);
                    result.Add(productCategoryAndChild);
                    categories.Remove(categories[0]);
                }
                else { break; }
            }
            Dictionary<int, ProductCategoryAndChildDto> hashChild = new Dictionary<int, ProductCategoryAndChildDto>();
            for (int i = 0; i < categories.Count - 1;)
            {
                if (categories[0].Level == categories[1].Level)
                {
                    ProductCategoryAndChildDto child = _mapper.Map<ProductCategoryAndChildDto>(categories[0]);
                    hashChild.Add(categories[0].Id, child);
                    if (hashParent.TryGetValue(categories[0].ParentId ?? 0, out ProductCategoryAndChildDto parent))
                    {
                        parent.Children.Add(child);
                    }
                    categories.Remove(categories[0]);
                }
                else
                {
                    ProductCategoryAndChildDto child = _mapper.Map<ProductCategoryAndChildDto>(categories[0]);
                    hashChild.Add(categories[0].Id, child);
                    if (hashParent.TryGetValue(categories[0].ParentId ?? 0, out ProductCategoryAndChildDto parentI))
                    {
                        parentI.Children.Add(child);
                    }

                    hashParent = hashChild;
                    categories.Remove(categories[0]);
                    hashChild = new Dictionary<int, ProductCategoryAndChildDto>();
                }

            }
            if (categories.Count != 0)
            {
                ProductCategoryAndChildDto childlast = _mapper.Map<ProductCategoryAndChildDto>(categories[0]);
                if (hashParent.TryGetValue(categories[0].ParentId ?? 0, out ProductCategoryAndChildDto parentIplus1))
                {
                    parentIplus1.Children.Add(childlast);
                    categories.Remove(categories[0]);
                }
            }
            return result;
        }
        public async Task<List<ProductCategoryAndChildDto>> GetProductCatgoryAndChild(HttpContext httpContext)
        {
            var list = await _dbContext.ProductCategories
                .Where(pc => pc.IsDeleted != true)
                .OrderBy(pc => pc.Level)
                .ToListAsync();
            var result = BuildTreeProductCategory(list);
            return result;
        }
        public async Task UpdateAsync(UpdateProductCategoryRequest updateCate)
        {
            // Check
            ProductCategory? isExist = await GetByIdAsync(updateCate.Id);
            if (isExist == null) { throw new NoDataException("The product does not exist"); }
            if (updateCate.Name != isExist.Name)
            {
                ProductCategory isExistName = await IsRecordWithNameExistsAsync(updateCate.Name);
                if (isExistName != null)
                {
                    throw new ForeignKeyConstraintException($"The Product category name : '{updateCate.Name}' already exist");
                }
                RepositorySingleton.Instance.ExecuteQueryAsync($"update products set CategoryName = {updateCate.Name} where CategoryId = {updateCate.Id}");
            }
            List<ProductCategory> children = await SearchChildBranches(isExist);
            ProductCategory? parent = await GetParentAsync(updateCate.ParentId ?? -1);
            if (children.Contains(parent)) { throw new ArgumentException($"{parent.Name} is child of {isExist.Name}"); }

            // Begin
            ProductCategory update = _mapper.Map<ProductCategory>(updateCate);
           
            int? parentLevel = parent == null ? 0 : parent.Level;
            update.ParentId = parent == null ? 0 : parent.Id;
            update.Level = parentLevel + 1;
            update.ParentName = parent == null ? "" : parent.Name;
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].ParentId == update.Id)
                {
                    children[i].ParentName = update.Name;
                }
                children[i].Level -= (isExist.Level - parentLevel - 1);
                if (children[i].Level > 5) { throw new ForeignKeyConstraintException("Level of child not more than 5"); }

            }
            await UpdateAsync(update);
            // End
        }
        public async Task<PagingResult<ProductCategoryDto>> GetProductCategorysAsync(PagingRequest paging)
        {
          
            var query = _dbContext.ProductCategories.Where(c => c.IsDeleted != true)
             .OrderByDescending(p => p.UpdatedAt);

            int total = await query.CountAsync();

            var categories = await query
                .Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();
            List<ProductCategoryDto> data = _mapper.Map<List<ProductCategoryDto>>(categories);

            var result = new PagingResult<ProductCategoryDto>(data, paging.PageIndex, paging.PageSize, paging.SortBy, paging.OrderBy, total);

            return result;
        }
        public async Task<bool> IsExistRecordUpdate()
        {
            return true;
        }
        public async Task<List<ProductCategoryDto>> GetProductCategorysAsync()
        {
            var query = _dbContext.ProductCategories.Where(c => c.IsDeleted != true)
             .OrderByDescending(p => p.UpdatedAt);
            var categories = await query
                .ToListAsync();
            List<ProductCategoryDto> result = _mapper.Map<List<ProductCategoryDto>>(categories); ;
            return result;
        }
        private async Task<List<ProductCategory>> SearchChildBranches(ProductCategory? child)
        {
            if (child == null) { return new List<ProductCategory>(); }
            if (child.IsDeleted == true) { return new List<ProductCategory>(); }
            List<ProductCategory> cates = await _dbContext.ProductCategories.Where(p => p.ParentId == child.Id && p.IsDeleted != true)
             .OrderByDescending(p => p.UpdatedAt)
            .ToListAsync();
            int count = cates.Count;
            for (int i = 0; i < count; i++)
            {
                cates.AddRange(await SearchChildBranches(cates[i]));
            }
            return cates;
        }
        public async Task<List<ProductCategoryDto>> FilterWithNameOrId(string name_or_id)
        {
            int id = int.TryParse(name_or_id, out int convert) ? convert : -1;
            string query = $"SELECT * FROM ProductCategories WHERE (Id = {id} or Name = N'{name_or_id}') and IsDeleted = 0";
            List<ProductCategory> result = await _dbContext.ProductCategories
                .FromSqlRaw(query).ToListAsync();
            return _mapper.Map<List<ProductCategoryDto>>(result);
        }
        public async Task<ProductCategory> IsRecordWithNameExistsAsync(string name)
        {
            name = StringHelper.CapitalizeFirstLetter(name ?? "");
            ProductCategory? checkName = await _dbContext.ProductCategories
             .OrderByDescending(p => p.UpdatedAt)
                .Where(productCate => productCate.Name == name && productCate.IsDeleted != true)
                .FirstOrDefaultAsync();
            return checkName;
        }
        public async Task DeleteAsync(int id)
        {
            ProductCategory? cate = await _dbContext.ProductCategories.Where(p => p.Id == id && p.IsDeleted != true).FirstOrDefaultAsync();
            if (cate == null) { throw new NoDataException("The product does not exist"); }
            cate.IsDeleted = true;
            List<ProductCategory> childs = await SearchChildBranches(cate);
            foreach (var item in childs)
            {
                item.IsDeleted = true;
            }
            await SaveChangesAsync();
        }
        public async Task CreateMultiAsync(CreateMultiProductCategoryRequest cate)
        {
          
        }
        public async Task CreateAsync(CreateProductCategoryRequest cate)
        {
           
            ProductCategory? _parent = (await GetByIdAsync(cate.ParentId ?? 0)) ?? new ProductCategory();
            if (_parent.IsDeleted == true)
            {
                throw new ArgumentException("This parent has been deleted");
            }
            if (_parent.Level == 5)
            {
                throw new ArgumentException("This parent's level is already equal to 5, which cannot be set as a parent");
            }
            ProductCategory newCate = _mapper.Map<ProductCategory>(cate);
            ProductCategory? checkCode = await _dbContext.ProductCategories.Where(c => c.Code == newCate.Code).FirstOrDefaultAsync();
            if (checkCode != null)
            {
                newCate.Code = StringHelper.GenerateRandomCode(8);
            }
         
            newCate.ParentId = _parent.Id;
            newCate.ParentName = _parent.Name;
            newCate.Level = _parent.Level == null ? 1 : _parent.Level + 1;
            await CreateAsync(newCate);
        }
        public async Task<ProductCategoryDto> GetOne(int id)
        {
            ProductCategory? productCategory = await GetByIdAsync(id);
            if (productCategory == null) { throw new NoDataException($"Not found product category with id = {id}"); }
            return _mapper.Map<ProductCategoryDto>(productCategory);
        }
        public async Task<List<ProductCategoryDto>> GetAllImmediateChilds(int id)
        {
            List<ProductCategory> productCategories = await _dbContext.ProductCategories.Where(p => p.ParentId == id && p.IsDeleted != true ).ToListAsync();
            if (productCategories == null) { throw new NoDataException("This parent does not child"); }
            return _mapper.Map<List<ProductCategoryDto>>(productCategories);
        }
    }



}
