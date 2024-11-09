using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Product;
using AFF_BE.Core.Models.Content.ProductVariant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.IRepositories
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductAsync(int id);
        Task<PagingResult<ProductManagementDto>> GetProductsForStoreAsync(PagingRequest paging);
        Task<PagingResult<ProductManagementDto>> FilterProductsForStoreAsync(SearchProductByKeyWord options);
        Task<List<ProductManagementDto>> FilterProductsForStoreAsync2(SearchProductByKeyWord options);
        Task<List<ProductDto>> SearchProductsByKeyWord(SearchProductByKeyWord option);
        Task<List<ProductVariantDto>> GetProductVariants(int productId);
        Task<Product> CreateProductWithFile(CreateProductWithFileRequest product);
        Task ChangeProductStatus(int Id);
        Task UpdateProductAsync(UpdateProductManagementRequest product);
        Task DeleteProductAsync(int id);
        Task<ProductManagementDto> GetOneProductManagementDto(int id);
        Task AddImages(int productId, string base64_image);
        Task UpdateImages(int imageId, string base64_image);
        Task DeleteImages(int imageId);
        Task<bool> CheckBarcode(string barCode, int? id = null);
        Task<bool> CheckSKU(string sku, int? id = null);

    }


}
