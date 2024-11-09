using AFF_BE.Core.Constants.System;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Content.News;
using AFF_BE.Data.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Data.Repositories
{
    public class NewsRepository : RepositoryBase<News, int>, INewsRepository
    {
        private readonly IMapper _mapper;
        public NewsRepository(AffContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<PagingResult<NewsDto>> GetAllPaging(string? title, string? subDescription, string? content, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10)
        {
            var query = _dbContext.News.AsQueryable();

            if (!string.IsNullOrEmpty(content))
            {
                query = query.Where(b => b.Content == content);
            }

            if (!string.IsNullOrEmpty(subDescription))
            {
                query = query.Where(b => b.SubDescription == subDescription);
            }

            if (!string.IsNullOrEmpty(title))
            {
                string search = title.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(title.ToLower()));
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


            var data = await _mapper.ProjectTo<NewsDto>(query).ToListAsync();


            var result = new PagingResult<NewsDto>(data, pageIndex, pageSize, sortBy, orderBy, total);

            return result;
        }
    }
}
