using AFF_BE.Api.Services.Interfaces;
using AFF_BE.Core.Constants;
using AFF_BE.Core.Data.Content;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.ISeedWorks;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Content.Banner;
using AFF_BE.Core.Models.Content.News;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AFF_BE.Api.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public NewsController(IMapper mapper, UserManager<User> userManager, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        [HttpGet("paging")]
        public async Task<ApiResult<PagingResult<NewsDto>>> Get([FromQuery] GetNewsRequest request)
        {
            var result = await _unitOfWork.News.GetAllPaging(request.Title, request.SubDescription, request.Content, request.SortBy, request.OrderBy, request.PageIndex, request.PageSize);

            return new ApiResult<PagingResult<NewsDto>>()
            {
                Status = true,
                Message = "Danh sách news đã được lấy thành công!",
                Data = result,
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<bool>> Create([FromForm] CreateNewsRequest request)
        {
            var news = _mapper.Map<CreateNewsRequest, News>(request);

            if (request.ImageFile?.Length > 0)
            {
                news.Image = await _fileService.UploadFileAsync(request.ImageFile, PathFolderConstant.News);
            }
            else
            {
                news.Image = ImageConstant.Avatar;
            }

            await _unitOfWork.News.CreateAsync(news);

            await _unitOfWork.CompleteAsync();


            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Tạo mới News thành công!",
                Data = true
            };
        }

        [HttpPut("update")]
        public async Task<ApiResult<bool>> Update([FromForm] EditNewsRequest request)
        {
            var news = await _unitOfWork.News.GetByIdAsync(request.Id);

            if (news == null)
            {
                return new ApiResult<bool>()
                {
                    Status = false,
                    Message = "Không tìm thấy news",
                    Data = false
                };
            }

            if (request.ImageFile?.Length > 0)
            {
                request.Image = await _fileService.UploadFileAsync(request.ImageFile, PathFolderConstant.News);
                await _fileService.DeleteFileAsync(news.Image);
            }
            else
            {
                request.Image = news.Image;
            }

            _mapper.Map(request, news);

            await _unitOfWork.CompleteAsync();


            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Cập nhật news thành công!",
                Data = true
            };

        }

        [HttpPut("delete")]
        public async Task<ApiResult<bool>> Delete([FromBody] EntityIdentityRequest<int> request)
        {
            var news = await _unitOfWork.News.GetByIdAsync(request.Id);

            if (news == null)
            {
                return new ApiResult<bool>()
                {
                    Status = false,
                    Message = "Không tìm thấy news",
                    Data = false
                };
            }

            await _unitOfWork.News.DeleteAsync(news);

            await _fileService.DeleteFileAsync(news.Image);

            await _unitOfWork.CompleteAsync();

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Xoá news thành công!",
                Data = true,
            };
        }

        [HttpPut("toggle-active")]
        public async Task<ApiResult<bool>> ToggleActiveStatus([FromBody] EntityIdentityRequest<int> request)
        {
            var news = await _unitOfWork.News.GetByIdAsync(request.Id);

            if (news == null)
            {
                return new ApiResult<bool>()
                {
                    Status = false,
                    Message = "Không tìm thấy news",
                    Data = false
                };
            }

            news.IsActive = !news.IsActive;

            await _unitOfWork.CompleteAsync();

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Đã cập nhật trạng thái active thành công!",
                Data = true
            };
        }
    }
}
