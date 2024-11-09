using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFF_BE.Core.IRepositories
{
    public interface IBrandRepository
    {
        Task<PagingResult<BrandDto>> GetBrands(PagingRequest request);
        Task CreateAsync(CreateBrandRequest bra);
        Task UpdateAsync(UpdateBrandRequest bra);
        Task<BrandDto> GetOne(int id);
        Task<bool> IsBrandNameExistAsync(string name);
        Task<bool> IsBrandNameExistAsyncUpdate(string name, int id);
        Task UpdateAsyncStastus(int id, bool isDeleted);
        Task<List<BrandDto>> GetAllBrands();
        Task<PagingResult<BrandDto>> SearchBrands(PagingRequest paging, string? keySearch);

    }
}
