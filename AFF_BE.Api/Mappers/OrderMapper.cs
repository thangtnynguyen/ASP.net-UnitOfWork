using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.Models.Payment.Order;
using AutoMapper;

namespace AFF_BE.Api.Mappers;

public class OrderMapper:Profile
{
    public OrderMapper()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<CreateOrderRequest, Order>()
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.OrderDetails.Sum(d => d.Quantity)))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderDetails.Sum(d => d.Quantity*d.UnitPrice)))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => OrderStatus.Pending)); 
        CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
        CreateMap<CreateOrderDetailRequest, OrderDetail>()
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src=> src.UnitPrice * src.Quantity));

        CreateMap<Product, OrderProductDto>().ReverseMap();
        CreateMap<User, OrderUserDto>().ReverseMap();
    }
}