using AFF_BE.Core.Data.Content;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.ProductCategory;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.IRepositories
{
    public interface IProductCategoryRepository: IRepositoryBase<ProductCategory, int>
    {
        Task CreateAsync(CreateProductCategoryRequest cate);
        Task CreateMultiAsync(CreateMultiProductCategoryRequest cate);
        Task DeleteAsync(int id);
        Task<List<ProductCategoryDto>> FilterWithNameOrId(string name_or_id);
        Task<List<ProductCategoryDto>> GetAllImmediateChilds(int id);
        Task<ProductCategoryDto> GetOne(int id);
        Task<PagingResult<ProductCategoryDto>> GetProductCategorysAsync(PagingRequest paging);
        Task<List<ProductCategoryDto>> GetProductCategorysAsync();
        Task<List<ProductCategoryAndChildDto>> GetProductCatgoryAndChild(HttpContext httpContext);
        Task<bool> IsExistRecordUpdate();
        Task<ProductCategory> IsRecordWithNameExistsAsync(string name);
        Task UpdateAsync(UpdateProductCategoryRequest updateCate);

    }
}
