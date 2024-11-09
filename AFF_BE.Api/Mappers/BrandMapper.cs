using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Models.Content.Brand;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class BrandMapper:Profile
    {
        public BrandMapper()
        {
            CreateMap<Brand, BrandDto>();

            CreateMap<BrandDto, Brand>();

        }
    }
}
