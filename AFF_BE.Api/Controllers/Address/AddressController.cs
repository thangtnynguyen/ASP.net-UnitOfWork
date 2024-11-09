using AFF_BE.Api.Extension;
using AFF_BE.Core.Data.Address;
using AFF_BE.Core.IRepositories;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Adress;
using AFF_BE.Core.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFF_BE.Api.Controllers.Address
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("create")]
        public async Task<ApiResult<bool>> Create()
        {
            await _unitOfWork.Address.Add();
            return new ApiResult<bool>()
            {
                Status=true,
                Message="Thành công",
                Data=true
            };
        }

        [HttpGet("get-city-id")]
        public async Task<ApiResult<List<City>>> GetCitiesByIdCo54untry([FromQuery] EntityIdentityRequest<int> request)
        {
            List<City> cities = await _unitOfWork.Address.GetCity(request.Id);
            return new ApiResult<List<City>>()
            {
                Status = true,
                Message = "Thành công",
                Data = cities
            };
        }


        [HttpGet("get-district-id")]
        public async Task<ApiResult<List<District>>> GetCitiesByIdCou3ntry([FromQuery] EntityIdentityRequest<int> request)
        {
            List<District> district = await _unitOfWork.Address.GetDistrict(request.Id);
            return new ApiResult<List<District>>()
            {
                Status = true,
                Message = "Thành công",
                Data = district
            };
        }

        [HttpGet("get-ward-id")]
        public async Task<ApiResult<List<Ward>>> GetCitiesByfIdC5ountry([FromQuery] EntityIdentityRequest<int> request)
        {
            List<Ward> wards = await _unitOfWork.Address.GetWard(request.Id);
            return new ApiResult<List<Ward>>()
            {
                Status = true,
                Message = "Thành công",
                Data = wards
            };
        }

        [HttpGet("get-cities")]
        public async Task<ApiResult<List<City>>> GetCitiesByIdCountry([FromQuery] EntityIdentityRequest<int> request)
        {
            List<City> cities = await _unitOfWork.Address.GetCitiesByIdCountry(request.Id);
            return new ApiResult<List<City>>()
            {
                Status = true,
                Message = "Thành công",
                Data = cities
            };

        }


        [HttpGet("get-districts")]
        public async Task<ApiResult<List<District>>> GetDistrictByIdCity([FromQuery] GetDistrictRequest reuquest)
        {
            List<District> districts = await _unitOfWork.Address.GetDistricts(reuquest);
            return new ApiResult<List<District>>()
            {
                Status = true,
                Message = "Thành công",
                Data = districts
            };
        }
        [HttpGet("get-wards")]
        public async Task<ApiResult<List<Ward>>> GetWardByIdDistrict([FromQuery] GetWardRequest reuquest)
        {
            List<Ward> wards = await _unitOfWork.Address.GetWards(reuquest);
            return new ApiResult<List<Ward>>()
            {
                Status = true,
                Message = "Thành công",
                Data = wards
            };
        }

    }
}
