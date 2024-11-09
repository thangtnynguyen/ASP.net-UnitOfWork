using AFF_BE.Core.Data.Commission;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Models.Commission;
using AFF_BE.Core.Models.Identity.Permission;
using AFF_BE.Core.Models.Identity.Role;
using AFF_BE.Core.Models.Identity.User;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class UserMapper  : Profile
    {
        public UserMapper()
        {
            // .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.OrderDetails.Sum(d => d.Quantity)))

            CreateMap<CreateUserRequest, User>();
            CreateMap<EditUserInfoRequest, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserTreeDto, User>();
            CreateMap<User, UserTreeDto>();


            CreateMap<CreateDirectCommissionRequest, DirectCommission>();
            CreateMap<User, GetCommissionByUserDto>()
                .ForMember(dest => dest.TotalDirectCommissions, opt => opt.MapFrom(src => src.DirectCommission != null ? src.DirectCommission.Amount : 0))
                .ForMember(dest => dest.TotalIndirectCommissions, opt => opt.MapFrom(src => src.IndirectCommissions != null ? src.IndirectCommissions.Sum(i => i.Price) : 0))
                .ReverseMap();

        } }
}
