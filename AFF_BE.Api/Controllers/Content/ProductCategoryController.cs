using AFF_BE.Api.Attributes;
using AFF_BE.Core.Constants.Identity;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.ProductCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFF_BE.Api.Controllers.Content
{

    [Route("api/product-category")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("is-exist-record-update")]
        public async Task<ApiResult<bool>> IsExistRecordUpdate()
        {
            await _unitOfWork.ProductCategories.IsExistRecordUpdate();

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<ProductCategoryDto>> GetById([FromQuery] EntityIdentityRequest<int> request)
        {
            var productCategoryDto = await _unitOfWork.ProductCategories.GetOne(request.Id);

            return new ApiResult<ProductCategoryDto>()
            {
                Status = true,
                Message = "Thành công",
                Data = productCategoryDto
            };
            
        }
        [HttpGet("get-all")]
        public async Task<ApiResult<PagingResult<ProductCategoryDto>>> GetProductCates([FromQuery] PagingRequest request)
        {
            PagingResult<ProductCategoryDto> productCates = await _unitOfWork.ProductCategories.GetProductCategorysAsync(request);

            return new ApiResult<PagingResult<ProductCategoryDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = productCates
            };

        }
        [HttpGet("get-by-name")]
        public async Task<ApiResult<ProductCategory>> IsRecordWithNameExistsAsync([FromQuery] string? name)
        {
            ProductCategory isExist = await _unitOfWork.ProductCategories.IsRecordWithNameExistsAsync(name);
            if (isExist == null)
            {
                return new ApiResult<ProductCategory>()
                {
                    Status = false,
                    Message = "Không tìm thấy danh mục",
                    Data = null
                };
            }

            return new ApiResult<ProductCategory>()
            {
                Status = true,
                Message = "Thành công",
                Data = isExist
            };
        }

        [HttpGet("get-all-none-paging")]
        public async Task<ApiResult<List<ProductCategoryDto>>> GetProductCates()
        {
            List<ProductCategoryDto> productCates = await _unitOfWork.ProductCategories.GetProductCategorysAsync();
            return new ApiResult<List<ProductCategoryDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = productCates
            };
           
        }
        [HttpGet("get-all-immediate-child")]
        public async Task<ApiResult<List<ProductCategoryDto>>> GetAllImmediateChilds(EntityIdentityRequest<int> request)
        {
            List<ProductCategoryDto> productCates = await _unitOfWork.ProductCategories.GetAllImmediateChilds(request.Id);
            return new ApiResult<List<ProductCategoryDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = productCates
            };
        }
        [HttpGet("filter-with-name-or-id")]
        public async Task<ApiResult<List<ProductCategoryDto>>> FilterWithNameOrId([FromQuery] string? name_or_id)
        {
            List<ProductCategoryDto> productCates = await _unitOfWork.ProductCategories.FilterWithNameOrId(name_or_id);
            return new ApiResult<List<ProductCategoryDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = productCates
            };
        }
        [HttpGet("get-all-tree-category")]
        public async Task<ApiResult<List<ProductCategoryAndChildDto>>> GetProductCatgoryAndChild()
        {
            List<ProductCategoryAndChildDto> productCates = await _unitOfWork.ProductCategories.GetProductCatgoryAndChild(HttpContext);
            return new ApiResult<List<ProductCategoryAndChildDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = productCates
            };
        }
        [HttpPost("create-multi")]
        public async Task<ApiResult<bool>> CreateMultiProductCategory([FromBody] CreateMultiProductCategoryRequest productCategory)
        {
            await _unitOfWork.ProductCategories.CreateMultiAsync(productCategory);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
            
        }
        [HttpPost("create")]
        //[HasPermission(PermissionConstant.ManageProductCategoryCreate)]
        public async Task<ApiResult<bool>> CreateProductCategory([FromBody] CreateProductCategoryRequest productCategory)
        {
            await _unitOfWork.ProductCategories.CreateAsync(productCategory);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }
        [HttpPut("update")]
        //[HasPermission(PermissionConstant.ManageProductCategoryEdit)]
        public async Task<ApiResult<bool>> UpdateProductCategory([FromBody] UpdateProductCategoryRequest productCategory)
        {
            await _unitOfWork.ProductCategories.UpdateAsync(productCategory);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }
        [HttpDelete("delete")]
        //[HasPermission(PermissionConstant.ManageProductCategoryDelete)]
        public async Task<ApiResult<bool>> DeleteCategory([FromBody] EntityIdentityRequest<int> request)
        {
            await _unitOfWork.ProductCategories.DeleteAsync(request.Id);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }
       

    }
}
