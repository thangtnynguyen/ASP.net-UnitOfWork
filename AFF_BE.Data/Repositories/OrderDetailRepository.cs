using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AFF_BE.Data.Repositories;

public class OrderDetailRepository:RepositoryBase<OrderDetail,int>,IOrderDetailRepository
{
    private readonly IMapper _mapper;
    private IRepositoryBase<OrderDetail, int> _repositoryBaseImplementation;

    public OrderDetailRepository(AffContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
    {
        _mapper = mapper;
    }
}