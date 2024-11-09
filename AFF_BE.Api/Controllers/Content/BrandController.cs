using AFF_BE.Core.Exceptions;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Brand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFF_BE.Api.Controllers.Content
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-all")]
        public async Task<ApiResult<PagingResult<BrandDto>>> GetBrands([FromQuery] PagingRequest request)
        {
            var brandDtos = await _unitOfWork.Brands.GetBrands(request);
            
            return new ApiResult<PagingResult<BrandDto>>() 
            {
                Status=true,
                Message="Thành công",
                Data= brandDtos

            };
          
        }


        [HttpGet("get-by-id")]
        public async Task<ApiResult<BrandDto>> GetOne([FromQuery] EntityIdentityRequest<int> request)
        {
            BrandDto bra = await _unitOfWork.Brands.GetOne(request.Id);
            return new ApiResult<BrandDto>()
            {
                Status = true,
                Message = "Thành công",
                Data = bra
            };
        }

        [HttpGet("check-exist")]
        public async Task<ApiResult<bool>> CheckBrandExistenceAsync(string name)
        {
            var isExist = await _unitOfWork.Brands.IsBrandNameExistAsync(name);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = isExist
            };
        }

        [HttpGet("check-exist-update")]
        public async Task<ApiResult<bool>> CheckBrandExistenceAsyncUpdate(string name, int id)
        {
            var isExist = await _unitOfWork.Brands.IsBrandNameExistAsyncUpdate(name, id);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = isExist
            };
        }

        [HttpGet("get-all-brand-pro")]
        public async Task<ApiResult<List<BrandDto>>> GetAllBrands()
        {
            var brands = await _unitOfWork.Brands.GetAllBrands();
            return new ApiResult<List<BrandDto>>()
            {
                Status = true,
                Message = "Thành công",
                Data = brands
            };
        }
        [HttpGet("search")]
        public async Task<ApiResult<PagingResult<BrandDto>>> SearchBrands([FromQuery] PagingRequest request ,[FromQuery] string? keySearch)
        {

            var brandDtos = await _unitOfWork.Brands.SearchBrands(request,keySearch);
            return new ApiResult<PagingResult<BrandDto>> ()
            {
                Status = true,
                Message = "Thành công",
                Data = brandDtos
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<bool>> CreateBrand([FromBody] CreateBrandRequest bra)
        {
            await _unitOfWork.Brands.CreateAsync(bra);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }
        [HttpPut("update")]
        public async Task<ApiResult<bool>> UpdateBrand([FromBody] UpdateBrandRequest bra)
        {
            await _unitOfWork.Brands.UpdateAsync(bra);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }

        [HttpPut("update-status")]
        public async Task<ApiResult<bool>> UpdateStatus([FromBody] int id, bool isDeleted)
        {
            await _unitOfWork.Brands.UpdateAsyncStastus(id, isDeleted);
            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Thành công",
                Data = true
            };
        }
    }

}
