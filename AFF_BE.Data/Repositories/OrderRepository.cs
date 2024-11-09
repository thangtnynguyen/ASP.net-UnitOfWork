using AFF_BE.Core.Constants.System;
using AFF_BE.Core.Data.Payment;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Payment.Order;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AFF_BE.Data.Repositories;

public class OrderRepository:RepositoryBase<Order, int>, IOrderRepository
{
    private readonly IMapper _mapper;

    public OrderRepository(AffContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
    {
        _mapper = mapper;
    }
    


    public async Task<PagingResult<OrderDto>> GetAllPaging(string? orderTrackingNumber, int? paymentAccountReceipt, OrderStatus? orderStatus, string? userName,
        DateTime? fromDate, DateTime? toDate, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10)
    {
        var query = _dbContext.Orders.
            Include(o => o.OrderDetails).
            ThenInclude(od=> od.Product).ThenInclude(p=> p.ProductImages).AsQueryable();
        if (!string.IsNullOrEmpty(orderTrackingNumber))
        {
            query = query.Where(b => b.OrderTrackingNumber == orderTrackingNumber);
        }

        if (paymentAccountReceipt.HasValue)
        {
            query = query.Where(b => b.PaymentAccountReceiptId == paymentAccountReceipt.Value);
        } 
        if (!string.IsNullOrEmpty(userName))
        {
            query = query.Where(b => b.UserName.Contains(userName));
        }
        if (orderStatus.HasValue)
        {
            query = query.Where(b => b.OrderStatus == orderStatus.Value);
        }
        if (fromDate.HasValue)
        {
            query = query.Where(b => b.CreatedAt >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(b => b.CreatedAt <= toDate.Value);
        }
        int total = await query.CountAsync();

        if (pageIndex == null) pageIndex = 1;
        if (pageSize == null) pageSize = total;


        if (string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(sortBy))
        {
            query = query.OrderByDescending(b => b.Id);
        }
        else if (string.IsNullOrEmpty(orderBy))
        {
            if (sortBy == SortByConstant.Asc)
            {
                query = query.OrderBy(b => b.Id);
            }
            else
            {
                query = query.OrderByDescending(b => b.Id);
            }
        }
        else if (string.IsNullOrEmpty(sortBy))
        {
            query = query.OrderByDescending(b => b.Id);
        }
        else
        {
            if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Asc)
            {
                query = query.OrderBy(b => b.Id);
            }
            else if (orderBy == OrderByConstant.Id && sortBy == SortByConstant.Desc)
            {
                query = query.OrderByDescending(b => b.Id);
            }
        }
        query = query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);


        var data = await _mapper.ProjectTo<OrderDto>(query).ToListAsync();
        var result = new PagingResult<OrderDto>(data, pageIndex, pageSize, sortBy, orderBy, total);

        return result;
    }
}