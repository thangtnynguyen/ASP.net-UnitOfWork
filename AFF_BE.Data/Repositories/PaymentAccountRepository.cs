using AFF_BE.Core.Constants.System;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Payment;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AFF_BE.Data.Repositories;

public class PaymentAccountRepository : RepositoryBase<PaymentAccount, int>, IPaymentAccountRepository
{
    private readonly IMapper _mapper;

    public PaymentAccountRepository(AffContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) :
        base(context, httpContextAccessor)
    {
        _mapper = mapper;
    }

    public async Task<PagingResult<PaymentAccountDto>> GetAllPaging(string? bankCode, string? bankName, string? sortBy,
        string? orderBy, int pageIndex = 1,
        int pageSize = 10)
    {
        var query = _dbContext.PaymentAccounts.AsQueryable();

        if (!string.IsNullOrEmpty(bankCode))
        {
            query = query.Where(b => b.BankCode == bankCode);
        }

        if (!string.IsNullOrEmpty(bankName))
        {
            query = query.Where(b => b.BankName.Contains(bankName));
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


        var data = await _mapper.ProjectTo<PaymentAccountDto>(query).ToListAsync();


        var result = new PagingResult<PaymentAccountDto>(data, pageIndex, pageSize, sortBy, orderBy, total);

        return result;
    }
}