using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Payment;
using AutoMapper;

namespace AFF_BE.Api.Mappers;
    public class PaymentAccountMapper : Profile
    {
        public PaymentAccountMapper()
        {
            CreateMap<PaymentAccount, PaymentAccountDto>().ReverseMap();
            CreateMap<CreatePaymentAccountRequest, PaymentAccount>().ReverseMap();
            CreateMap<EditPaymentAccountRequest, PaymentAccount>().ReverseMap();
        }
    }