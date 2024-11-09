using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Models.Content.ProductCategory;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class ProductCategoryMapper:Profile
    {
        public ProductCategoryMapper()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();

            CreateMap<CreateProductCategoryRequest, ProductCategory>();

            CreateMap<UpdateProductCategoryRequest, ProductCategory>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<CreateMultiProductCategoryRequest, ProductCategory>();

            CreateMap<ProductCategory, ProductCategoryAndChildDto>();
        }
    }
}
