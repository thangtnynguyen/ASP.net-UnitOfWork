using AFF_BE.Core.Data.Commission;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.Models.Commission;
using AFF_BE.Core.Models.Payment.Order;
using AutoMapper;

namespace AFF_BE.Api.Mappers
{
    public class IndirectCommissionMapper : Profile
    {
        public IndirectCommissionMapper()
        {
            CreateMap<CreateIndirectCommissionDto, IndirectCommission>();
            CreateMap<IndirectCommission, IndirectCommissionDto>().ReverseMap();
            CreateMap<DirectCommission, DirectCommissionDto>().ReverseMap();
            
        }
    }
}
