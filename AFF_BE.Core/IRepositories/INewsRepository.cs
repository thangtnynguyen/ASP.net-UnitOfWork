using AFF_BE.Core.Data.Content;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Content.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.IRepositories
{
    public interface INewsRepository : IRepositoryBase<News, int>
    {
        Task<PagingResult<NewsDto>> GetAllPaging(string? title, string? subDescription, string? content, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10);
    }
}
