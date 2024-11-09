

using AFF_BE.Core.Data.Content;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;

namespace AFF_BE.Core.IRepositories
{
    public interface IBannerRepository: IRepositoryBase<Banner, int>
    {

        Task<PagingResult<BannerDto>> GetAllPaging(string? place, string? type ,string? title, string? sortBy, string? orderBy, int pageIndex = 1, int pageSize = 10);

    }
}
