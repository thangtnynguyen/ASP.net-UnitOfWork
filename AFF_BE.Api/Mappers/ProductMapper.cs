using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Models.Content.Product;
using AFF_BE.Core.Models.Content.ProductImage;
using AFF_BE.Core.Models.Content.ProductVariant;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class ProductMapper:Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<ProductVariant, ProductVariantManagementDto>()
            .ForMember(dest => dest.Quantity, opt => opt.Ignore());

            CreateMap<CreateProductVariantRequest, ProductVariant>();
            CreateMap<UpdateProductVariantRequest, ProductVariant>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<UpdateProductVariantManagementRequest, ProductVariant>().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductManagementDto>();
            CreateMap<CreateProductWithFileRequest, Product>();
            CreateMap<UpdateProductRequest, Product>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<UpdateProductManagementRequest, Product>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<ProductImage, GetProductImagesManagementRequest>();
            CreateMap<UpdateProductImagesManagementRequest, ProductImage>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            
        }
    }
}
