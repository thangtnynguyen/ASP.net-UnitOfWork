using AFF_BE.Api.Services.Interfaces;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AFF_BE.Api.Controllers
{
    [Route("api/banner")]
    [ApiController]
    public class BannerController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public BannerController(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork,IFileService fileService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _fileService = fileService;

        }


        [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<BannerDto>>> Get([FromQuery] GetBannerRequest request)
        {
            var result = await _unitOfWork.Banners.GetAllPaging(request.Place,request.Type, request.Title,request.SortBy, request.OrderBy,request.PageIndex, request.PageSize);

            return new ApiResult<PagingResult<BannerDto>>()
            {
                Status = true,
                Message = "Danh sách banner đã được lấy thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<bool>> Create([FromForm] CreateBannerRequest request)
        {

            var banner = _mapper.Map<CreateBannerRequest, Banner>(request);

            banner.Priority = 1;
            banner.Alt=banner.Title;


            if (request.ImageFile?.Length > 0)
            {
                banner.Image = await _fileService.UploadFileAsync(request.ImageFile, PathFolderConstant.Banner);
            }
            else
            {
                banner.Image=ImageConstant.Avatar;
            }

            await _unitOfWork.Banners.CreateAsync(banner);

            await _unitOfWork.CompleteAsync();


            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Tạo mới banner thành công!",
                Data = true
            };
        }

        [HttpPut("update")]
        public async Task<ApiResult<bool>> Update([FromForm] EditBannerRequest request)
        {
            var banner = await _unitOfWork.Banners.GetByIdAsync(request.Id);

            if (banner == null)
            {
                return new ApiResult<bool>()
                {
                    Status = false,
                    Message = "Không tìm thấy banner",
                    Data = false
                };
            }

            if (request.ImageFile?.Length > 0)
            {
                request.Image = await _fileService.UploadFileAsync(request.ImageFile, PathFolderConstant.Banner);
                await _fileService.DeleteFileAsync(banner.Image);
            }
            else
            {
                request.Image = banner.Image;
            }

            _mapper.Map(request, banner);

            await _unitOfWork.CompleteAsync();


            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Cập nhật banner thành công!",
                Data = true
            };

        }

        [HttpPut("delete")]
        public async Task<ApiResult<bool>> Delete([FromBody] EntityIdentityRequest<int> request)
        {
            var banner = await _unitOfWork.Banners.GetByIdAsync(request.Id);

            if (banner == null)
            {
                return new ApiResult<bool>()
                {
                    Status = false,
                    Message = "Không tìm thấy banner",
                    Data = false
                };
            }

            await _unitOfWork.Banners.DeleteAsync(banner);

            await _fileService.DeleteFileAsync(banner.Image);

            await _unitOfWork.CompleteAsync();

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Xoá banner thành công!",
                Data = true,
            };
        }

       
    }
}
