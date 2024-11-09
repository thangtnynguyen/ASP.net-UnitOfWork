using AFF_BE.Api.Attributes;
using AFF_BE.Core.Constants.Identity;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Product;
using AFF_BE.Core.Models.Content.ProductImage;
using AFF_BE.Core.Models.Content.ProductVariant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFF_BE.Api.Controllers.Content
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }


        #region CRUD
        [HttpGet("get-products-for-store")]
        public async Task<ApiResult<PagingResult<ProductManagementDto>>> GetProductsForStoreAsync([FromQuery] PagingRequest paging)
        {
            PagingResult<ProductManagementDto> products = await _unitOfWork.Products.GetProductsForStoreAsync(paging);
            return new ApiResult<PagingResult<ProductManagementDto>>() { 
                Status=true,
                Message="Thành công",
                Data=products
            };
        }
        [HttpGet("get-products-variants")]
        public async Task<ApiResult<List<ProductVariantDto>>> GetProductVariants([FromQuery] EntityIdentityRequest<int> request)
        {
            List<ProductVariantDto> products = await _unitOfWork.Products.GetProductVariants(request.Id);
            return new ApiResult<List<ProductVariantDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = products
            };
            
        }
        [HttpGet("search-products-by-keyword")]
        public async Task<ApiResult<List<ProductDto>>> SearchProductsByKeyWord([FromQuery] SearchProductByKeyWord option)
        {
            List<ProductDto> products = await _unitOfWork.Products.SearchProductsByKeyWord(option);
            return new ApiResult<List<ProductDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = products
            };
        }

        [HttpPut("change-product-status")]
        public async Task<ApiResult<bool>> ChangeProductStatus([FromBody] EntityIdentityRequest<int> request)
        {
            await _unitOfWork.Products.ChangeProductStatus(request.Id);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
          
        }

        [HttpPost("create-with-file")]
        //[HasPermission(PermissionConstant.ManageProductCreate)]
        public async Task<ApiResult<bool>> CreateProductWithFile([FromBody] CreateProductWithFileRequest productDto)
        {
            var product = await _unitOfWork.Products.CreateProductWithFile(productDto);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<ProductDto>> GetProduct([FromQuery] EntityIdentityRequest<int> request)
        {
            var product = await _unitOfWork.Products.GetProductAsync(request.Id);
            return new ApiResult<ProductDto>()
            {
                Status = true,
                Message = "Thành công",
                Data = product
            };
        }

        [HttpGet("filter-products-for-store")]
        public async Task<ApiResult<PagingResult<ProductManagementDto>>> FilterProductsForStoreAsync([FromQuery] SearchProductByKeyWord options)
        {
            PagingResult<ProductManagementDto> products = await _unitOfWork.Products.FilterProductsForStoreAsync(options);
            return new ApiResult<PagingResult<ProductManagementDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = products
            };
        }


        [HttpGet("filter-products")]
        public async Task<ApiResult<List<ProductManagementDto>>> FilterProductsForStoreAsync2([FromQuery] SearchProductByKeyWord options)
        {
            List<ProductManagementDto> products = await _unitOfWork.Products.FilterProductsForStoreAsync2(options);

            return new ApiResult<List<ProductManagementDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = products
            };
        }
        [HttpPut("update")]
        //[HasPermission(PermissionConstant.ManageProductEdit)]
        public async Task<ApiResult<bool>> UpdateProduct([FromBody] UpdateProductManagementRequest productDto)
        {
            await _unitOfWork.Products.UpdateProductAsync(productDto);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }

        [HttpDelete("delete")]
        //[HasPermission(PermissionConstant.ManageProductDelete)]
        public async Task<ApiResult<bool>> DeleteProduct([FromBody] EntityIdentityRequest<int> request)
        {
            await _unitOfWork.Products.DeleteProductAsync(request.Id);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }

        [HttpGet("get-product-and-variant")]
        public async Task<ApiResult<ProductManagementDto>> GetOneProductManagementDto([FromQuery] EntityIdentityRequest<int> request)
        {
            ProductManagementDto product = await _unitOfWork.Products.GetOneProductManagementDto(request.Id);
            return new ApiResult<ProductManagementDto>()
            {
                Status = true,
                Message = "Thành công",
                Data = product
            };
        }

        [HttpGet("check-bar-code")]
        public async Task<ApiResult<bool>> CheckBarCode([FromQuery] string barCode, int id)
        {
            var isExist = await _unitOfWork.Products.CheckBarcode(barCode, id);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = isExist
            };
        }

        [HttpGet("check-sku")]
        public async Task<ApiResult<bool>> CheckSKU([FromQuery] string sku, int id)
        {
            var isExist = await _unitOfWork.Products.CheckSKU(sku, id);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = isExist
            };
        }


        #endregion

        #region Images
        [HttpDelete("delete-image")]
        public async Task<ApiResult<bool>> DeleteImages([FromBody] EntityIdentityRequest<int> request)
        {
            await _unitOfWork.Products.DeleteImages(request.Id);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }


        [HttpPut("update-image")]
        public async Task<ApiResult<bool>> UpdateImages([FromBody] UpdateProductImagesManagementRequest request)
        {
            await _unitOfWork.Products.UpdateImages(request.Id, request.base64_image);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }

        [HttpPost("add-image")]
        public async Task<ApiResult<bool>> AddImages([FromBody] CreateProductImageRequest productImage)
        {
            await _unitOfWork.Products.AddImages(productImage.ProductId, productImage.base64_image);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }
        #endregion

    }
}
